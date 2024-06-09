using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class GameManager : MonoBehaviour
{
    [Tooltip ("The game object where the end screen is.")]
    [SerializeField] private GameObject endScreen;
    [Tooltip ("The game object where the win panel is.")]
    [SerializeField] private GameObject winPanel;
    [Tooltip ("The game object where the lose panel is.")]
    [SerializeField] private GameObject losePanel;
    private int currentCharacterIndex;
    private bool hasWon;
    private int initialPlayerAmount;
    private void Start()
    {
        currentCharacterIndex = 0;
        CharacterTracker.Instance.activeCharacter = CharacterTracker.Instance.activeCharacters[currentCharacterIndex];
    }

    public void StartGame()
    {
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

        endScreen.SetActive(true);
        if (hasWon) { winPanel.SetActive(true); }
        else { losePanel.SetActive(true); }
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
        if (CharacterTracker.Instance.activeCharacters.Exists(character => character is Enemy) &&
            CharacterTracker.Instance.activeCharacters.Count(character => character is Player) < initialPlayerAmount)
        {
            hasWon = false;
            return true;
        }

        if (CharacterTracker.Instance.activeCharacters.Count == 1)
        {
            hasWon = true;
            return true;
        }

        return false;
    }

}