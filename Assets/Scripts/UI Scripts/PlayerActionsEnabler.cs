using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.GraphicsBuffer;

public class PlayerActionsEnabler : CharacterActions
{
    [Tooltip("Game object with the script that detects input.")]
    [SerializeField] private InputManager inputManager;
    [Tooltip("The color of the button when it's enabled.")]
    [SerializeField] private Color enabledColor;
    [Tooltip("The color of the button when it's disabled.")]
    [SerializeField] private Color disabledColor;
    [Tooltip("A reference to the panel that shows the player's available actions.")]
    [SerializeField] private Button healButton;
    [Tooltip("A reference to the panel that shows the player's available actions.")]
    [SerializeField] private Button rangeButton;
    [Tooltip("A reference to the panel that shows the player's available actions.")]
    [SerializeField] private Button meleeButton;

    private void Start()
    {
        inputManager.onMovementInput += CloseOnMovement;
    }

    private void OnEnable()
    {
        inputManager.onInteractionInput -= CharacterTracker.Instance.activeCharacter.GetComponent<PlayerAction>().OnInteractionPerformed;
    }

    private void OnDisable()
    {
        inputManager.onInteractionInput += CharacterTracker.Instance.activeCharacter.GetComponent<PlayerAction>().OnInteractionPerformed;
    }

    private void CloseOnMovement(Vector2 inputVector)
    {
        gameObject.SetActive(false);
    }

    public void toggleActions(Character player, Character target)
    {
        toggleMeleeButton(player, target);
        toggleRangeButton(player, target);
        toggleHealButton(player, target);
    }
    private void toggleMeleeButton(Character player, Character target)
    {
        if (CanMeleeAttack(player, target))
        {
            meleeButton.image.color = enabledColor;
        }
        else
        {
            meleeButton.image.color = disabledColor;
        }
    }
    private void toggleRangeButton(Character player, Character target)
    {
        if (CanRangeAttack(player, target))
        {
            rangeButton.image.color = enabledColor;
        }
        else
        {
            rangeButton.image.color = disabledColor;
        }
    }
    private void toggleHealButton(Character player, Character target)
    {
        if (CanHeal(player, target))
        {
            healButton.image.color = enabledColor;
        }
        else
        {
            healButton.image.color = disabledColor;
        }
    }

}
