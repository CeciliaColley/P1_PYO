using UnityEngine;

[CreateAssetMenu(fileName = "BoardInformation", menuName = "Game/BoardInformation")]
public class BoardInformation : ScriptableObject
{
    public int boardWidth = 6;
    public int boardHeight = 4;
    public int playerStepLength = 1;
    public int lowestTilesY = 1;
    public int leftmostTilesX = 1;
    public int playerAmount = 5;
    public bool tileSharingAllowed = false;
    public float maxInteractionDistance;

}
