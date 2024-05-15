using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Initializer : MonoBehaviour
{
    private PlayerActivator playerActivator;

    private void Start()
    {
        playerActivator = GetComponent<PlayerActivator>();
        InitializeCharacters();
    }

    private void InitializeCharacters()
    {
        foreach (GameObject player in playerActivator.activePlayers)
        {
            PlayerBehaviour playerBehaviour = player.GetComponent<PlayerBehaviour>();
            if (playerBehaviour.isHealer)
            {
                HealerInformation healerInformation = InformationRetriever.Instance.healerInformation;
                SetVariables(playerBehaviour, healerInformation);
            }
            else if (playerBehaviour.isFighter)
            {
                FighterInformation fighterInformation = InformationRetriever.Instance.fighterInformation;
                SetVariables(playerBehaviour, fighterInformation);
            }
            else if (playerBehaviour.isRange)
            {
                RangeInformation rangeInformation = InformationRetriever.Instance.rangeInformation;
                SetVariables(playerBehaviour, rangeInformation);
            }
            else
            {
                EnemyInformation enemyInformation = InformationRetriever.Instance.enemyInformation;
                SetVariables(playerBehaviour, enemyInformation);
            }
        }
    }

    public void SetVariables(PlayerBehaviour playerBehaviour, HealerInformation healerInformation)
    {
        playerBehaviour.health = healerInformation.health;
        playerBehaviour.movements = healerInformation.movements;
        playerBehaviour.meleeAttack = healerInformation.meleeAttack;
        playerBehaviour.heal = healerInformation.heal;
        playerBehaviour.canCureOthers = healerInformation.canCureOthers;
        playerBehaviour.rangeAttack = healerInformation.rangeAttack;
        playerBehaviour.canRangeAttack = true;
    }

    public void SetVariables(PlayerBehaviour playerBehaviour, RangeInformation rangeInformation)
    {
        playerBehaviour.health = rangeInformation.health;
        playerBehaviour.movements = rangeInformation.movements;
        playerBehaviour.meleeAttack = rangeInformation.meleeAttack;
        playerBehaviour.heal = rangeInformation.heal;
        playerBehaviour.canCureOthers = rangeInformation.canCureOthers;
        playerBehaviour.rangeAttack = rangeInformation.rangeAttack;
        playerBehaviour.canRangeAttack = true;
    }

    public void SetVariables(PlayerBehaviour playerBehaviour, FighterInformation fighterInformation)
    {
        playerBehaviour.health = fighterInformation.health;
        playerBehaviour.movements = fighterInformation.movements;
        playerBehaviour.meleeAttack = fighterInformation.meleeAttack;
        playerBehaviour.heal = fighterInformation.heal;
        playerBehaviour.canCureOthers = fighterInformation.canCureOthers;
    }

    public void SetVariables(PlayerBehaviour playerBehaviour, EnemyInformation enemyInformation)
    {
        playerBehaviour.health = enemyInformation.health;
        playerBehaviour.movements = enemyInformation.movements;
        playerBehaviour.meleeAttack = enemyInformation.meleeAttack;
    }
}

