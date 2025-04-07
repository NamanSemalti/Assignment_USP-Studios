using System;
using UnityEngine;
using USPDigital;

/// <summary>
/// Manages the main flow of the application, including user interactions and helper initialization.
/// </summary>
public class FlowManager : SingletonMonoBehaviour<FlowManager>
{
    /// <summary>
    /// Invoked when the user requests a word lookup.
    /// </summary>
    public static Action<string> OnUserRequestedWord = delegate { }; // Parameter: word to look up

    /// <summary>
    /// Invoked when the word data has been successfully parsed.
    /// </summary>
    public static Action<Definition> OnWordDataParsedSuccessfully = delegate { };

    /// <summary>
    /// Invoked when parsing the word data has failed.
    /// </summary>
    public static Action<string> OnDataParseFailed = delegate { }; // Parameter: error message

    /// <summary>
    /// Invoked when the character state changes.
    /// </summary>
    public static Action<CharacterState> OnCharacterStateChanged = delegate { };

    private void Start()
    {
        InitHelperClass();
    }

    // Initializes the Helper_UnityWebRequest if it hasn't been added to the scene yet
    private void InitHelperClass()
    {
        if (Helper_UnityWebRequest.Instance == null)
        {
            GameObject helper = new GameObject();
            helper.name = "Helper";
            helper.AddComponent<Helper_UnityWebRequest>();
        }
    }
}
