using System;
using UnityEngine;
using System.Collections;
using Unity.VisualScripting;
using static UnityEngine.GraphicsBuffer;
using UnityEngine.UI;

public class Character : MonoBehaviour
{
    [Tooltip("The UI that displays the stats of the character.")]
    public GameObject characterDisplay;
    
    public StatsDisplayer statsDisplayer;
    public string characterName;
    public int movesLeft;
    public float health;
    public float actionsLeft;
    public bool isMoving = false;
    public bool hasMoved = false;
    public bool hasActed = false;
    public int initialHealth;
    public int meleeAttackDamage;
    public int rangedAttackDamage;
    public int rangedAttackMaxRange;
    public int healAmount;
    public int healMaxRange;
    protected ICharacterMovement CharacterMovementInterface { get; set; }
    protected ICharacterAction CharacterActionInterface { get; set; }
    private int speed;
    private int actions;

    public void Initialize(string characterStatsPath)
    {
        statsDisplayer = GetComponent<StatsDisplayer>();
        SO_Character characterStats = Resources.Load<SO_Character>("ScriptableObjects/" + characterStatsPath);

        if (characterStats != null)
        {
            characterName = characterStats.characterName;
            initialHealth = characterStats.initialHealth;
            health = characterStats.initialHealth;
            movesLeft = characterStats.speed;
            actions = characterStats.actions;
            actionsLeft = characterStats.actions;
            speed = characterStats.speed;
            meleeAttackDamage = characterStats.meleeAttackDamage;
            rangedAttackDamage = characterStats.rangedAttackDamage;
            rangedAttackMaxRange = characterStats.rangedAttackMaxRange;
            healAmount = characterStats.healAmount;
            healMaxRange = characterStats.healMaxRange;
        }
    }
    public void Move()
    {
        if (CharacterMovementInterface != null)
        {
            CharacterMovementInterface.Move(this);
        }
    }
    public void Act()
    {
        if (CharacterActionInterface != null)
        {
            CharacterActionInterface.Act(this);
        }
    }    
    public void ResetCharacter()
    {
        hasMoved = false;
        movesLeft = speed;
        hasActed = false;
        actionsLeft = actions;
    }    
    public void Die()
    {
        statsDisplayer.UpdateStats(false);
        CharacterTracker.Instance.activeCharacters.Remove(this);
        CharacterTracker.Instance.occupiedPositions.Remove(new Vector2(transform.position.x, transform.position.y));
        gameObject.SetActive(false);
    }

}
