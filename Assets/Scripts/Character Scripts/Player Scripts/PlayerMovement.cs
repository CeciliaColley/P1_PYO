using UnityEngine.InputSystem;
using UnityEngine;
using System.Collections;

public class PlayerMovement : CharacterMovement, ICharacterMovement
{
    [Tooltip("Speed of the movement animation.")]
    [SerializeField] private float moveSpeed = 0.0f;
    private PlayerInputActions playerInput;

    public void Move(Character character)
    {
        playerInput.Movement.Enable();
        playerInput.Movement.Move.performed += ctx => OnMovementPerformed(ctx, character);
        StartCoroutine(DisableAfterMovement(character));
    }
    private void Awake()
    {
        playerInput = new PlayerInputActions();
    }
    private IEnumerator DisableAfterMovement(Character player)
    {
        yield return new WaitUntil(() => (player.movesLeft <= 0 || player.hasMoved));
        playerInput.Movement.Disable();
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
    private void OnMovementPerformed(InputAction.CallbackContext ctx, Character player)
    {
        if ( player.isMoving == false)
        {
            BoardRules.Direction direction = GetDesiredDirection(ctx);
            Vector2 desiredCell = GetDesiredCell(direction);
            if ((BoardRules.Instance.DesiredCellExists(desiredCell) && BoardRules.Instance.DesiredCellIsEmpty(desiredCell)))
            {
                MoveTheCharacter(player, desiredCell, moveSpeed);
            }
        }
    }
}
