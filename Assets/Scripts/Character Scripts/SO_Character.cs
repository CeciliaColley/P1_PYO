using System.Reflection;
using System;
using UnityEngine;
using System.Text;

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

    public string[] GetFields()
    {
        Type type = this.GetType();
        FieldInfo[] fields = type.GetFields(BindingFlags.Public | BindingFlags.Instance);

        string[] fieldStrings = new string[fields.Length];

        for (int i = 0; i < fields.Length; i++)
        {
            FieldInfo field = fields[i];
            fieldStrings[i] = FormatFieldName(field.Name) + ": " + field.GetValue(this);
        }

        return fieldStrings;
    }

    private string FormatFieldName(string fieldName)
    {
        if (string.IsNullOrEmpty(fieldName))
            return fieldName;

        StringBuilder formattedName = new StringBuilder();
        formattedName.Append(char.ToUpper(fieldName[0]));

        for (int i = 1; i < fieldName.Length; i++)
        {
            if (char.IsUpper(fieldName[i]))
            {
                formattedName.Append(' ');
            }
            formattedName.Append(fieldName[i]);
        }

        return formattedName.ToString();
    }

}
