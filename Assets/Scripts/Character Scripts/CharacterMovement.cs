using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    [SerializeField] private AudioClip stepSound;
    protected ReactToAction reactToAction;
    public Vector2 GetDesiredCell(BoardRules.Direction direction)
    {
        Vector2 desiredPosition = new Vector2();
        desiredPosition = BoardRules.Instance.GetStepResult(gameObject, direction);
        return desiredPosition;
    }
    public void MoveTheCharacter(Character character, Vector2 desiredCell, float moveSpeed)
    {
        Vector2 currentPosition = new Vector2(transform.position.x, transform.position.y);
        CharacterTracker.Instance.occupiedPositions.Remove(currentPosition);
        CharacterTracker.Instance.occupiedPositions.Add(desiredCell);

        Vector3 newPosition = new Vector3(desiredCell.x, desiredCell.y, 0);
        StartCoroutine(SlideToPosition(character, newPosition, moveSpeed, character.isMoving));
    }
    private IEnumerator SlideToPosition(Character character, Vector3 position, float moveSpeed, bool isMoving)
    {
        if (isMoving == false)
        {
            character.isMoving = true;
            character.MovesLeft--;
            float step = moveSpeed * Time.deltaTime;
            while (transform.position != position)
            {
                transform.position = Vector3.MoveTowards(transform.position, position, step);
                yield return null;
            }
            if (reactToAction != null && stepSound != null) { reactToAction.PlaySound(stepSound); }
            character.isMoving = false;
            if (character.MovesLeft <= 0) { character.hasMoved = true; }
        }
    }
}
