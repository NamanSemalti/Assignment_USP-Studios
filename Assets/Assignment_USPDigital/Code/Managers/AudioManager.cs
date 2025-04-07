using System.Collections;
using UnityEngine;

namespace USPDigital
{
    /// <summary>
    /// Manages audio playback and communicates character state changes.
    /// </summary>
    public class AudioManager : SingletonMonoBehaviour<AudioManager>
    {
        [SerializeField] private AudioSource audioSource;

        /// <summary>
        /// Plays the given audio clip and manages character state transitions.
        /// </summary>
        /// <param name="audioClip">The audio clip to play.</param>
        public void PlayAudio(AudioClip audioClip)
        {
            StartCoroutine(AudioRoutine(audioClip));
        }

        // Plays the audio and updates character state while the audio is playing
        private IEnumerator AudioRoutine(AudioClip audioClip)
        {
            audioSource.clip = audioClip;
            audioSource.Play();
            FlowManager.OnCharacterStateChanged?.Invoke(CharacterState.Talking);

            // Wait until audio playback is finished
            yield return new WaitWhile(() => audioSource.isPlaying);

            FlowManager.OnCharacterStateChanged?.Invoke(CharacterState.Idle);
            audioSource.clip = null;

            // Cleanup the audio clip to free memory
            Destroy(audioClip);
        }
    }
}
