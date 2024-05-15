using UnityEngine;

[CreateAssetMenu(fileName = "RangeInformation", menuName = "Game/RangeInformation")]
public class RangeInformation : ScriptableObject
{
    public int health = 15;
    public int movements = 4;
    public int meleeAttack = 1;
    public int rangeAttack = 3;
    public int heal = 2;
    public bool canCureOthers = false;
}
