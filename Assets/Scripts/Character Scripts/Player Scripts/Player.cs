using UnityEngine;

public class Player : Character
{
    [Tooltip("The path to the scriptable object that has the player's information.")]
    [SerializeField] private string playerStatsPath;

    public Character target;

    private void Awake()
    {
        Initialize(playerStatsPath);
        characterMovement = GetComponent<PlayerMovement>();
        characterAction = GetComponent<PlayerAction>();
    }
}
