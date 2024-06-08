using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class ButtonNoise : MonoBehaviour
{
    [Tooltip("The Game Object with the audio source component that will play the button's sound.")]
    [SerializeField] private AudioSource audioSource;
    [Tooltip("The sound to play when the button is disabled.")]
    [SerializeField] private AudioClip disabledSound;
    [Tooltip("The sound to play when the button is enabled.")]
    [SerializeField] private AudioClip enabledSound;
    
    public void PlaySound(Button button)
    {
        if (button.IsInteractable())
        {
            audioSource.clip = enabledSound;
            audioSource.Play();
        }
        else
        {
            audioSource.clip = disabledSound;
            audioSource.Play();
        }
    }

    public void PlaySound(bool buttonUsesEnabledColor)
    {
        if (buttonUsesEnabledColor)
        {
            audioSource.clip = enabledSound;
            audioSource.Play();
        }
        else
        {
            audioSource.clip = disabledSound;
            audioSource.Play();
        }
    }
}
