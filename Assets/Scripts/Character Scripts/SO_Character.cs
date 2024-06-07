using UnityEngine;

[CreateAssetMenu(fileName = "CharacterStats", menuName = "ScriptableObjects/CharacterStats", order = 1)]
public class SO_Character : ScriptableObject
{
    public string characterName;
    public int initialHealth;
    public int speed;
    public int actions;
    public int meleeAttackDamage;
    public int rangedAttackDamage;
    public int rangedAttackMaxRange;
    public int healAmount;
    public int healMaxRange;
}
