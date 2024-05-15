using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    private MovementInputAction movement;
    private Vector2 moveInput;
    [SerializeField] private GameObject player;

    private void Awake()
    {
        movement = new MovementInputAction();
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

    //private void OnMove(InputAction.CallbackContext context)
    //{
    //    if (context.control.IsPressed())
    //    {
    //        Debug.Log("Multiple keys are pressed");
    //        return;
    //    }

    //    // Read move input
    //    moveInput = context.ReadValue<Vector2>();
    //    Debug.Log(moveInput);

    //    // Calculate the new position vector directly
    //    Vector3 newPosition = new Vector3(
    //        player.transform.position.x + moveInput.x,
    //        player.transform.position.y + moveInput.y,
    //        player.transform.position.z
    //    );


    //        // Apply constraints (if necessary) before updating the position
    //        if (newPosition.x < 1 || newPosition.x > 6)
    //    {
    //        newPosition.x = player.transform.position.x;
    //    }

    //    if (newPosition.y < 1 || newPosition.y > 4)
    //    {
    //        newPosition.y = player.transform.position.y;
    //    }

    //    // Apply the new position to the player
    //    player.transform.position = newPosition;
    //}

    private void OnMove(InputAction.CallbackContext context)
    {
        Vector2 inputVector = context.ReadValue<Vector2>();

        //either move in x or y but not both
        if (inputVector.x != 0)
        {
            // always move by a unit of 1
            moveInput.x = inputVector.x;
            moveInput.x = Mathf.Round(moveInput.x);
            moveInput.y = 0;
        }
        else if (inputVector.y != 0)
        {
            // always move by a unit of 1
            moveInput.y = inputVector.y;
            moveInput.y = Mathf.Round(moveInput.y);
            moveInput.x = 0;
        }

        // Calculate the new position
        Vector3 newPosition = player.transform.position + new Vector3(moveInput.x, moveInput.y, 0);

        // Apply constraints (if necessary) before updating the position
        newPosition.x = Mathf.Clamp(newPosition.x, 1f, 6f);
        newPosition.y = Mathf.Clamp(newPosition.y, 1f, 4f);

        // Apply the new position to the player
        player.transform.position = newPosition;
    }
}
