using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class GameManager : MonoBehaviour
{
    private int currentCharacterIndex;
    private void Start()
    {
        currentCharacterIndex = 0;
        CharacterTracker.Instance.activeCharacter = CharacterTracker.Instance.activeCharacters[currentCharacterIndex];
        StartCoroutine(GameLoop());
    }
    private IEnumerator GameLoop()
    {
        while (!IsGameOver())
        {
            CharacterTracker.Instance.activeCharacter.Move();
            CharacterTracker.Instance.activeCharacter.Act();
            yield return new WaitUntil(() => CharacterTracker.Instance.activeCharacter.hasMoved && CharacterTracker.Instance.activeCharacter.hasActed);

            GoToNextCharacter();

            yield return null;
        }
    }
    private void GoToNextCharacter()
    {
        currentCharacterIndex++;
        if (currentCharacterIndex >= CharacterTracker.Instance.activeCharacters.Count)
        {
            currentCharacterIndex = 0;
            ResetAllCharacters();
        }
        CharacterTracker.Instance.activeCharacter = CharacterTracker.Instance.activeCharacters[currentCharacterIndex];
    }

    private void ResetAllCharacters()
    {
        foreach (Character character in CharacterTracker.Instance.activeCharacters)
        {
            character.ResetCharacter();
        }
    }
    private bool IsGameOver()
    {
        if (CharacterTracker.Instance.activeCharacters.Count == 1)
        {
            return true;
        }
        return false;
    }
}