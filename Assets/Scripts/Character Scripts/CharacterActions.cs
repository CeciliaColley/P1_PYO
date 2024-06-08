using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterActions : MonoBehaviour
{
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

    }
    public void MeleeAttack(Character attacker, Character target)
    {
        target.Health -= attacker.meleeAttackDamage;
        CheckForKill(target);

    }
    public void Heal(Character healer, Character target)
    {
        target.Health += healer.healAmount;
        if (target.Health > target.initialHealth)
        {
            target.Health = target.initialHealth;
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
