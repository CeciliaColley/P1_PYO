using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;
    [SerializeField] string boardInformationSOName;

    private MovementInputAction movement;
    private Vector2 moveInput;
    private GameObject player;
    private BoardInformation boardInformation;

    private void Awake()
    {
        movement = new MovementInputAction();
        GetBoardInformation(boardInformationSOName);
    }

    private void OnEnable()
    {
        movement.AM_Movement.Move.performed += OnMove;
        movement.AM_Movement.Enable();
    }

    private void OnDisable()
    {
        movement.AM_Movement.Move.performed -= OnMove;
        movement.AM_Movement.Disable();
    }

    private void OnMove(InputAction.CallbackContext context)
    {
        player = gameManager.activePlayer;
        Vector2 inputVector = context.ReadValue<Vector2>();
        setMovementDirectionAndLength(inputVector);
        Vector3 newPosition = player.transform.position + new Vector3(moveInput.x, moveInput.y, 0);
        newPosition.x = Mathf.Clamp(newPosition.x, boardInformation.lowestTilesY, boardInformation.boardWidth);
        newPosition.y = Mathf.Clamp(newPosition.y, boardInformation.leftmostTilesX, boardInformation.boardHeight);
        player.transform.position = newPosition;
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

    public void GetBoardInformation(string scriptableObjectName)
    {
        if (boardInformation != null)
        {
            Destroy(boardInformation);
        }
        var reference = Resources.Load<BoardInformation>("Scriptable Objects/" + scriptableObjectName);
        boardInformation = reference;
    }
}
