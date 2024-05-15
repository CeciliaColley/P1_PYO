using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    private PlayerActivator playerActivator;
    private BoardInformation boardInformation;
    private InputReader inputReader;
    private GameObject activePlayer;
    private Vector2 moveInput;

    private void Start()
    {
        playerActivator = GetComponent<PlayerActivator>();
        boardInformation = InformationRetriever.Instance.boardInformation;
        inputReader = gameObject.GetComponent<InputReader>();
        inputReader.onMovementInput += OnMovementInputPerformed;
    }

    private void OnMovementInputPerformed(Vector2 movementInput, InputActionPhase phase)
    {
        if (phase == InputActionPhase.Performed)
        {
            Move(movementInput);
        }
    }
    private void Move(Vector2 movementInput)
    {
        activePlayer = playerActivator.activePlayer;
        Vector3 newPosition = GetNewPosition(movementInput);
        PlayerBehaviour playerBehaviour = activePlayer.GetComponent<PlayerBehaviour>();
        if (playerBehaviour.IsPositionOccupied(newPosition))
        {
            return;
        }
        activePlayer.transform.position = newPosition;
    }

    private Vector3 GetNewPosition(Vector2 moveVector)
    {
        setMovementDirectionAndLength(moveVector);
        Vector3 newPosition = activePlayer.transform.position + new Vector3(moveInput.x, moveInput.y, 0);
        newPosition.x = Mathf.Clamp(newPosition.x, boardInformation.lowestTilesY, boardInformation.boardWidth);
        newPosition.y = Mathf.Clamp(newPosition.y, boardInformation.leftmostTilesX, boardInformation.boardHeight);
        return newPosition;
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
}
