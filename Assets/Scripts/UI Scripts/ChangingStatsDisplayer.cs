using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ChangingStatsDisplayer : CharacterInformationUI
{
    private void Awake()
    {
        character = GetComponent<Character>();
        character.characterDisplay = characterDisplay;
    }
    private void Start()
    {
        textComponent  = characterDisplay.GetComponentInChildren<TextMeshProUGUI>();
        character.DisplayedStatChanged += UpdateStats;
        UpdateStats();
    }
    public void UpdateStats()
    {
        textComponent.text = BuildCharacterInfo("Name: ", character.characterName, "Health: ", character.Health, "Moves left: ", character.MovesLeft, "Actions left: ", character.ActionsLeft);
    }
    public void UpdateStats(float newValue)
    {
        textComponent.text = BuildCharacterInfo("Name: ", character.characterName, "Health: ", character.Health, "Moves left: ", character.MovesLeft, "Actions left: ", character.ActionsLeft);
    }
    public void UpdateStats(bool isAlive)
    {
        textComponent.text = "Eliminated";
        Image characterImage = characterDisplay.GetComponentInChildren<Image>();
        characterImage.color = Color.black;
    }
    
}
