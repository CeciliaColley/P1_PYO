using System;
using UnityEngine;
using System.Collections;
using Unity.VisualScripting;
using static UnityEngine.GraphicsBuffer;
using UnityEngine.UI;

public class Character : MonoBehaviour
{
    [Tooltip("The UI that displays the stats of the character.")]
    public RectTransform characterDisplay;
    public string characterName;
    public int movesLeft;
    public float health;
    public bool isMoving = false;
    public bool hasMoved = false;
    public bool hasActed = false;
    protected ICharacterMovement characterMovement { get; set; }
    protected ICharacterAction characterAction { get; set; }
    
    private int speed;
    private int initialHealth;
    private int meleeAttackDamage;
    private int rangedAttackDamage;
    private int rangedAttackMaxRange;
    private int healAmount;
    private int healMaxRange;

    public void Initialize(string characterStatsPath)
    {
        SO_Character characterStats = Resources.Load<SO_Character>("ScriptableObjects/" + characterStatsPath);

        if (characterStats != null)
        {
            characterName = characterStats.characterName;
            initialHealth = characterStats.initialHealth;
            health = characterStats.initialHealth;
            movesLeft = characterStats.speed;
            speed = characterStats.speed;
            meleeAttackDamage = characterStats.meleeAttackDamage;
            rangedAttackDamage = characterStats.rangedAttackDamage;
            rangedAttackMaxRange = characterStats.rangedAttackMaxRange;
            healAmount = characterStats.healAmount;
            healMaxRange = characterStats.healMaxRange;
        }
        else
        {
            Debug.LogError("CharacterData not found at path: " + characterStatsPath);
        }
    }
    public void Move()
    {
        if (characterMovement != null)
        {
            characterMovement.Move(this);
        }
    }
    public void Act()
    {
        if (characterAction != null)
        {
            characterAction.Act(this);
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
            isMoving = true;
            movesLeft--;
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
    public void ResetCharacter()
    {
        hasMoved = false;
        movesLeft = speed;
        hasActed = false;
    }
    public void RangeAttack(Character target)
    {
        target.health -= rangedAttackDamage;
        CheckForKill(target);
        
    }
    public void MeleeAttack(Character target)
    {
        target.health -= meleeAttackDamage;
        CheckForKill(target);

    }
    public void CheckForKill(Character target)
    {
        if (target.health <= 0)
        {
            target.Die();
        }
    }
    public void Heal(Character target)
    {
        target.health += healAmount;
        if (health > initialHealth)
        {
            health = initialHealth;
        }
    }
    public bool CanRangeAttack(Character target)
    {
        if ((target != this) &
            (rangedAttackDamage > 0) &&
            BoardRules.Instance.IsInDiamondRange(this, target, rangedAttackMaxRange) &&
            !BoardRules.Instance.IsInSquareRange(this, target, 1))
        {
            return true;
        }
        else return false;
    }
    public bool CanMeleeAttack(Character target)
    {
        if ((target != this) &&
            (meleeAttackDamage > 0) &&
            BoardRules.Instance.IsInSquareRange(this, target, 1))
        {
            return true;
        }
        else return false;
    }
    public bool CanHeal(Character target)
    {
        if ((healAmount > 0 && target == this) ||
            (healMaxRange > 0 &&
            healAmount > 0 &&
            BoardRules.Instance.IsInDiamondRange(this, target, healMaxRange)))
        {
            return true;
        }
        else return false;
        
    }
    public void Die()
    {
        CharacterTracker.Instance.activeCharacters.Remove(this);
        CharacterTracker.Instance.occupiedPositions.Remove(new Vector2(transform.position.x, transform.position.y));
        gameObject.SetActive(false);
    }

}
