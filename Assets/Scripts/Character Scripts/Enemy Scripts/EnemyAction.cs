using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAction : MonoBehaviour, ICharacterAction
{
    public void Act(Character character)
    {
        Enemy enemy = character.GetComponent<Enemy>();
        enemy.DetermineTarget();
        if (enemy.CanMeleeAttack(enemy.target))
        {
            enemy.MeleeAttack(enemy.target);
        }
        else if (enemy.CanRangeAttack(enemy.target))
        {
            enemy.RangeAttack(enemy.target);
        }

        enemy.hasActed = true;
    }

}
