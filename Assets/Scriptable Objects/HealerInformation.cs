using UnityEngine;

[CreateAssetMenu(fileName = "HealerInformation", menuName = "Game/HealerInformation")]
public class HealerInformation : ScriptableObject
{
    [SerializeField] private int health = 15;
    [SerializeField] private int movements = 2;
    [SerializeField] private int meleeAttack = 2;
    [SerializeField] private int rangeAttack = 2;
    [SerializeField] private int heal = 5;
    [SerializeField] private bool canCureOthers = true;
}