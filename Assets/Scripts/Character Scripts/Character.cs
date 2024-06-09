using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using static UnityEngine.GraphicsBuffer;
using UnityEngine.UI;

public class Character : MonoBehaviour
{
    [Tooltip("The UI that displays the stats of the character.")]
    public GameObject characterDisplay;    
    public bool isMoving = false;
    public bool hasMoved = false;
    public bool hasActed = false;
    public string characterName;
    public int initialHealth;
    public int meleeAttackDamage;
    public int rangedAttackDamage;
    public int rangedAttackMaxRange;
    public int healAmount;
    public int healMaxRange;
    public event Action<float> DisplayedStatChanged;
    public SO_CharacterReaction CharacterReactionInfo;
    public SO_Character characterInfo;

    protected ICharacterMovement CharacterMovementInterface { get; set; }
    protected ICharacterAction CharacterActionInterface { get; set; }
    
    // YOU MADE STATS DISPLAYER PRIVATE AND WE DON?T KNOW WHY IT WAS PUBLIC
    private ChangingStatsDisplayer statsDisplayer;
    private float _movesLeft;
    private int speed;
    private int actions;
    private float _health;
    private float _actionsLeft;

    public float MovesLeft
    {
        get { return _movesLeft; }
        set 
        {
            if (_movesLeft != value)
            {
                _movesLeft = value;
                DisplayedStatChanged?.Invoke(_movesLeft);
            }
        }
    }
    public float Health
    {
        get { return _health; }
        set
        {
            if (value != _health)
            {
                _health = value;
                DisplayedStatChanged?.Invoke(_health);
            }
        }
    }
    public float ActionsLeft
    {
        get { return _actionsLeft; }
        set 
        { 
            if (_actionsLeft != value)
            {
                _actionsLeft = value;
                DisplayedStatChanged?.Invoke(_actionsLeft);
            }
        }
    }
    public void Initialize(string characterStatsPath)
    {
        statsDisplayer = GetComponent<ChangingStatsDisplayer>();
        characterInfo = Resources.Load<SO_Character>("ScriptableObjects/" + characterStatsPath);

        if (characterInfo != null)
        {
            characterName = characterInfo.characterName;
            initialHealth = characterInfo.initialHealth;
            _health = characterInfo.initialHealth;
            _movesLeft = characterInfo.speed;
            actions = characterInfo.actions;
            _actionsLeft = characterInfo.actions;
            speed = characterInfo.speed;
            meleeAttackDamage = characterInfo.meleeAttackDamage;
            rangedAttackDamage = characterInfo.rangedAttackDamage;
            rangedAttackMaxRange = characterInfo.rangedAttackMaxRange;
            healAmount = characterInfo.healAmount;
            healMaxRange = characterInfo.healMaxRange;

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
        _movesLeft = speed;
        hasActed = false;
        _actionsLeft = actions;
        statsDisplayer.UpdateStats(0);
    }    
    public void Die()
    {
        statsDisplayer.UpdateStats(false);
        CharacterTracker.Instance.activeCharacters.Remove(this);
        CharacterTracker.Instance.occupiedPositions.Remove(new Vector2(transform.position.x, transform.position.y));
        gameObject.SetActive(false);
    }

}
