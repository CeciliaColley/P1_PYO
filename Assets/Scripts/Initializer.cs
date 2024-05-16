using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

// This script is in charge of handling the set up of the game, which is in line with the "Single Responsibility Principle" of solid code.
// To achieve this task it does two things: 
// Initialize players variables using data from scriptable objects.
// Place the players in random unique positions.

// Dependencies:
// GameManager: this is where the script obtains information regarding the board and the different player types.
// PlayerActivator: this is where the script can reference the players that it needs to initialize.

public class Initializer : MonoBehaviour
{
    private PlayerActivator playerActivator;
    private BoardInformation boardInformation;

    private void Start()
    {
        playerActivator = GetComponent<PlayerActivator>();
        boardInformation = GameManager.Instance.boardInformation;
        InitializeCharacters();
        PositionPlayersRandomly();
    }

    // Pass the variables from the scriptable objects to each player type as coresponds
    private void InitializeCharacters()
    {
        foreach (GameObject player in playerActivator.activePlayers)
        {
            CharacterBehaviour playerBehaviour = player.GetComponent<CharacterBehaviour>();
            if (playerBehaviour.canHealOthers && playerBehaviour.canRangeAttack)
            {
                HealerInformation healerInformation = GameManager.Instance.healerInformation;
                SetVariables(playerBehaviour, healerInformation);
            }
            else if (!playerBehaviour.canHealOthers && !playerBehaviour.canRangeAttack)
            {
                FighterInformation fighterInformation = GameManager.Instance.fighterInformation;
                SetVariables(playerBehaviour, fighterInformation);
            }
            else if (playerBehaviour.isRange)
            {
                RangeInformation rangeInformation = GameManager.Instance.rangeInformation;
                SetVariables(playerBehaviour, rangeInformation);
            }
            else
            {
                EnemyInformation enemyInformation = GameManager.Instance.enemyInformation;
                SetVariables(playerBehaviour, enemyInformation);
            }
        }
    }

    // Created a method with four different overloads for populating each of the four player types variables.
    public void SetVariables(CharacterBehaviour playerBehaviour, HealerInformation healerInformation)
    {
        playerBehaviour.maxHealth = healerInformation.health;
        playerBehaviour.maxMovements = healerInformation.movements;
        playerBehaviour.movements = healerInformation.movements;
        playerBehaviour.maxActions = healerInformation.maxActions;
        playerBehaviour.meleeAttack = healerInformation.meleeAttack;
        playerBehaviour.heal = healerInformation.heal;
        playerBehaviour.canHealOthers = healerInformation.canCureOthers;
        playerBehaviour.rangeAttack = healerInformation.rangeAttack;
    }

    public void SetVariables(CharacterBehaviour playerBehaviour, RangeInformation rangeInformation)
    {
        playerBehaviour.maxHealth = rangeInformation.health;
        playerBehaviour.maxMovements = rangeInformation.movements;
        playerBehaviour.movements = rangeInformation.movements;
        playerBehaviour.maxActions = rangeInformation.maxActions;
        playerBehaviour.meleeAttack = rangeInformation.meleeAttack;
        playerBehaviour.heal = rangeInformation.heal;
        playerBehaviour.canHealOthers = rangeInformation.canCureOthers;
        playerBehaviour.rangeAttack = rangeInformation.rangeAttack;
    }

    public void SetVariables(CharacterBehaviour playerBehaviour, FighterInformation fighterInformation)
    {
        playerBehaviour.maxHealth = fighterInformation.health;
        playerBehaviour.maxMovements = fighterInformation.movements;
        playerBehaviour.movements = fighterInformation.movements;
        playerBehaviour.maxActions = fighterInformation.maxActions;
        playerBehaviour.meleeAttack = fighterInformation.meleeAttack;
        playerBehaviour.heal = fighterInformation.heal;
        playerBehaviour.canHealOthers = fighterInformation.canCureOthers;
    }

    public void SetVariables(CharacterBehaviour playerBehaviour, EnemyInformation enemyInformation)
    {
        playerBehaviour.maxHealth = enemyInformation.health;
        playerBehaviour.maxMovements = enemyInformation.movements;
        playerBehaviour.maxActions = enemyInformation.maxActions;
        playerBehaviour.meleeAttack = enemyInformation.meleeAttack;
    }

    // Logic to initialize players at random positions

    private void PositionPlayersRandomly()
    {
        int numPlayers = playerActivator.activePlayers.Count;
        List<Vector3> randomPositions = GenerateUniqueRandomPositions(numPlayers);
        for (int i = 0; i < numPlayers; i++)
        {
            GameObject player = playerActivator.activePlayers[i];
            player.transform.position = randomPositions[i];
        }
    }

    private List<Vector3> GenerateUniqueRandomPositions(int count)
    {
        List<Vector3> uniquePositions = new List<Vector3>();
        for (int i = 0; i < count; i++)
        {
            Vector3 randomPosition = GenerateRandomPosition();
            while (uniquePositions.Contains(randomPosition))
            {
                randomPosition = GenerateRandomPosition();
            }
            uniquePositions.Add(randomPosition);
        }
        return uniquePositions;
    }

    private Vector3 GenerateRandomPosition()
    {
        int randomX = Random.Range(0, boardInformation.boardWidth) * boardInformation.playerStepLength;
        int randomY = Random.Range(0, boardInformation.boardHeight) * boardInformation.playerStepLength;
        randomX += boardInformation.leftmostTilesX;
        randomY += boardInformation.lowestTilesY;
        Vector3 randomPosition = new Vector3(randomX, randomY, 0);
        return randomPosition;
    }
}

