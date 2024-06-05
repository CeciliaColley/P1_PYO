
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

public class SetterUpper : MonoBehaviour
{
    [Tooltip("The path to the scriptable object that has the board's information.")]
    [SerializeField] private string boardInfoPath;
    SO_Board boardInfo;

    private void Awake()
    {
        boardInfo = Resources.Load<SO_Board>("ScriptableObjects/" + boardInfoPath);
    }
    private void Start()
    {
        PositionCharactersRandomly();
    }
    private void PositionCharactersRandomly()
    {
        foreach (Character character in CharacterTracker.Instance.activeCharacters)
        {
            Vector2 position = GenerateRandomPosition();

            while (!BoardRules.Instance.DesiredCellIsEmpty(position))
            {
                position = GenerateRandomPosition();
            }

            character.transform.position = new Vector3(position.x, position.y, 0);
            CharacterTracker.Instance.occupiedPositions.Add(position);
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
