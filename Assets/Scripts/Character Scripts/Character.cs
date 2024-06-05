using System;
using UnityEngine;
using System.Collections;


public class Character : MonoBehaviour
{
    public bool isMoving = false;
    public bool hasMoved = false;
    public int movesLeft;
    protected ICharacterMovement characterMovement { get; set; }
    
    //Make this protected maybe?
    private int speed;
    private string characterName;
    private int initialHealth;
    private int meleeAttackDamage;
    private int rangedAttackDamage;
    private int rangedAttackMaxRange;
    private int healAmount;
    private int healMaxRange;
    private bool canHealOthers;


    public void Initialize(string characterStatsPath)
    {
        SO_Character characterStats = Resources.Load<SO_Character>("ScriptableObjects/" + characterStatsPath);

        if (characterStats != null)
        {
            characterName = characterStats.characterName;
            initialHealth = characterStats.initialHealth;
            movesLeft = characterStats.speed;
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
        CharacterTracker.Instance.occupiedPositions.Remove(currentPosition);
        CharacterTracker.Instance.occupiedPositions.Add(desiredCell);

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
            movesLeft--;
            isMoving = true;
            float step = moveSpeed * Time.deltaTime;
            while (transform.position != position)
            {
                transform.position = Vector3.MoveTowards(transform.position, position, step);
                yield return null;
            }
            isMoving = false;
            if (movesLeft <= 0) { hasMoved = true; }
        }
    }
    public void Move()
    {
        if (characterMovement != null)
        {
            characterMovement.Move(this);
        }
    }

    public void ResetCharacter()
    {
        hasMoved = false;
        movesLeft = speed;
    }
}
