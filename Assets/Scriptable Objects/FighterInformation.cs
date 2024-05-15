using UnityEngine;

[CreateAssetMenu(fileName = "FighterInformation", menuName = "Game/FighterInformation")]
public class FighterInformation : ScriptableObject
{
    [SerializeField] private int health = 20;
    [SerializeField] private int movements = 3;
    [SerializeField] private int meleeAttack = 5;
    [SerializeField] private int heal = 2;
    [SerializeField] private bool canCureOthers = false;
}
