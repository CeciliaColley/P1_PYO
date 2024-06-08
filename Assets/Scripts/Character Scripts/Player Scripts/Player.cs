using UnityEngine;

public class Player : Character
{
    [Tooltip("The path to the scriptable object that has the player's information.")]
    [SerializeField] private string playerStatsPath;
    [Tooltip("The path to the scriptable object that has the player's reaction information.")]
    [SerializeField] private string playerReactionPath;

    public Character target;

    private void Awake()
    {
        Initialize(playerStatsPath);
        CharacterReactionInfo = Resources.Load<SO_CharacterReaction>("ScriptableObjects/" + playerReactionPath);
        CharacterMovementInterface = GetComponent<PlayerMovement>();
        CharacterActionInterface = GetComponent<PlayerAction>();
    }
}
