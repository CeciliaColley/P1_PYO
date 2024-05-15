using UnityEngine;

[CreateAssetMenu(fileName = "EnemyInformation", menuName = "Game/EnemyInformation")]
public class EnemyInformation : ScriptableObject
{
    [SerializeField] private int health = 10;
    [SerializeField] private int movements = 1;
    [SerializeField] private int meleeAttack = 3;
    [SerializeField] private int rangeAttack = 1;
}
