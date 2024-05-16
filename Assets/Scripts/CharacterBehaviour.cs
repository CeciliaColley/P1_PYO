using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Transactions;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

// With optimization in mind, all player characters, including enemies share the same Behaviour as a "base behaviour".
// In line with "O" from solid code, the script is open to be added onto, but protected from modification. You can couple it with either PlayerActions, for players, or EnemyActions for enemies.

public class CharacterBehaviour : MonoBehaviour
{
    // Variables initialized by the scriptable object;
    public int maxHealth;
    public int maxMovements;
    public int maxActions;
    public int movements;
    public int meleeAttack;
    public int rangeAttack;
    public int heal;
    public bool canRangeAttack;
    public bool canHealOthers;
    
    // Variables shared for interactions betweens characters
    [SerializeField] public GameObject characterCanvas;
    [SerializeField] public bool clickedCharacterIsAdyacent;
    [SerializeField] public int currentHealth;
    [SerializeField] public int actionsLeft;
    [SerializeField] public string characterName;

    // Private variables for self management
    [SerializeField] public PlayerActivator playerActivator;
    [SerializeField] private GameObject optionsPanel;
    [SerializeField] private GameObject statsText;
    [SerializeField] private string currentHealthDisplay;
    [SerializeField] private string movementsDisplay;

    public bool isHealer = false;
    public bool isFighter = false;
    public bool isRange = false;


    // Players add themsleves to the active players list when the game powers up.
    // Initialization happens in Start, and requires the active player list to already be populated
    void Awake()
    {
        playerActivator.activePlayers.Add(gameObject);
    }

    private void Start()
    {
        InitializeProtectedVariables();
        PrintStats();
    }

    private void InitializeProtectedVariables()
    {
        currentHealth = maxHealth;
        actionsLeft = maxActions;
    }

    public void PrintStats()
    {
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.Append(characterName);
        stringBuilder.Append(currentHealthDisplay);
        stringBuilder.Append(currentHealth);
        stringBuilder.Append(movementsDisplay);
        stringBuilder.Append(movements);

        TextMeshProUGUI textMeshPro = statsText.GetComponent<TextMeshProUGUI>();
        textMeshPro.text = stringBuilder.ToString();
    }

    public void OnInteractedWith()
    {
        GameManager.Instance.clickedPlayer = gameObject;
        
        GameObject activePlayer = playerActivator.activePlayer;
        CharacterBehaviour activePlayerBehaviour = activePlayer.GetComponent<CharacterBehaviour>();
        if (activePlayerBehaviour.actionsLeft < 0) { return; }
        if (optionsPanel.activeSelf) { optionsPanel.SetActive(false); }

        DisplayPossibleActionsIfInRange(activePlayer, activePlayerBehaviour);
    }

    private void DisplayPossibleActionsIfInRange(GameObject activePlayer, CharacterBehaviour activePlayerBehaviour)
    {
        BoardInformation boardInformation = GameManager.Instance.boardInformation; ;
        if ((Vector3.Distance(transform.position, activePlayer.transform.position) <= boardInformation.maxInteractionDistance))
        {
            ShowActionOptions(activePlayerBehaviour, true);
        }
        else if (activePlayerBehaviour.canRangeAttack)
        {
            ShowActionOptions(activePlayerBehaviour, false);
        }
    }

    private void ShowActionOptions(CharacterBehaviour activePlayerBehaviour, bool clickedPlayerIsAdyacent)
    {
        optionsPanel.transform.position = Camera.main.WorldToScreenPoint(transform.position);
        activePlayerBehaviour.clickedCharacterIsAdyacent = clickedPlayerIsAdyacent;
        optionsPanel.SetActive(true);
    }

    public void Recuperate(int healAmount)
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
            playerActivator.activePlayers.Remove(gameObject);
            gameObject.SetActive(false);
            CheckAndUpdateGameState();
        }
        PrintStats();
    }

    private void CheckAndUpdateGameState()
    {
        bool enemiesActive = false;
    
        if (playerActivator.activePlayers.Any(player => player.GetComponent<EnemyBehaviour>() != null))
        {
            enemiesActive = true;
        }
        if (playerActivator.activePlayers.Count == 1)
        {
            EndGame(true);
        }
        else if ((isHealer || isFighter || isRange) && enemiesActive)
        {
            EndGame(false);
        }
    }

    private static void EndGame(bool hasWon)
    {
        GameManager.Instance.isGameOver = true;
        GameManager.Instance.hasWon = hasWon;
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
