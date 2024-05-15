using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    private Vector2 moveInput;
    private BoardInformation boardInformation;

    public GameObject activePlayer; // Referenced by Game Manager when Game Manager assigns it's value.

    private void Start()
    {
        boardInformation = InformationRetriever.Instance.boardInformation;
    }

    public void Move(Vector2 moveVector)
    {
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
}
