using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;
using USPDigital;

/// <summary>
/// Handles loading and processing word data from an online dictionary API.
/// </summary>
public class WordDataLoader : MonoBehaviour, ICallbackHandler
{
    // Subscribe to the event when the component starts
    private void Start()
    {
        FlowManager.OnUserRequestedWord += LoadWordData;
    }

    // Unsubscribe from the event when the component is destroyed
    private void OnDestroy()
    {
        FlowManager.OnUserRequestedWord -= LoadWordData;
    }

    // Wrapper method to begin loading word data
    private void LoadWordData(string word)
    {
        LoadDataAsync(word);
    }

    // Method implemnted from the interface ICallbackHandler, Loads the data
    public async void LoadDataAsync(string word)
    {
        // Show loading screen
        ScreenManager.Instance.ManageLoadingScreenVisibility(true);

        // Call the helper class to fetch data
        await Helper_UnityWebRequest.Instance.MakeAPICallAndGetResponseAsync(word: word,
            onSuccess: (string response) => { HandleSuccessCallback(response); },
            onError: (Exception exception) => { HandleFailureCallback(exception); });

        // Hide loading screen
        ScreenManager.Instance.ManageLoadingScreenVisibility(false);
    }

    // Method implemnted from the interface ICallbackHandler, Handles Success response
    public void HandleSuccessCallback(string response)
    {
        ProcessJSON(response);
    }

    // Method implemnted from the interface ICallbackHandler, Handles failure response
    public void HandleFailureCallback(Exception exceptions)
    {
        FlowManager.OnDataParseFailed?.Invoke(exceptions.ToString());
        Debug.LogError(exceptions);
    }

    // Processes the JSON string and extracts relevant data
    private void ProcessJSON(string json)
    {
        WordData[] wordArray = JsonConvert.DeserializeObject<WordData[]>(json);
        WordData word = wordArray[0];

        Debug.Log("Word: " + word.Word);

        // Get first available audio
        string audioUrl = word.Phonetics?.Find(p => !string.IsNullOrEmpty(p.Audio))?.Audio;
        if (!string.IsNullOrEmpty(audioUrl))
        {
            PlayAudio(audioUrl);
        }

        // Get first definition and example
        if (word.Meanings.Count > 0 && word.Meanings[0].Definitions.Count > 0)
        {
            var def = word.Meanings[0].Definitions[0];
            Definition wordData = new Definition();

            // Check and assign example or fallback
            wordData.Example = def.Example ?? StaticDataHolder.ExampleWarning;

            // Check and assign definition or fallback
            wordData.DefinitionText = def.DefinitionText ?? StaticDataHolder.DefinitionWarning;

            FlowManager.OnWordDataParsedSuccessfully?.Invoke(wordData);

            Debug.Log("Definition : " + def.DefinitionText);
            Debug.Log("Example : " + def.Example);
        }
        else
        {
            FlowManager.OnDataParseFailed?.Invoke("No Data Found");
        }
    }

    // Downloads and plays audio from the given URL
    private async void PlayAudio(string audioUrl)
    {
        try
        {
            AudioClip clip = await Helper_UnityWebRequest.Instance.DownloadAudioClipAsync(audioUrl);
            AudioManager.Instance.PlayAudio(clip);
        }
        catch (System.Exception ex)
        {
            Debug.LogError("Failed to play audio: " + ex.Message);
        }
    }
}

#region JSON Objects

/// <summary>
/// Root object representing the word and its associated data.
/// </summary>
[Serializable]
public class WordData
{
    [JsonProperty("word")]
    public string Word;

    [JsonProperty("phonetics")]
    public List<Phonetic> Phonetics;

    [JsonProperty("meanings")]
    public List<Meaning> Meanings;
}

/// <summary>
/// Contains the audio pronunciation URL.
/// </summary>
[Serializable]
public class Phonetic
{
    [JsonProperty("audio")]
    public string Audio;
}

/// <summary>
/// Represents a meaning, which can have multiple definitions.
/// </summary>
[Serializable]
public class Meaning
{
    [JsonProperty("definitions")]
    public List<Definition> Definitions;
}

/// <summary>
/// Represents a single definition and an example usage.
/// </summary>
[Serializable]
public class Definition
{
    [JsonProperty("definition")]
    public string DefinitionText;

    [JsonProperty("example")]
    public string Example;
}
#endregion
