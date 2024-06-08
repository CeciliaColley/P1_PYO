using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReactToAction : MonoBehaviour
{
    [Tooltip("The Game Object with the audio source component that will play the reaction sound.")]
    [SerializeField] private AudioSource audioSource;
    public IEnumerator ChangeColor(SpriteRenderer spriteRenderer, Color originalColor, Color targetColor, float duration)
    {
        float timer = 0f;

        while (timer < duration)
        {
            float t = timer / duration;
            spriteRenderer.color = Color.Lerp(originalColor, targetColor, t);
            timer += Time.deltaTime;
            yield return null;
        }
        spriteRenderer.color = targetColor;
    }

    public IEnumerator FlashColor(Character character, Color targetColor)
    {
        SpriteRenderer spriteRenderer = character.GetComponent<SpriteRenderer>();
        Color originalColor = spriteRenderer.color;
        float duration = character.CharacterReactionInfo.reactionDuration;
        StartCoroutine(ChangeColor(spriteRenderer, originalColor, targetColor, duration));
        yield return new WaitUntil(() => spriteRenderer.color == targetColor);
        StartCoroutine(ChangeColor(spriteRenderer, targetColor, originalColor, duration));
    }

    public void Flash(Character character, Color targetColor)
    {
        StartCoroutine(FlashColor(character, targetColor));
    }

    public void PlaySound(AudioClip sound)
    {
        audioSource.clip = sound;
        audioSource.Play();
    }
}
