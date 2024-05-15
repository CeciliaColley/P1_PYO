using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerBehaviour : MonoBehaviour
{
    public bool isHealer = false;
    public bool isFighter = false;
    public bool isRange = false;
    public int maxHealth;
    public int movements;
    public int meleeAttack;
    public int rangeAttack;
    public int heal;
    public bool canRangeAttack;
    public bool canHealOthers;
    public bool clickedPlayerIsAdyacent;
    public bool acted = false;
    public int currentHealth;

    [SerializeField] private PlayerActivator playerActivator;
    [SerializeField] private GameObject optionsPanel;
    [SerializeField] private GameObject statsText;
    [SerializeField] private string playerDisplay;
    [SerializeField] private string currentHealthDisplay;
    [SerializeField] private string movementsDisplay;

    private BoardInformation boardInformation;
    private GameObject activePlayer;
    


    void Awake()
    {
        playerActivator.activePlayers.Add(gameObject);
    }

    private void Start()
    {
        boardInformation = InformationRetriever.Instance.boardInformation;
        PositionPlayerRandomly();
        currentHealth = maxHealth;
        PrintStats();

    }

    public void PrintStats()
    {
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.Append(playerDisplay);
        stringBuilder.Append(currentHealthDisplay);
        stringBuilder.Append(currentHealth);
        stringBuilder.Append(movementsDisplay);
        stringBuilder.Append(movements);

        TextMeshProUGUI textMeshPro = statsText.GetComponent<TextMeshProUGUI>();
        if ( textMeshPro == null)
        {
            Debug.Log("Couldn't find a text mesh component.");
        }
        textMeshPro.text = stringBuilder.ToString();
    }

    public void InteractIfInRange()
    {
        InformationRetriever.Instance.clickedPlayer = gameObject;
        activePlayer = playerActivator.activePlayer;
        PlayerBehaviour activePlayerBehaviour = activePlayer.GetComponent<PlayerBehaviour>();

        if (optionsPanel.activeSelf)
        {
            optionsPanel.SetActive(false);
        }
        
        if (!(Vector3.Distance(transform.position, activePlayer.transform.position) > boardInformation.maxInteractionDistance))
        {
            optionsPanel.transform.position = Camera.main.WorldToScreenPoint(transform.position);
            clickedPlayerIsAdyacent = true;
            optionsPanel.SetActive(true);
        }
        else if (activePlayerBehaviour.canRangeAttack)
        {

            optionsPanel.transform.position = Camera.main.WorldToScreenPoint(transform.position);
            optionsPanel.SetActive(true);
        }
    }

    public void Heal(int healAmount)
    {
        currentHealth += healAmount;
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
        PrintStats();
    }

    public void RecieveDamage(int damageAmount)
    {
        currentHealth -= damageAmount;
        if (currentHealth <= 0)
        {
            currentHealth = 0;
            gameObject.SetActive(false);
        }
        PrintStats();
    }

    private void PositionPlayerRandomly()
    {
        Vector3 randomPosition = GenerateRandomPosition();
        while (IsPositionOccupied(randomPosition))
        {
            randomPosition = GenerateRandomPosition();
        }
        transform.position = randomPosition;
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

    public bool IsPositionOccupied(Vector3 position)
    {
        foreach (GameObject player in playerActivator.activePlayers)
        {
            if (player != gameObject && Vector3.Distance(player.transform.position, position) < 0.01f)
            {
                return true;
            }
        }
        return false;
    }
}
