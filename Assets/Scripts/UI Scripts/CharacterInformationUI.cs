using System.Collections;
using System.Collections.Generic;
using System;
using System.Reflection;
using System.Text;
using UnityEngine;
using TMPro;


public class CharacterInformationUI : MonoBehaviour
{
    [Tooltip("The UI that displays the stats of the character.")]
    [SerializeField] protected GameObject characterDisplay;
    [Tooltip("The UI that displays the stats of the character.")]
    [SerializeField] protected Character character;
    protected TextMeshProUGUI textComponent;
    public string BuildCharacterInfo(params object[] info)
    {
        StringBuilder stringBuilder = new StringBuilder();

        for (int i = 0; i < info.Length; i += 2)
        {
            if (i + 1 < info.Length)
            {
                stringBuilder.AppendLine(info[i] + info[i + 1].ToString());
            }
            else
            {
                stringBuilder.AppendLine(info[i].ToString());
            }
        }

        return stringBuilder.ToString();
    }
    public string BuildCharacterInfo(SO_Character characterSO)
    {
        StringBuilder stringBuilder = new StringBuilder();
        string[] fields = characterSO.GetFields();

        for (int i = 0; i < fields.Length; i++)
        {
            stringBuilder.AppendLine(fields[i]);
        }

        return stringBuilder.ToString();
    }
}
