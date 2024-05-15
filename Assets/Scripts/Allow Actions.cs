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

    private void OnEnable()
    {
        activePlayer = playerActivator.activePlayer;
        playerBehaviour = activePlayer.GetComponent<PlayerBehaviour>();
        PlayerBehaviour clickedPlayerBehaviour = InformationRetriever.Instance.clickedPlayer.GetComponent<PlayerBehaviour>();
        
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
        activePlayer = playerActivator.activePlayer;
        PlayerBehaviour activePlayerBehaviour = activePlayer.GetComponent<PlayerBehaviour>();
        PlayerBehaviour clickedPlayerBehaviour = InformationRetriever.Instance.clickedPlayer.GetComponent<PlayerBehaviour>();
        clickedPlayerBehaviour.currentHealth += activePlayerBehaviour.heal;
        if (clickedPlayerBehaviour.currentHealth > clickedPlayerBehaviour.maxHealth)
        {
            clickedPlayerBehaviour.currentHealth = clickedPlayerBehaviour.maxHealth;
        }
        gameObject.SetActive(false);
    }

    public void MeleeAttack()
    {
        activePlayer = playerActivator.activePlayer;
        PlayerBehaviour activePlayerBehaviour = activePlayer.GetComponent<PlayerBehaviour>();
        PlayerBehaviour clickedPlayerBehaviour = InformationRetriever.Instance.clickedPlayer.GetComponent<PlayerBehaviour>();
        clickedPlayerBehaviour.currentHealth -= activePlayerBehaviour.meleeAttack;
        if (clickedPlayerBehaviour.currentHealth <= 0)
        {
            clickedPlayerBehaviour.currentHealth = 0;
            InformationRetriever.Instance.clickedPlayer.SetActive(false);
        }
        gameObject.SetActive(false);
    }

    public void RangeAttack()
    {
        activePlayer = playerActivator.activePlayer;
        PlayerBehaviour activePlayerBehaviour = activePlayer.GetComponent<PlayerBehaviour>();
        PlayerBehaviour clickedPlayerBehaviour = InformationRetriever.Instance.clickedPlayer.GetComponent<PlayerBehaviour>();
        clickedPlayerBehaviour.currentHealth -= activePlayerBehaviour.rangeAttack;
        if (clickedPlayerBehaviour.currentHealth <= 0)
        {
            clickedPlayerBehaviour.currentHealth = 0;
            InformationRetriever.Instance.clickedPlayer.SetActive(false);
        }
        gameObject.SetActive(false);
    }
}
