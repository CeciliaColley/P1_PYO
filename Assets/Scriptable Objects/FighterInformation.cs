using UnityEngine;

[CreateAssetMenu(fileName = "FighterInformation", menuName = "Game/FighterInformation")]
public class FighterInformation : ScriptableObject
{
    public int health = 20;
    public int movements = 3;
    public int meleeAttack = 5;
    public int heal = 2;
    public bool canCureOthers = false;
}
