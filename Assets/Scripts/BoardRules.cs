using UnityEngine;
using UnityEngine.UIElements;

public class BoardRules : MonoBehaviour
{
    [Tooltip("The path to the scriptable object that has the board's information.")]
    [SerializeField] private string boardInfoPath;
    private SO_Board boardInfo;
    private GameManager gameManager;

    public enum Direction
    { Left, Right, Up, Down, Static }


    private void Awake()
    {
        boardInfo = Resources.Load<SO_Board>("ScriptableObjects/" + boardInfoPath);
        gameManager = GetComponent<GameManager>();
    }

    public bool DesiredCellIsEmpty(Vector2 position)
    {
        if (!gameManager.occupiedPositions.Contains(position))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool DesiredCellExists(Vector2 position)
    {
        if (position.x >= boardInfo.lowestPositionX && 
            position.x <= boardInfo.horizontalCells*boardInfo.stepLength &&
            position.y >= boardInfo.lowestPositionY &&
            position.y <= boardInfo.verticalCells*boardInfo.stepLength)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public Vector2 GetStepResult(GameObject character, Direction direction)
    {
        Vector2 currentPosition = new Vector2(character.transform.position.x, character.transform.position.y);
        Vector2 resultingPosition = new Vector2(0,0);
        switch (direction)
        {
            case (Direction.Up):
                resultingPosition = new Vector2(currentPosition.x,  currentPosition.y + boardInfo.stepLength);
                break;
            case (Direction.Down):
                resultingPosition = new Vector2(currentPosition.x, currentPosition.y - boardInfo.stepLength);
                break;
            case (Direction.Left):
                resultingPosition = new Vector2(currentPosition.x - boardInfo.stepLength, currentPosition.y);
                break;            
            case (Direction.Right):
                resultingPosition = new Vector2(currentPosition.x + boardInfo.stepLength, currentPosition.y);
                break;
            case (Direction.Static):
                resultingPosition = currentPosition;
                break;
            default:
                Debug.LogError("No valid direction was registered by the Board Rules script");
            break;            
        }
        return resultingPosition;
    }
}
