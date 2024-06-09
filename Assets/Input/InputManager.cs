using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    private PlayerInputActions playerInputAction;
    public event Action<Vector2> onMovementInput = delegate { };
    public event Action onInteractionInput = delegate { };

    private void Awake()
    {
        playerInputAction = new PlayerInputActions();
    }

    private void OnEnable()
    {
        playerInputAction.PlayerActions.Enable();
        playerInputAction.PlayerActions.Move.performed += HandleMovementInput;
        playerInputAction.PlayerActions.Interact.canceled += HandleInteractionInput;
    }

    private void OnDisable()
    {
        playerInputAction.PlayerActions.Disable();
        playerInputAction.PlayerActions.Move.performed -= HandleMovementInput;
        playerInputAction.PlayerActions.Interact.canceled -= HandleInteractionInput;
    }

    public void HandleMovementInput(InputAction.CallbackContext ctx)
    {
        onMovementInput.Invoke(ctx.ReadValue<Vector2>());
    }

    public void HandleInteractionInput(InputAction.CallbackContext ctx)
    {
        onInteractionInput.Invoke();
    }
}
