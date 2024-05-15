using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AllowActions : MonoBehaviour
{
    [SerializeField] private Button rangeAttackButton;
    [SerializeField] private Button meleeAttackButton;
    [SerializeField] private Button healButton;
    [SerializeField] private PlayerActivator playerActivator;

    private GameObject activePlayer;
    private PlayerBehaviour playerBehaviour;
    private PlayerBehaviour clickedPlayerBehaviour;

    private void OnEnable()
    {
        activePlayer = playerActivator.activePlayer;
        playerBehaviour = activePlayer.GetComponent<PlayerBehaviour>();
        clickedPlayerBehaviour = InformationRetriever.Instance.clickedPlayer.GetComponent<PlayerBehaviour>();
        
        healButton.interactable = true;
        meleeAttackButton.interactable = true;
        rangeAttackButton.interactable = true;

        if (!playerBehaviour.canHealOthers)
        {
            healButton.interactable = false;
        }

        if (activePlayer == InformationRetriever.Instance.clickedPlayer)
        {
            meleeAttackButton.interactable = false;
            rangeAttackButton.interactable = false;
            healButton.interactable = true;
        }
        
        if (!playerBehaviour.canRangeAttack)
        {
            rangeAttackButton.interactable = false;
        }

        if (!clickedPlayerBehaviour.clickedPlayerIsAdyacent)
        {
            meleeAttackButton.interactable = false;
        }
    }

    public void Heal()
    {
        clickedPlayerBehaviour.Heal(playerBehaviour.heal);
        playerBehaviour.acted = true;
        if (playerBehaviour.movements <= 0)
        {
            InformationRetriever.Instance.EndTurn();
        }
        gameObject.SetActive(false);
    }

    public void MeleeAttack()
    {
        clickedPlayerBehaviour.RecieveDamage(playerBehaviour.meleeAttack);
        playerBehaviour.acted = true;
        if (playerBehaviour.movements <= 0)
        {
            InformationRetriever.Instance.EndTurn();
        }
        gameObject.SetActive(false);
    }

    public void RangeAttack()
    {
        clickedPlayerBehaviour.RecieveDamage(playerBehaviour.rangeAttack);
        playerBehaviour.acted = true;
        if (playerBehaviour.movements <= 0)
        {
            InformationRetriever.Instance.EndTurn();
        }
        
        gameObject.SetActive(false);
    }
}
