using UnityEngine;

[CreateAssetMenu(fileName = "BoardInformation", menuName = "Game/BoardInformation")]
public class BoardInformation : ScriptableObject
{
    [SerializeField] public int boardWidth = 6;
    [SerializeField] public int boardHeight = 4;
    [SerializeField] public int playerStepLength = 1;
    [SerializeField] public int lowestTilesY = 1;
    [SerializeField] public int leftmostTilesX = 1;
    [SerializeField] public int playerAmount = 5;
    [SerializeField] public bool tileSharingAllowed = false;
}
