using System;
using System.Collections.Generic;
using UnityEngine;

public class CharacterTracker : MonoBehaviour
{
    public List<Character> activeCharacters;
    public List<Vector2> occupiedPositions = new List<Vector2>();
    public static CharacterTracker Instance; 
    public event Action<Character> OnActiveCharacterChanged;
    
    private Character _activeCharacter;
    public Character activeCharacter
    {
        get { return _activeCharacter; }
        set
        {
            if (_activeCharacter != value)
            {
                _activeCharacter = value;
                OnActiveCharacterChanged?.Invoke(_activeCharacter);
            }
        }
    }
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }
}
