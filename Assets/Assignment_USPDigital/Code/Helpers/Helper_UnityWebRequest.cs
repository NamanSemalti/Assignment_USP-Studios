using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

namespace USPDigital
{
    public class Helper_UnityWebRequest : SingletonMonoBehaviour<Helper_UnityWebRequest>
    {
        // Base URL for fetching data from API
        private const string baseURL = "https://api.dictionaryapi.dev/api/v2/entries/en/";

        /// <summary>
        /// Fetches dictionary data for the given word asynchronously.
        /// </summary>
        /// <param name="word">The word to look up in the dictionary API.</param>
        /// <param name="onSuccess">Callback invoked when the request is successful, passing the JSON response string.</param>
        /// <param name="onError">Callback invoked when an error occurs, passing the exception.</param>
        public async Task MakeAPICallAndGetResponseAsync(string word, Action<string> onSuccess = null, Action<Exception> onError = null)
        {
            // Validate input word
            if (string.IsNullOrWhiteSpace(word))
            {
                onError?.Invoke(new ArgumentException("Word cannot be null or empty."));
                return;
            }

            string finalURL = baseURL + word; //Concatenating URL and Word

            try
            {
                using (UnityWebRequest request = UnityWebRequest.Get(finalURL))
                {
                    // Send the request and wait asynchronously
                    UnityWebRequestAsyncOperation operation = request.SendWebRequest();
                    while (!operation.isDone)
                        await Task.Yield();

                    // Check the result
                    if (request.result == UnityWebRequest.Result.Success)
                    {
                        string responseData = request.downloadHandler.text;
                        onSuccess?.Invoke(responseData);
                    }
                    else
                    {
                        string errorDetails = $"Error: {request.error}, StatusCode: {request.responseCode}";
                        onError?.Invoke(new Exception(errorDetails));
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle unexpected exceptions
                onError?.Invoke(ex);
            }
        }

        /// <summary>
        /// Downloads an audio clip from the specified URL asynchronously.
        /// </summary>
        /// <param name="url">The URL of the audio file.</param>
        /// <returns>An AudioClip object if successful; otherwise, an exception is thrown.</returns>
        public async Task<AudioClip> DownloadAudioClipAsync(string url)
        {
            var tcs = new TaskCompletionSource<AudioClip>();

            // Prepare the request for an audio clip
            UnityWebRequest www = UnityWebRequestMultimedia.GetAudioClip(url, AudioType.MPEG);
            var operation = www.SendWebRequest();

            // Wait for the request to complete asynchronously
            while (!operation.isDone)
                await Task.Yield(); // Non-blocking wait

            if (www.result == UnityWebRequest.Result.Success)
            {
                // Extract and return the AudioClip
                AudioClip clip = DownloadHandlerAudioClip.GetContent(www);
                tcs.SetResult(clip);
            }
            else
            {
                // Log error and complete task with exception
                Debug.LogError("Audio download failed: " + www.error);
                tcs.SetException(new System.Exception(www.error));
            }

            return await tcs.Task;
        }
    }
}
