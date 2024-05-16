using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlayerActions : MonoBehaviour
{
    [SerializeField] private Button rangeAttackButton;
    [SerializeField] private Button meleeAttackButton;
    [SerializeField] private Button healButton;
    [SerializeField] private PlayerActivator playerActivator;

    private CharacterBehaviour activePlayerCharacterBehaviour;
    private CharacterBehaviour clickedPlayerCharacterBehaviour;
    private GameObject activePlayer;

    private void OnEnable()
    {
        activePlayer = playerActivator.activePlayer;
        activePlayerCharacterBehaviour = activePlayer.GetComponent<CharacterBehaviour>();
        clickedPlayerCharacterBehaviour = GameManager.Instance.clickedPlayer.GetComponent<CharacterBehaviour>();

        healButton.interactable = true;
        meleeAttackButton.interactable = true;
        rangeAttackButton.interactable = true;

        if (activePlayerCharacterBehaviour.isFighter)
        {
            healButton.interactable = false;
            rangeAttackButton.interactable = false;
            return;
        }
        else if (activePlayerCharacterBehaviour.isRange)
        {
            healButton.interactable = false;
        }

        if (activePlayer == GameManager.Instance.clickedPlayer)
        {
            meleeAttackButton.interactable = false;
            rangeAttackButton.interactable = false;
            healButton.interactable = true;
        };

        if (IsInRange())
        {
            rangeAttackButton.interactable = false;
        }
        else if (!IsInRange())
        {
            meleeAttackButton.interactable = false;
        }
    }



    public bool IsInRange()
    {
        float maxDistance = GameManager.Instance.boardInformation.maxInteractionDistance;
        float distance = Vector3.Distance(GameManager.Instance.clickedPlayer.transform.position, activePlayer.transform.position);
        return distance <= maxDistance;
    }

    public void Heal()
    {
        clickedPlayerCharacterBehaviour.Recuperate(activePlayerCharacterBehaviour.heal);
        activePlayerCharacterBehaviour.actionsLeft --;
        if (activePlayerCharacterBehaviour.movements <= 0)
        {
            GameManager.Instance.EndTurn();
        }
        gameObject.SetActive(false);
    }

    public void MeleeAttack()
    {
        clickedPlayerCharacterBehaviour.RecieveDamage(activePlayerCharacterBehaviour.meleeAttack);
        activePlayerCharacterBehaviour.actionsLeft--;
        if (activePlayerCharacterBehaviour.movements <= 0)
        {
            GameManager.Instance.EndTurn();
        }
        gameObject.SetActive(false);
    }

    public void RangeAttack()
    {
        clickedPlayerCharacterBehaviour.RecieveDamage(activePlayerCharacterBehaviour.rangeAttack);
        activePlayerCharacterBehaviour.actionsLeft--;
        if (activePlayerCharacterBehaviour.movements <= 0)
        {
            GameManager.Instance.EndTurn();
        }
        
        gameObject.SetActive(false);
    }
}
