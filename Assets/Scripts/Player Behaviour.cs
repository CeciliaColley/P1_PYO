using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    [SerializeField] private PlayerActivator gameManager;
    private BoardInformation boardInformation;

    void Awake()
    {
        gameManager.activePlayers.Add(gameObject);
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
        foreach (GameObject player in gameManager.activePlayers)
        {
            if (player != gameObject && Vector3.Distance(player.transform.position, position) < 0.01f)
            {
                return true;
            }
        }
        return false;
    }
}
