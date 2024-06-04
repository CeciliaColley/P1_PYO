
using UnityEngine;
using UnityEngine.UIElements;

public class SetterUpper : MonoBehaviour
{
    [Tooltip("The path to the scriptable object that has the board's information.")]
    [SerializeField] private string boardInfoPath;
    SO_Board boardInfo;

    private GameManager gameManager;
    private BoardRules boardRules;

    private void Awake()
    {
        gameManager = GetComponent<GameManager>();
        boardRules = GetComponent<BoardRules>();
        boardInfo = Resources.Load<SO_Board>("ScriptableObjects/" + boardInfoPath);
        PositionCharactersRandomly();
    }

    private void PositionCharactersRandomly()
    {
        foreach (Character character in gameManager.activeCharacters)
        {
            Vector2 position = GenerateRandomPosition();

            while (!boardRules.DesiredCellIsEmpty(position))
            {
                position = GenerateRandomPosition();
            }

            character.transform.position = new Vector3(position.x, position.y, 0);
            gameManager.occupiedPositions.Add(position);
        }
    }

    private Vector2 GenerateRandomPosition()
    {
        int randomXStep = Random.Range(0, (int)boardInfo.horizontalCells);
        int randomYStep = Random.Range(0, (int)boardInfo.verticalCells);

        float randomX = boardInfo.lowestPositionX + (randomXStep * boardInfo.stepLength);
        float randomY = boardInfo.lowestPositionY + (randomYStep * boardInfo.stepLength);

        return new Vector2(randomX, randomY);
    }
}
