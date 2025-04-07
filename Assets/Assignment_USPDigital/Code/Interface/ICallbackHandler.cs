using System;

/// <summary>
/// Interface defining callback methods for handling asynchronous operations.
/// </summary>
public interface ICallbackHandler
{
    /// <summary>
    /// Initiates asynchronous data loading with an optional word parameter.
    /// </summary>
    /// <param name="word">The word to load data for (optional).</param>
    void LoadDataAsync(string word = "");

    /// <summary>
    /// Called when the asynchronous operation completes successfully.
    /// </summary>
    /// <param name="response">The response string from the operation.</param>
    void HandleSuccessCallback(string response);

    /// <summary>
    /// Called when the asynchronous operation fails.
    /// </summary>
    /// <param name="exceptions">The exception thrown during the operation.</param>
    void HandleFailureCallback(Exception exceptions);
}
