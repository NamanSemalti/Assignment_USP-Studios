using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace USPDigital
{
    /// <summary>
    /// Manages the user interface interactions for word lookup and character animations.
    /// </summary>
    public class UIManager : MonoBehaviour
    {
        // Input field for user to type the word
        [SerializeField] private TMP_InputField inputField_Word;

        // Button to trigger meaning lookup
        [SerializeField] private Button button_GetMeaning;

        // UI Text to show input warnings (e.g., white space)
        [SerializeField] private TextMeshProUGUI textTMP_Warning;

        // UI Texts for displaying definition and example
        [SerializeField] private TextMeshProUGUI textTMP_Definition;
        [SerializeField] private TextMeshProUGUI textTMP_Example;

        // Animator to control the character's animations
        [SerializeField] private Animator anim_characterAnimator;
        private void Start()
        {
            AddListeners();
            CheckForSpaces(inputField_Word.text);
            SubscribeEvents();
        }

        // Unity lifecycle method
        private void OnDestroy()
        {
            UnsubscribeEvents();
        }

        // Subscribes to events from the FlowManager
        private void SubscribeEvents()
        {
            FlowManager.OnWordDataParsedSuccessfully += UpdateWordDataUI;
            FlowManager.OnDataParseFailed += EnableErrorScreen;
            FlowManager.OnCharacterStateChanged += PlayCharacterAnimation;
        }

        // Unsubscribes from events when the object is destroyed
        private void UnsubscribeEvents()
        {
            FlowManager.OnWordDataParsedSuccessfully -= UpdateWordDataUI;
            FlowManager.OnDataParseFailed -= EnableErrorScreen;
            FlowManager.OnCharacterStateChanged -= PlayCharacterAnimation;
        }

        // Adds UI event listeners to input and button
        private void AddListeners()
        {
            inputField_Word.onValueChanged.AddListener(CheckForSpaces);
            button_GetMeaning.onClick.AddListener(GetWordMeaning);
            button_GetMeaning.interactable = false;
        }

        // Invokes the word request event
        private void GetWordMeaning()
        {
            FlowManager.OnUserRequestedWord?.Invoke(inputField_Word.text);
        }

        // Checks if input has invalid white spaces and updates UI accordingly
        private void CheckForSpaces(string word)
        {
            if (word == string.Empty)
            {
                button_GetMeaning.interactable = false;
                return;
            }

            if (HelperFunctions.HasEmptySpace(word))// Method called from HelperFunctions.cs
            {
                textTMP_Warning.text = StaticDataHolder.WhiteSpaceWarning;
                button_GetMeaning.interactable = false;
            }
            else
            {
                button_GetMeaning.interactable = true;
                textTMP_Warning.text = string.Empty;
            }
        }

        // Updates UI elements with definition and example
        private void UpdateWordDataUI(Definition definition)
        {
            textTMP_Definition.text = "Definiton : " + definition.DefinitionText;
            textTMP_Example.text = "Example : " + definition.Example;
        }

        // Triggers error screen when word parsing fails
        private void EnableErrorScreen(string message)
        {
            ScreenManager.Instance.EnableErrorScreen(message);
        }

        // Controls character animation state based on current state
        private void PlayCharacterAnimation(CharacterState state)
        {
            switch (state)
            {
                case CharacterState.Idle:
                    anim_characterAnimator.SetBool(StaticDataHolder.CharacterTalkAnimationKey, false);
                    break;
                case CharacterState.Talking:
                    anim_characterAnimator.SetBool(StaticDataHolder.CharacterTalkAnimationKey, true);
                    break;
                default:
                    anim_characterAnimator.SetBool(StaticDataHolder.CharacterTalkAnimationKey, false);
                    break;
            }
        }
    }
}
