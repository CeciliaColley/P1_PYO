using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System.Text;
using TMPro;

public class InformationRetriever : MonoBehaviour
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
    public static InformationRetriever Instance;

    private PlayerBehaviour playerBehaviour;

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
        boardInformation.maxInteractionDistance =  Mathf.Sqrt(boardInformation.playerStepLength * boardInformation.playerStepLength + boardInformation.playerStepLength * boardInformation.playerStepLength);
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
        yield return new WaitUntil(()=>playerActivator.activePlayer != null);
        playerBehaviour = playerActivator.activePlayer.GetComponent<PlayerBehaviour>();
        activePlayerBox.transform.position = playerBehaviour.canvas.transform.position;
        TextMeshProUGUI textMeshPro = activePlayerTitle.GetComponent<TextMeshProUGUI>();
        textMeshPro.text = playerBehaviour.playerDisplay;
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
        playerBehaviour = playerActivator.activePlayer.GetComponent<PlayerBehaviour>();
        activePlayerBox.transform.position = playerBehaviour.canvas.transform.position;
        TextMeshProUGUI textMeshPro = activePlayerTitle.GetComponent<TextMeshProUGUI>();
        textMeshPro.text = playerBehaviour.playerDisplay;

        foreach (GameObject player in playerActivator.activePlayers)
        {
            PlayerBehaviour playerBehaviour = player.GetComponent<PlayerBehaviour>();
            playerBehaviour.movements = playerBehaviour.maxMovements;
            playerBehaviour.acted = false;
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
}
