using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.GraphicsBuffer;

public class PlayerActionsUI : MonoBehaviour
{
    [Tooltip("A reference to the panel that shows the player's available actions.")]
    [SerializeField] private Button healButton;
    [Tooltip("A reference to the panel that shows the player's available actions.")]
    [SerializeField] private Button rangeButton;
    [Tooltip("A reference to the panel that shows the player's available actions.")]
    [SerializeField] private Button meleeButton;

    public void toggleActions(Character player, Character target)
    {
        toggleMeleeButton(player, target);
        toggleRangeButton(player, target);
        toggleHealButton(player, target);
    }

    private void toggleMeleeButton(Character player, Character target)
    {
        if (player.CanMeleeAttack(target))
        {
            meleeButton.enabled = true;
        }
        else meleeButton.enabled = false;
    }

    private void toggleRangeButton(Character player, Character target)
    {
        if (player.CanRangeAttack(target))
        {
            rangeButton.enabled = true;
        }
        else rangeButton.enabled = false;
    }

    private void toggleHealButton(Character player, Character target)
    {
        if (player.CanHeal(target))
        {
            healButton.enabled = true;
        }
        else healButton.enabled = false;
    }
}
