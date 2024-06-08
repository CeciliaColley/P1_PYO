using UnityEngine.InputSystem;
using UnityEngine;
using System.Collections;

public class PlayerAction : CharacterActions, ICharacterAction
{
    [Tooltip("The game object that displays the player's possible actions.")]
    [SerializeField] private GameObject actionsPanel;
    private PlayerActionsEnabler playerActionEnabler;
    private Player player;
    private PlayerInputActions playerInput;
    private Camera mainCamera;
    //private Character character;
    private void Awake()
    {
        playerInput = new PlayerInputActions();
        player = GetComponent<Player>();
        mainCamera = Camera.main;
        playerActionEnabler = actionsPanel.GetComponent<PlayerActionsEnabler>();
        reactToAction = GetComponent<ReactToAction>();
    }
    public void Act(Character character)
    {
        playerInput.Interaction.Enable();
        playerInput.Interaction.Interact.performed += ctx => OnInteractionPerformed(ctx, player);
        StartCoroutine(DisableAfterInterction(character));
    }
    private IEnumerator DisableAfterInterction(Character character)
    {
        yield return new WaitUntil(() => character.hasActed);
        actionsPanel.SetActive(false);
        playerInput.Interaction.Disable();
    }
    private void OnInteractionPerformed(InputAction.CallbackContext ctx, Player player)
    {
        var rayHit = Physics2D.GetRayIntersection(mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue()));
        if (!rayHit.collider)
        {
            return;
        }
        else
        {
            player.target = rayHit.collider.gameObject.GetComponent<Character>();
            playerActionEnabler.toggleActions(player, player.target);
            actionsPanel.transform.position = Camera.main.WorldToScreenPoint(rayHit.collider.gameObject.transform.position);
            actionsPanel.SetActive(true);
        }
    }
}
