using UnityEngine.InputSystem;
using UnityEngine;

public class PlayerMovement : MonoBehaviour, ICharacterMovement
{
    [Tooltip("Speed of the movement animation.")]
    [SerializeField] private float moveSpeed = 0.0f;
    private PlayerMovementActions playerMovement;

    private void Awake()
    {
        playerMovement = new PlayerMovementActions();
    }
    private void DisableMovement()
    {
        playerMovement.Disable();
    }
    private BoardRules.Direction GetDesiredDirection(InputAction.CallbackContext ctx)
    {
        Vector2 inputVector = ctx.ReadValue<Vector2>();

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
    private void OnMovementPerformed(InputAction.CallbackContext ctx, Character character)
    {
        if ( character.isMoving == false)
        {
            BoardRules.Direction direction = GetDesiredDirection(ctx);
            Vector2 desiredCell = character.GetDesiredCell(direction);
            if ((BoardRules.Instance.DesiredCellExists(desiredCell) && BoardRules.Instance.DesiredCellIsEmpty(desiredCell)))
            {
                character.MoveTheCharacter(desiredCell, moveSpeed);
            }
        }
        if (character.movesLeft <= 0)
        {
            DisableMovement();
        }
    }
    public void Move(Character character)
    {
        playerMovement.Enable();
        playerMovement.Movement.Move.performed += ctx => OnMovementPerformed(ctx, character);
    }
}
