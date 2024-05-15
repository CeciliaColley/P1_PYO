using UnityEngine;

[CreateAssetMenu(fileName = "RangeInformation", menuName = "Game/RangeInformation")]
public class RangeInformation : ScriptableObject
{
    [SerializeField] private int health = 15;
    [SerializeField] private int movements = 4;
    [SerializeField] private int meleeAttack = 1;
    [SerializeField] private int rangeAttack = 3;
    [SerializeField] private int heal = 2;
    [SerializeField] private bool canCureOthers = false;
}
