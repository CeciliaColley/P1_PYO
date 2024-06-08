using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StatsDisplayer : MonoBehaviour
{
    [Tooltip("The UI that displays the stats of the character.")]
    public GameObject characterDisplay;
    private TextMeshProUGUI textComponent;
    private Character character;

    private void Awake()
    {
        character = GetComponent<Character>();
        character.characterDisplay = characterDisplay;
    }
    private void Start()
    {
        textComponent  = characterDisplay.GetComponentInChildren<TextMeshProUGUI>();
        character.DisplayedStatChanged += UpdateStats;
        UpdateStats(0);
    }
    public void UpdateStats(float newValue)
    {
        textComponent.text = BuildCharacterInfo();
    }
    public void UpdateStats(bool isAlive)
    {
        textComponent.text = "Eliminated";
        Image characterImage = characterDisplay.GetComponentInChildren<Image>();
        characterImage.color = Color.black;
    }
    public string BuildCharacterInfo()
    {
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.AppendLine(character.characterName);
        stringBuilder.AppendLine("Health: " + character.Health);
        stringBuilder.AppendLine("Moves left: " + character.MovesLeft);
        stringBuilder.AppendLine("Actions left: " + character.ActionsLeft);

        return stringBuilder.ToString();
    }
}
