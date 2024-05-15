using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject Fighter;
    [SerializeField] private GameObject Range;
    [SerializeField] private GameObject Healer;
    [SerializeField] private GameObject Enemy1;
    [SerializeField] private GameObject Enemy2;

    private List<Vector3> playerPositions = new List<Vector3>();

    public GameObject activePlayer;


    private enum playerTurn
    {
        Fighter,
        Range, 
        Healer, 
        Enemy1, 
        Enemy2,
        LAST
    }
    private playerTurn currentPlayer;

    private void Start()
    {
        
    }

    private void PositionPlayersRandomly()
    {
        
    }
}
