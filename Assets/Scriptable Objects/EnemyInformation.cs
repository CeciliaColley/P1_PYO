using UnityEngine;

[CreateAssetMenu(fileName = "EnemyInformation", menuName = "Game/EnemyInformation")]
public class EnemyInformation : ScriptableObject
{
    public int health = 10;
    public int maxActions = 1;
    public int movements = 1;
    public int meleeAttack = 3;
    public int rangeAttack = 1;
}
