using System;
using UnityEngine;
using System.Collections;


public class Character : MonoBehaviour
{
    public int speed;
    protected string characterName;
    protected int initialHealth;
    protected int meleeAttackDamage;
    protected int rangedAttackDamage;
    protected int rangedAttackMaxRange;
    protected int healAmount;
    protected int healMaxRange;
    protected bool canHealOthers;
    protected ICharacterMovement characterMovement { get; set; }

    public bool isMoving = false;

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

    public void MoveTheCharacter(Vector2 desiredCell, float moveSpeed)
    {
        Vector2 currentPosition = new Vector2(transform.position.x, transform.position.y);
        GameManager.Instance.occupiedPositions.Remove(currentPosition);
        GameManager.Instance.occupiedPositions.Add(desiredCell);

        Vector3 newPosition = new Vector3(desiredCell.x, desiredCell.y, 0);
        StartCoroutine(SlideToPosition(newPosition, moveSpeed));
    }
    public Vector2 GetDesiredCell(BoardRules.Direction direction)
    {
        Vector2 desiredPosition = new Vector2();
        desiredPosition = BoardRules.Instance.GetStepResult(gameObject, direction);
        return desiredPosition;
    }
    public IEnumerator SlideToPosition(Vector3 position, float moveSpeed)
    {
        if (isMoving == false)
        {
            isMoving = true;
            float step = moveSpeed * Time.deltaTime;
            while (transform.position != position)
            {
                transform.position = Vector3.MoveTowards(transform.position, position, step);
                yield return null;
            }
            isMoving = false;
        }
    }


    public void Move()
    {
        if (characterMovement != null)
        {
            characterMovement.Move(this);
        }
    }
}
