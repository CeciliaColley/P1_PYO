using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

public class Enemy : Character
{
    [Tooltip("The path to the scriptable object that has the enemy's information.")]
    [SerializeField] private string enemyStatsPath;
    [Tooltip("The path to the scriptable object that has the enemy's reaction information.")]
    [SerializeField] private string enemyReactionPath;

    public Player target;

    private void Awake()
    {
        if (enemyStatsPath != null)
        {
            Initialize(enemyStatsPath);
        }
        if (enemyReactionPath != null)
        {
            CharacterReactionInfo = Resources.Load<SO_CharacterReaction>("ScriptableObjects/" + enemyReactionPath);
        }
        CharacterMovementInterface = GetComponent<EnemyMovement>();
        CharacterActionInterface = GetComponent<EnemyAction>();
    }
    public void DetermineTarget()
    {
        var players = CharacterTracker.Instance.activeCharacters.Where(character => character is Player).Cast<Player>();

        if (players.Any())
        {
            Player _target = players.OrderBy(player =>
                Vector2.Distance(this.transform.position, player.transform.position)).FirstOrDefault();
            target = _target;
        }
    }
}
