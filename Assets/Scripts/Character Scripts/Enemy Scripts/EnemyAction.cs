using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAction : CharacterActions, ICharacterAction
{
    private void Awake()
    {
        reactToAction = GetComponent<ReactToAction>();
    }
    public void Act(Character character)
    {
        StartCoroutine(WaitUntilHasMovedThenAttack(character));
    }
    private IEnumerator WaitUntilHasMovedThenAttack(Character character)
    {
        yield return new WaitUntil(() => character.hasMoved);
        Enemy enemy = character.GetComponent<Enemy>();
        if (enemy != null) { yield break; }
        enemy.DetermineTarget();

        if (CanMeleeAttack(enemy, enemy.target))
        {
            MeleeAttack(enemy, enemy.target);
            enemy.ActionsLeft--;
        }
        else if (CanRangeAttack(enemy, enemy.target))
        {
            RangeAttack(enemy, enemy.target);
            enemy.ActionsLeft--;
        }

        enemy.hasActed = true;
    }

}
