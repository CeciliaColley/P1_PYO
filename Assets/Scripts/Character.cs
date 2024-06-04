using System;
using UnityEngine;
using System.Collections;


public class Character : MonoBehaviour
{
    protected string characterName;
    protected int initialHealth;
    protected int speed;
    protected int meleeAttackDamage;
    protected int rangedAttackDamage;
    protected int rangedAttackMaxRange;
    protected int healAmount;
    protected int healMaxRange;
    protected bool canHealOthers;

    public void Initialize(string characterStatsPath)
    {
        SO_Character characterStats = Resources.Load<SO_Character>("ScriptableObjects/" + characterStatsPath);

        if (characterStats != null)
        {
            characterName = characterStats.characterName;
            initialHealth = characterStats.initialHealth;
            speed = characterStats.speed;
            meleeAttackDamage = characterStats.meleeAttackDamage;
            rangedAttackDamage = characterStats.rangedAttackDamage;
            rangedAttackMaxRange = characterStats.rangedAttackMaxRange;
            healAmount = characterStats.healAmount;
            healMaxRange = characterStats.healMaxRange;
            canHealOthers = characterStats.canHealOthers;
        }
        else
        {
            Debug.LogError("CharacterData not found at path: " + characterStatsPath);
        }
    }
    public Vector2 GetDesiredCell(BoardRules boardRules, BoardRules.Direction direction)
    {
        Vector2 desiredPosition = new Vector2();
        desiredPosition = boardRules.GetStepResult(gameObject, direction);
        return desiredPosition;
    }
    public IEnumerator SlideToPosition(Vector3 position, float moveSpeed)
    {
        float step = moveSpeed * Time.deltaTime;
        while (transform.position != position)
        {
            transform.position = Vector3.MoveTowards(transform.position, position, step);
            yield return null;
        }
    }
}
