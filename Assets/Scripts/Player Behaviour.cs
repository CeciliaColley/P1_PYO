using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    public bool isHealer = false;
    public bool isFighter = false;
    public bool isRange = false;
    
    [SerializeField] private PlayerActivator playerActivator;
    
    private BoardInformation boardInformation;
    public int health;
    public int movements;
    public int meleeAttack;
    public int rangeAttack;
    public int heal;
    public bool canRangeAttack;
    public bool canCureOthers;

    void Awake()
    {
        playerActivator.activePlayers.Add(gameObject);
    }

    private void Start()
    {
        boardInformation = InformationRetriever.Instance.boardInformation;
        PositionPlayerRandomly();

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
