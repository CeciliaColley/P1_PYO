using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerActionsEnactor : CharacterActions
{    
    [Tooltip("The action this button should perform.")]
    [SerializeField] private Action action;
    [Tooltip("The color of the button's image when it's enabled.")]
    [SerializeField] private Color enabledColor;
    private Button button;
    private ButtonNoise buttonNoise;
    private enum Action
    {
        Heal,
        RangeAttack,
        MeleeAttack
    }

    private void Start()
    {
        button = GetComponent<Button>();
        buttonNoise = GetComponent<ButtonNoise>();
    }

    private void PerformAction(Action action, bool buttonUsesEnabledColor)
    {
        if ( buttonNoise != null)
        {
            buttonNoise.PlaySound(buttonUsesEnabledColor);
        }        
        if (!buttonUsesEnabledColor) { return; }

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
        activePlayer.ActionsLeft--;
        if (activePlayer.ActionsLeft <= 0)
        {
            activePlayer.hasActed = true;
        }
    }

    public void OnActionClick()
    {
        if (button.image.color == enabledColor)
        {
            PerformAction(action, true);
        }
        else
        {
            PerformAction(action, false);
        }
    }
}
