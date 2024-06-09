using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UnchangingStatsDisplayer : CharacterInformationUI
{
    private void Start()
    {
        textComponent = characterDisplay.GetComponentInChildren<TextMeshProUGUI>();
        DisplayStats();
    }

    public void DisplayStats()
    {
        string charactersUnchangingStats = BuildCharacterInfo(character.characterInfo);
        textComponent.text = charactersUnchangingStats;
    }
}
