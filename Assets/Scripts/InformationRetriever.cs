using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class InformationRetriever : MonoBehaviour
{
    [SerializeField] private PlayerActivator playerActivator;

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
        }
    }
}
