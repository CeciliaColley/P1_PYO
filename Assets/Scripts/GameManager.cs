using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System.Text;
using TMPro;

public class GameManager
    : MonoBehaviour
{
    [SerializeField] private PlayerActivator playerActivator;
    [SerializeField] private GameObject activePlayerBox;
    [SerializeField] private GameObject activePlayerTitle;
    [SerializeField] private GameObject endScreen;
    [SerializeField] private GameObject winText;
    [SerializeField] private GameObject loseText;

    public bool isGameOver = false;
    public bool hasWon = false;
    public string boardSOName;
    public string rangeSOName;
    public string healerSOName;
    public string fighterSOName;
    public string enemySOName;
    public BoardInformation boardInformation;
    public RangeInformation rangeInformation;
    public HealerInformation healerInformation;
    public FighterInformation fighterInformation;
    public EnemyInformation enemyInformation;
    public GameObject clickedPlayer;
    public static GameManager Instance;

    private CharacterBehaviour playerBehaviour;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        GetInformation(ref boardInformation, boardSOName);
        boardInformation.maxInteractionDistance = Mathf.Sqrt(boardInformation.playerStepLength * boardInformation.playerStepLength + boardInformation.playerStepLength * boardInformation.playerStepLength);
        GetInformation(ref rangeInformation, rangeSOName);
        GetInformation(ref healerInformation, healerSOName);
        GetInformation(ref fighterInformation, fighterSOName);
        GetInformation(ref enemyInformation, enemySOName);
    }

    private void Start()
    {
        StartCoroutine(WaitForActivePlayer());
    }

    private IEnumerator WaitForActivePlayer()
    {
        yield return new WaitUntil(() => playerActivator.activePlayer != null);
        playerBehaviour = playerActivator.activePlayer.GetComponent<CharacterBehaviour>();
        activePlayerBox.transform.position = playerBehaviour.characterCanvas.transform.position;
        TextMeshProUGUI textMeshPro = activePlayerTitle.GetComponent<TextMeshProUGUI>();
        textMeshPro.text = playerBehaviour.characterName;
    }

    public void GetInformation<T>(ref T information, string resourceName) where T : ScriptableObject
    {
        if (information != null)
        {
            Destroy(information);
        }
        var reference = Resources.Load<T>("Scriptable Objects/" + resourceName);
        information = reference;
    }

    public void EndTurn()
    {
        MoveDownActivePlayerList();
        UpdateUI();

        if (!playerBehaviour.isHealer && !playerBehaviour.isFighter && !playerBehaviour.isRange)
        {
            EnemyTurn(playerBehaviour);
        }

        if (isGameOver && hasWon)
        {
            endScreen.SetActive(true);
            loseText.SetActive(false);
            winText.SetActive(true);
        }
        if (isGameOver && !hasWon)
        {
            endScreen.SetActive(true);
            loseText.SetActive(true);
            winText.SetActive(false);
        }
    }

    private void UpdateUI()
    {
        playerBehaviour = playerActivator.activePlayer.GetComponent<CharacterBehaviour>();
        activePlayerBox.transform.position = playerBehaviour.characterCanvas.transform.position;
        TextMeshProUGUI textMeshPro = activePlayerTitle.GetComponent<TextMeshProUGUI>();
        textMeshPro.text = playerBehaviour.characterName;
    }

    private void MoveDownActivePlayerList()
    {
        int playerIndex = playerActivator.activePlayerIndex++;
        if (playerIndex < playerActivator.activePlayers.Count)
        {
            playerActivator.activePlayer = playerActivator.activePlayers[playerIndex];
        }
        else
        {
            playerActivator.activePlayer = playerActivator.activePlayers.First();
            playerActivator.activePlayerIndex = 1;
        }
    }

    private void EnemyTurn(CharacterBehaviour enemyBehaviour)
    {
        // Find the closest non-enemy character
        GameObject closestNonEnemy = FindClosestNonEnemy();

        if (closestNonEnemy != null)
        {
            // Move towards the closest non-enemy character
            Vector2 direction = (closestNonEnemy.transform.position - enemyBehaviour.transform.position).normalized;


            // Attack if in range
            if (Vector3.Distance(enemyBehaviour.transform.position, closestNonEnemy.transform.position) <= 1.5f)
            {
                CharacterBehaviour targetBehaviour = closestNonEnemy.GetComponent<CharacterBehaviour>();
                targetBehaviour.RecieveDamage(enemyBehaviour.meleeAttack);
            }
        }

        // End the turn
        EndTurn();
    }

    private GameObject FindClosestNonEnemy()
    {
        GameObject closest = null;
        float minDistance = float.MaxValue;

        foreach (GameObject player in playerActivator.activePlayers)
        {
            CharacterBehaviour playerBehaviour = player.GetComponent<CharacterBehaviour>();
            if (playerBehaviour.isHealer || playerBehaviour.isFighter || playerBehaviour.isRange)
            {
                float distance = Vector3.Distance(player.transform.position, playerActivator.activePlayer.transform.position);
                if (distance < minDistance)
                {
                    minDistance = distance;
                    closest = player;
                }
            }
        }

        return closest;
    }
}
