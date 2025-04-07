using TMPro;
using UnityEngine;

namespace USPDigital
{
    /// <summary>
    /// Manages the visibility of all the screens in the UI.
    /// </summary>
    public class ScreenManager : SingletonMonoBehaviour<ScreenManager>
    {
        [Header("Loading Screen")]
        [SerializeField] private GameObject loadingScreen; // Reference to the loading screen UI

        [Header("Error Screen")]
        [SerializeField] private GameObject errorScreen; // Reference to the error screen UI
        [SerializeField] private TextMeshProUGUI textTMP_ErrorText; // UI text field to display error messages

        /// <summary>
        /// Toggles the visibility of the loading screen.
        /// </summary>
        /// <param name="isActive">Whether to show or hide the loading screen.</param>
        public void ManageLoadingScreenVisibility(bool isActive)
        {
            loadingScreen.SetActive(isActive);
        }

        /// <summary>
        /// Displays the error screen with the specified error message.
        /// </summary>
        /// <param name="message">The error message to show on the screen.</param>
        public void EnableErrorScreen(string message)
        {
            textTMP_ErrorText.text = "Parse failed due to : " + message;
            errorScreen.SetActive(true);
        }
    }
}
