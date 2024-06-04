using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

public class Enemy : Character
{
    [Tooltip("The path to the scriptable object that has the enemy's information.")]
    [SerializeField] private string enemyStatsPath;

    private void Start()
    {
        Initialize(enemyStatsPath);
    }

    public Player DetermineTarget(GameManager gameManager)
    {
        var players = gameManager.activeCharacters.Where(character => character is Player).Cast<Player>();

        if (players.Any())
        {
            Player target = players.OrderBy(player =>
                Vector2.Distance(this.transform.position, player.transform.position)).FirstOrDefault();
            return target;
        }
        else
        {
            return null;
        }
    }
}
