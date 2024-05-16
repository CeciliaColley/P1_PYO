using UnityEngine;

[CreateAssetMenu(fileName = "HealerInformation", menuName = "Game/HealerInformation")]
public class HealerInformation : ScriptableObject
{
    public int health = 15;
    public int maxActions = 1;
    public int movements = 2;
    public int meleeAttack = 2;
    public int rangeAttack = 2;
    public int heal = 5;
    public bool canCureOthers = true;
}