using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class GameManager : MonoBehaviour
{
    private int currentCharacterIndex;

    void Start()
    {
        currentCharacterIndex = 0;
        StartCoroutine(GameLoop());
    }

    private IEnumerator GameLoop()
    {
        while (!IsGameOver())
        {

            CharacterTracker.Instance.activeCharacter = CharacterTracker.Instance.activeCharacters[currentCharacterIndex];

            while (CharacterTracker.Instance.activeCharacter.movesLeft > 0)
            {
                CharacterTracker.Instance.activeCharacter.Move();
                yield return new WaitUntil(() => CharacterTracker.Instance.activeCharacter.hasMoved);
            }

            NextCharacter();

            yield return null;
        }
    }

    private void NextCharacter()
    {
        currentCharacterIndex++;
        if (currentCharacterIndex >= CharacterTracker.Instance.activeCharacters.Count)
        {
            currentCharacterIndex = 0;
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