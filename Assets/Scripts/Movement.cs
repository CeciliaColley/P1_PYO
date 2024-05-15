using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    [SerializeField] private string boardInformationSOName;

    //private MovementInputAction movement;
    private Vector2 moveInput;
    private BoardInformation boardInformation;

    public GameObject activePlayer; // Referenced by Game Manager when Game Manager assigns it's value.

    private void Awake()
    {
        //movement = new MovementInputAction();
        GetBoardInformation(boardInformationSOName);
    }

    //private void OnEnable()
    //{
    //    movement.AM_Movement.Move.performed += OnMove;
    //    movement.AM_Movement.Enable();
    //}

    //private void OnDisable()
    //{
    //    movement.AM_Movement.Move.performed -= OnMove;
    //    movement.AM_Movement.Disable();
    //}

    public void OnMove(Vector2 moveVector)
    {
        Debug.Log("Moving");
        setMovementDirectionAndLength(moveVector);
        Vector3 newPosition = activePlayer.transform.position + new Vector3(moveInput.x, moveInput.y, 0);
        newPosition.x = Mathf.Clamp(newPosition.x, boardInformation.lowestTilesY, boardInformation.boardWidth);
        newPosition.y = Mathf.Clamp(newPosition.y, boardInformation.leftmostTilesX, boardInformation.boardHeight);
        activePlayer.transform.position = newPosition;
    }

    private void setMovementDirectionAndLength(Vector2 inputVector)
    {
        if (inputVector.x != 0)
        {
            moveInput = new Vector2(inputVector.x, 0);
            moveInput.x = Mathf.Round(moveInput.x) * boardInformation.playerStepLength;
        }
        else if (inputVector.y != 0)
        {
            moveInput = new Vector2(0, inputVector.y);
            moveInput.y = Mathf.Round(moveInput.y) * boardInformation.playerStepLength;
        }
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
}
