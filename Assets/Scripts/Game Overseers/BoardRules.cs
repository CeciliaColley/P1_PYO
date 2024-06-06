using UnityEngine;
using System.Collections.Generic;

public class BoardRules : MonoBehaviour
{
    [Tooltip("The path to the scriptable object that has the board's information.")]
    [SerializeField] private string boardInfoPath;
    private SO_Board boardInfo;

    public enum Direction
    { Left, Right, Up, Down, Default }
    public static BoardRules Instance;


    private void Awake()
    {
        boardInfo = Resources.Load<SO_Board>("ScriptableObjects/" + boardInfoPath);
        Instance = this;
    }
    public Vector2 GetStepResult(GameObject character, Direction direction)
    {
        Vector2 resultingPosition = new Vector2(0,0);
        switch (direction)
        {
            case (Direction.Up):
                resultingPosition = new Vector2(character.transform.position.x, character.transform.position.y + boardInfo.stepLength);
                break;
            case (Direction.Down):
                resultingPosition = new Vector2(character.transform.position.x, character.transform.position.y - boardInfo.stepLength);
                break;
            case (Direction.Left):
                resultingPosition = new Vector2(character.transform.position.x - boardInfo.stepLength, character.transform.position.y);
                break;            
            case (Direction.Right):
                resultingPosition = new Vector2(character.transform.position.x + boardInfo.stepLength, character.transform.position.y);
                break;
            default:
                Debug.LogError("No valid direction was registered by the Board Rules script");
            break;            
        }
        return resultingPosition;
    }
    public bool DesiredCellIsEmpty(Vector2 position)
    {
        if (!CharacterTracker.Instance.occupiedPositions.Contains(position))
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
    public bool IsInDiamondRange(Character character, Character target, int range)
    {
        float _range = range * boardInfo.stepLength;
        float horizontalDifference = Mathf.Abs(character.transform.position.x - target.transform.position.x);
        float verticalDifference = Mathf.Abs(character.transform.position.y - target.transform.position.y);

        if ((horizontalDifference + verticalDifference) <= _range)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    public bool IsInSquareRange(Character character, Character target, int range)
    {
        float _range = range * boardInfo.stepLength;
        float horizontalDifference = Mathf.Abs(character.transform.position.x - target.transform.position.x);
        float verticalDifference = Mathf.Abs(character.transform.position.y - target.transform.position.y);

        if (horizontalDifference <= _range && verticalDifference <=range)
        {
            return true;
        }
        else
        { return false; }
    }
}
