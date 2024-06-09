using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.TextCore.Text;
using System.Collections;

public class EnemyMovement : CharacterMovement, ICharacterMovement
{
    [Tooltip ("Speed of the movement animation.")]
    [SerializeField] private float moveSpeed = 0.0f;
    [Tooltip("Seconds between movement.")]
    [SerializeField] private float seconds = 0.0f;
    private Enemy enemy;

    public void Move(Character character)
    {
        bool moved = false;
        enemy.DetermineTarget();
        Queue<BoardRules.Direction> movementQueue = CreateMovementQueue(enemy.target);
        while (movementQueue.Count > 0)
        {
            BoardRules.Direction direction = movementQueue.Dequeue();
            Vector2 desiredCell = GetDesiredCell(direction);
            if (BoardRules.Instance.DesiredCellExists(desiredCell) && BoardRules.Instance.DesiredCellIsEmpty(desiredCell))
            {
                StartCoroutine(WaitThenMoveEnemy(seconds, character, desiredCell));
                moved = true;
                break;
            }
        }
        if (!moved) 
        { 
            character.hasMoved = true; 
        }
    }
    private void Start()
    {
        enemy = GetComponent<Enemy>();
        reactToAction = GetComponent<ReactToAction>();
    }    
    private BoardRules.Direction DetermineHorizontalDirection(Vector2 targetPosition)
    {
        return transform.position.x > targetPosition.x ? BoardRules.Direction.Left : BoardRules.Direction.Right;
    }
    private BoardRules.Direction DetermineVerticalDirection(Vector2 targetPosition)
    {
        return transform.position.y > targetPosition.y ? BoardRules.Direction.Down : BoardRules.Direction.Up;
    }
    private void PrioritizeDirections(Queue<BoardRules.Direction> directionQueue, Vector2 targetPosition, BoardRules.Direction horizontalDirection, BoardRules.Direction verticalDirection)
    {
        Vector2 resultIfHorizontalMovement = GetDesiredCell(horizontalDirection);
        Vector2 resultIfVerticalMovement = GetDesiredCell(verticalDirection);

        float distanceIfHorizontalMovement = Vector2.Distance(targetPosition, resultIfHorizontalMovement);
        float distanceIfVerticalMovement = Vector2.Distance(targetPosition, resultIfVerticalMovement);

        if (distanceIfHorizontalMovement < distanceIfVerticalMovement)
        {
            directionQueue.Enqueue(horizontalDirection);
            directionQueue.Enqueue(verticalDirection);
        }
        else
        {
            directionQueue.Enqueue(verticalDirection);
            directionQueue.Enqueue(horizontalDirection);
        }
    }
    private void FillRemainingDirections(Queue<BoardRules.Direction> directionQueue)
    {
        if (!directionQueue.Contains(BoardRules.Direction.Left)) directionQueue.Enqueue(BoardRules.Direction.Left);
        if (!directionQueue.Contains(BoardRules.Direction.Right)) directionQueue.Enqueue(BoardRules.Direction.Right);
        if (!directionQueue.Contains(BoardRules.Direction.Down)) directionQueue.Enqueue(BoardRules.Direction.Down);
        if (!directionQueue.Contains(BoardRules.Direction.Up)) directionQueue.Enqueue(BoardRules.Direction.Up);
    }
    private Queue<BoardRules.Direction> CreateMovementQueue(Player target)
    {
        Queue<BoardRules.Direction> directionQueue = new Queue<BoardRules.Direction>();
        Vector2 targetPosition = target.transform.position;

        BoardRules.Direction horizontalDirection = DetermineHorizontalDirection(targetPosition);
        BoardRules.Direction verticalDirection = DetermineVerticalDirection(targetPosition);

        PrioritizeDirections(directionQueue, targetPosition, horizontalDirection, verticalDirection);
        FillRemainingDirections(directionQueue);

        return directionQueue;
    }    
    private IEnumerator WaitThenMoveEnemy(float seconds, Character enemy, Vector2 desiredCell)
    { 
        yield return new WaitForSeconds(seconds);
        MoveTheCharacter(enemy, desiredCell, moveSpeed);
        if (enemy.MovesLeft > 0)
        {
            Move(enemy);
        }
    }
}
