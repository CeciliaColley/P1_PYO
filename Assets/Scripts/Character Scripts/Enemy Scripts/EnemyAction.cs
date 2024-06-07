using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAction : CharacterActions, ICharacterAction
{
    public void Act(Character character)
    {
        Enemy enemy = character.GetComponent<Enemy>();
        enemy.DetermineTarget();
        if (CanMeleeAttack(enemy, enemy.target))
        {
            MeleeAttack(enemy, enemy.target);
            enemy.actionsLeft--;
        }
        else if (CanRangeAttack(enemy, enemy.target))
        {
            RangeAttack(enemy, enemy.target);
            enemy.actionsLeft--;
        }

        enemy.hasActed = true;
    }

}
