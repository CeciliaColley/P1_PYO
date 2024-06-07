using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerActionsEnactor : CharacterActions
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
                Heal(activePlayer, activePlayer.target);
                break;
            case Action.RangeAttack:
                RangeAttack(activePlayer, activePlayer.target);
                break;
            case Action.MeleeAttack:
                MeleeAttack(activePlayer, activePlayer.target);
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
