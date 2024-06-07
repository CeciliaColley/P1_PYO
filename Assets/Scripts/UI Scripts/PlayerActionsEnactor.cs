using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerActionsEnactor : MonoBehaviour
{
    private enum Action
    {
        Heal,
        RangeAttack,
        MeleeAttack
    }

    private void PerformAction(Action action)
    {
        Player activePlayer = CharacterTracker.Instance.activeCharacter.GetComponent<Player>();
        switch (action)
        {
            case Action.Heal:
                activePlayer.Heal(activePlayer.target);
                break;
            case Action.RangeAttack:
                activePlayer.RangeAttack(activePlayer.target);
                break;
            case Action.MeleeAttack:
                activePlayer.MeleeAttack(activePlayer.target);
                break;
        }

        UpdateActions(activePlayer);
    }

    private void UpdateActions(Player activePlayer)
    {
        activePlayer.actionsLeft--;
        if (activePlayer.actionsLeft <= 0)
        {
            activePlayer.hasActed = true;
        }

        activePlayer.statsDisplayer.UpdateStats();
    }

    public void Heal()
    {
        PerformAction(Action.Heal);
    }

    public void RangeAttack()
    {
        PerformAction(Action.RangeAttack);
    }

    public void MeleeAttack()
    {
        PerformAction(Action.MeleeAttack);
    }
}
