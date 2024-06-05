using UnityEngine;

[CreateAssetMenu(fileName = "BoardInformation", menuName = "ScriptableObjects/BoardInformation", order = 1)]
public class SO_Board : ScriptableObject
{
    public float horizontalCells;
    public float verticalCells;
    public float lowestPositionX;
    public float lowestPositionY;
    public int players;
    public int enemies;
    public float stepLength;
}
