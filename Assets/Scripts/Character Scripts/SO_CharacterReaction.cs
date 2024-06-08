using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CharacterReaction", menuName = "ScriptableObjects/CharacterReaction", order = 1)]
public class SO_CharacterReaction : ScriptableObject
{
    public Color attackedColor;
    public Color healedColor;
    public float reactionDuration;
    public AudioClip meleeAttackedSound;
    public AudioClip rangeAttackedSound;
    public AudioClip healSound;
}
