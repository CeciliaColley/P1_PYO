using UnityEngine;
using System.Collections;

public class PlayerMovement : CharacterMovement, ICharacterMovement
{
    [Tooltip("Speed of the movement animation.")]
    [SerializeField] private float moveSpeed = 0.0f;
    [Tooltip("Game object with the script that detects input.")]
    [SerializeField] private InputManager inputManager;
    private Character player;

    public void Move(Character character)
    {
        Debug.Log("Moving the character");
        player = character;
        inputManager.onMovementInput += OnMovementPerformed;
        StartCoroutine(DisableAfterMovement(player));
    }

    private void OnMovementPerformed(Vector2 inputVector) 
    {
        if (player.isMoving == false)
        {
            BoardRules.Direction direction = GetDesiredDirection(inputVector);
            Vector2 desiredCell = GetDesiredCell(direction);
            if ((BoardRules.Instance.DesiredCellExists(desiredCell) && BoardRules.Instance.DesiredCellIsEmpty(desiredCell)))
            {
                MoveTheCharacter(player, desiredCell, moveSpeed);
            }
        }
    }

    private BoardRules.Direction GetDesiredDirection(Vector2 inputVector)
    {
        if (inputVector == Vector2.up)
        {
            return BoardRules.Direction.Up;
        }
        else if (inputVector == Vector2.down)
        {
            return BoardRules.Direction.Down;
        }
        else if (inputVector == Vector2.left)
        {
            return BoardRules.Direction.Left;
        }
        else if (inputVector == Vector2.right)
        {
            return BoardRules.Direction.Right;
        }
        else
        {
            return BoardRules.Direction.Default;
        }
    }

    private IEnumerator DisableAfterMovement(Character player)
    {
        yield return new WaitUntil(() => (player.MovesLeft <= 0 || player.hasMoved));
        inputManager.onMovementInput -= OnMovementPerformed;
    }

    private void OnDisable()
    {
        inputManager.onMovementInput -= OnMovementPerformed;
    }
}