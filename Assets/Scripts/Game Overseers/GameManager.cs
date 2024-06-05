using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public List<Character> activeCharacters;
    public List<Vector2> occupiedPositions = new List<Vector2>();
    public Character activeCharacter;

    public static GameManager Instance; 
    private void Awake()
    {
        Instance = this;
    }
}
