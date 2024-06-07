using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndTurnButtonBehaviour : MonoBehaviour
{
    public void EndTurn()
    {
        CharacterTracker.Instance.activeCharacter.hasActed = true;
        CharacterTracker.Instance.activeCharacter.hasMoved = true;
    }
}
