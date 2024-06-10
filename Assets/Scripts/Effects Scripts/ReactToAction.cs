using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class ReactToAction : MonoBehaviour
{
    [Tooltip("The Game Object with the audio source component that will play the reaction sound.")]
    [SerializeField] private AudioSource audioSource;

    public IEnumerator ChangeColor(SpriteRenderer spriteRenderer, Color originalColor, Color targetColor, float duration)
    {
        if (spriteRenderer == null)
        {
            yield break;
        }

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
        if (character == null)
        {
            yield break;
        }

        SpriteRenderer spriteRenderer = character.GetComponent<SpriteRenderer>();
        if (spriteRenderer == null)
        {
            yield break;
        }

        Color originalColor = spriteRenderer.color;
        float duration = character.CharacterReactionInfo != null ? character.CharacterReactionInfo.reactionDuration : 0f;
        StartCoroutine(ChangeColor(spriteRenderer, originalColor, targetColor, duration));
        yield return new WaitUntil(() => spriteRenderer.color == targetColor);
        StartCoroutine(ChangeColor(spriteRenderer, targetColor, originalColor, duration));
    }

    public void Flash(Character character, Color targetColor)
    {
        if (character != null)
        {
            StartCoroutine(FlashColor(character, targetColor));
        }
    }

    public void PlaySound(AudioClip sound)
    {
        if (audioSource != null && sound != null)
        {
            audioSource.clip = sound;
            audioSource.Play();
        }
    }

    public AudioClip GetRandomSound(params AudioClip[] sounds)
    {
        if (sounds == null || sounds.Length == 0)
        {
            return null;
        }

        int randomIndex = Random.Range(0, sounds.Length);
        return sounds[randomIndex];
    }

    public void DefaultAttackReaction(Character target)
    {
        if (target != null && target.CharacterReactionInfo != null)
        {
            Flash(target, target.CharacterReactionInfo.attackedColor);
            AudioClip sound = GetRandomSound(target.CharacterReactionInfo.attackSound1,
                                              target.CharacterReactionInfo.attackSound2,
                                              target.CharacterReactionInfo.attackSound3,
                                              target.CharacterReactionInfo.attackSound4,
                                              target.CharacterReactionInfo.attackSound5);
            PlaySound(sound);
        }
    }
}