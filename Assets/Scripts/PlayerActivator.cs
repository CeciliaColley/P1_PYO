using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

// Consider renaming this to turn tracker, or ActivePlayerReferencer
public class GameManager : MonoBehaviour
{
    private InputReader inputReader;
    private Movement movement;
    private GameObject activePlayer;

    public List<GameObject> activePlayers = new List<GameObject>(); // Referenced by Player Behaviour, so players can add themselves to the list.

    private void Start()
    {
        activePlayer = activePlayers.First();
        movement = gameObject.GetComponent<Movement>();
        inputReader = gameObject.GetComponent<InputReader>();
        inputReader.onMovementInput += PassActivePlayer;
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
