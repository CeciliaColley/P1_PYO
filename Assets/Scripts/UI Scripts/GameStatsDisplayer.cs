using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.TextCore.Text;
using UnityEngine.UI;

public class GameStatsDisplayer : MonoBehaviour
{
    [Tooltip ("The textbox that displays the current character's turn.")]
    [SerializeField] private TextMeshProUGUI playerTurnText;
    [Tooltip("The box that encloses the stats of the current character.")]
    [SerializeField] private RectTransform activeBox;
    [Tooltip("The speed at which the active box moves.")]
    [SerializeField] private float moveSpeed;
    private void Start()
    {
        CharacterTracker.Instance.OnActiveCharacterChanged += OnActiveCharacterChanged; 
    }

    private void OnDisable()
    {
        CharacterTracker.Instance.OnActiveCharacterChanged -= OnActiveCharacterChanged;
    }

    private void OnActiveCharacterChanged(Character newCharacter)
    {
        DisplayCurrentPlayersName();
        SlideActiveBoxOverCurrentCharacter();
    }
    public void DisplayCurrentPlayersName()
    {
        playerTurnText.text = CharacterTracker.Instance.activeCharacter.characterName;
    }

    public void SlideActiveBoxOverCurrentCharacter()
    {
        StartCoroutine(SlideCoroutine());
    }

    private IEnumerator SlideCoroutine()
    {
        Vector3 startPosition = activeBox.position;
        Vector3 targetPosition = CharacterTracker.Instance.activeCharacter.characterDisplay.position;

        float elapsedTime = 0;

        while (elapsedTime < moveSpeed)
        {
            activeBox.position = Vector3.Lerp(startPosition, targetPosition, elapsedTime / moveSpeed);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        activeBox.position = targetPosition;
    }
}
