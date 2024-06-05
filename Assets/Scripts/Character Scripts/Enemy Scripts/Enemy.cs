using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

public class Enemy : Character
{
    [Tooltip("The path to the scriptable object that has the enemy's information.")]
    [SerializeField] private string enemyStatsPath;

    public Player target;

    private void Start()
    {
        Initialize(enemyStatsPath);
        characterMovement = GetComponent<EnemyMovement>();
    }

    public void DetermineTarget()
    {
        var players = GameManager.Instance.activeCharacters.Where(character => character is Player).Cast<Player>();

        if (players.Any())
        {
            Player _target = players.OrderBy(player =>
                Vector2.Distance(this.transform.position, player.transform.position)).FirstOrDefault();
            target = _target;
        }
    }
}
