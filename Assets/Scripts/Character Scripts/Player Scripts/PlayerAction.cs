using UnityEngine.InputSystem;
using UnityEngine;
using System.Collections;

public class PlayerAction : CharacterActions, ICharacterAction
{
    [Tooltip("Game object with the script that detects input.")]
    [SerializeField] private InputManager inputManager;
    [Tooltip("The game object that displays the player's possible actions.")]
    [SerializeField] private GameObject actionsPanel;
    private PlayerActionsEnabler playerActionEnabler;
    public Player player;
    private Camera mainCamera;

    private void Start()
    {
        playerActionEnabler = actionsPanel.GetComponent<PlayerActionsEnabler>();
        player = GetComponent<Player>();
        mainCamera = Camera.main;
        reactToAction = GetComponent<ReactToAction>();
    }

    public void Act (Character character)
    {
        inputManager.onInteractionInput += OnInteractionPerformed;
        StartCoroutine(DisableAfterInteraction());
    }

    public void OnInteractionPerformed()
    {
        var rayHit = Physics2D.GetRayIntersection(mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue()));
        if (!rayHit.collider) { return; }
        player.target = rayHit.collider.gameObject.GetComponent<Character>();
        playerActionEnabler.toggleActions(player, player.target);
        actionsPanel.transform.position = Camera.main.WorldToScreenPoint(rayHit.collider.gameObject.transform.position);
        actionsPanel.SetActive(true);
    }
    private IEnumerator DisableAfterInteraction()
    {
        yield return new WaitUntil(() => player.hasActed);
        actionsPanel.SetActive(false);
        inputManager.onInteractionInput -= OnInteractionPerformed;
    }

    private void OnDisable()
    {
        inputManager.onInteractionInput -= OnInteractionPerformed;
    }
}
