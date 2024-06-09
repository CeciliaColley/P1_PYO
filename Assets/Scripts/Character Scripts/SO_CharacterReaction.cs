using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CharacterReaction", menuName = "ScriptableObjects/CharacterReaction", order = 1)]
public class SO_CharacterReaction : ScriptableObject
{
    public Color attackedColor;
    public Color healedColor;
    public float reactionDuration;
    public AudioClip attackSound1;
    public AudioClip attackSound2;
    public AudioClip attackSound3;
    public AudioClip attackSound4;
    public AudioClip attackSound5;
    public AudioClip healSound;
}
