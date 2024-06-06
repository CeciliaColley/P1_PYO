using UnityEngine.InputSystem;
using UnityEngine;

public class PlayerAction : MonoBehaviour, ICharacterAction
{
    [Tooltip ("A reference to the panel that shows the player's available actions.")]
    [SerializeField] private GameObject actionsPanel;

    private PlayerActionsUI playerActionUI;
    private Player player;
    private PlayerMovementActions playerInteraction;
    private Camera mainCamera;
    Character character;
    public void Act(Character character)
    {
        playerInteraction.Interaction.Enable();
        playerInteraction.Interaction.Interact.performed += ctx => OnInteractionPerformed(ctx, player);
    }
    private void Awake()
    {
        player = GetComponent<Player>();
        mainCamera = Camera.main;
        playerInteraction = new PlayerMovementActions();
        character= GetComponent<Character>();
        Act(character);
    }

    private void Start()
    {
        playerActionUI = actionsPanel.GetComponent<PlayerActionsUI>();
    }

    private void DisableInteracion()
    {
        playerInteraction.Interaction.Disable();
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
            playerActionUI.toggleActions(player, player.target);
            actionsPanel.transform.position = Camera.main.WorldToScreenPoint(rayHit.collider.gameObject.transform.position);
            actionsPanel.SetActive(true);
        }

        if (player.hasActed)
        {
            DisableInteracion();
        }
    }
}
