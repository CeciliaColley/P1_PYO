using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterActions : MonoBehaviour
{
    protected ReactToAction reactToAction;
    public void CheckForKill(Character target)
    {
        if (target.Health <= 0)
        {
            target.Die();
        }
    }
    public void RangeAttack(Character attacker, Character target)
    {
        target.Health -= attacker.rangedAttackDamage;
        CheckForKill(target);
        if (reactToAction != null)
        {
            reactToAction.DefaultAttackReaction(target);
        }
    }
    public void MeleeAttack(Character attacker, Character target)
    {
        target.Health -= attacker.meleeAttackDamage;
        CheckForKill(target);
        if (reactToAction != null)
        {
            reactToAction.DefaultAttackReaction(target);
        }
    }
    public void Heal(Character healer, Character target)
    {
        target.Health += healer.healAmount;
        if (target.Health > target.initialHealth)
        {
            target.Health = target.initialHealth;
        }
        if (reactToAction != null)
        {
            reactToAction.Flash(target, target.CharacterReactionInfo.healedColor);
            reactToAction.PlaySound(target.CharacterReactionInfo.healSound);
        }
    }
    public bool CanRangeAttack(Character attacker, Character target)
    {
        if ((target != attacker) &
            (attacker.rangedAttackDamage > 0) &&
            BoardRules.Instance.IsInDiamondRange(attacker, target, attacker.rangedAttackMaxRange) &&
            !BoardRules.Instance.IsInSquareRange(attacker, target, 1))
        {
            return true;
        }
        else return false;
    }
    public bool CanMeleeAttack(Character attacker, Character target)
    {
        if ((target != attacker) &&
            (attacker.meleeAttackDamage > 0) &&
            BoardRules.Instance.IsInSquareRange(attacker, target, 1))
        {
            return true;
        }
        else return false;
    }
    public bool CanHeal(Character healer, Character target)
    {
        if ((healer.healAmount > 0 && target == healer) ||
            (healer.healMaxRange > 0 &&
            healer.healAmount > 0 &&
            BoardRules.Instance.IsInDiamondRange(healer, target, healer.healMaxRange)))
        {
            return true;
        }
        else return false;

    }
}