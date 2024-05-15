using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    [SerializeField] private string boardInformationSOName;

    private InputReader inputReader;
    private BoardInformation boardInformation;
    private Movement movement;
    private GameObject activePlayer;

    public List<GameObject> activePlayers = new List<GameObject>(); // Referenced by Player Behaviour, so players can add themselves to the list.


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
        GetBoardInformation(boardInformationSOName);
        activePlayer = activePlayers.First();
        movement = gameObject.GetComponent<Movement>();
        inputReader = gameObject.GetComponent<InputReader>();
        inputReader.onMovementInput += PassActivePlayer;
    }

    private void PositionPlayersRandomly()
    {
        int randomX = Random.Range(1 , boardInformation.boardWidth+1) * boardInformation.playerStepLength;
        int randomY = Random.Range(1, boardInformation.boardHeight+1) * boardInformation.playerStepLength;
    }

    private void GetBoardInformation(string scriptableObjectName)
    {
        if (boardInformation != null)
        {
            Destroy(boardInformation);
        }
        var reference = Resources.Load<BoardInformation>("Scriptable Objects/" + scriptableObjectName);
        boardInformation = reference;
    }

    private void PassActivePlayer(Vector2 movementInput, InputActionPhase phase)
    {
        if (phase == InputActionPhase.Performed)
        {
            movement.activePlayer = activePlayer;
            movement.Move(movementInput);
        }
    }

}
