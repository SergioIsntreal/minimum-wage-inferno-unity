using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MovementDirection
{ 
    Up,
    Down,
    Left,
    Right
}

public class GridMovementCharacter : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private Vector2 gridSize = new Vector2(1f, 1f);
    [SerializeField] private ObstacleTilemap obstacleTilemap;
    [SerializeField] private Tile_Selection tileSelection;

    private Vector2 targetPosition;
    private bool isMoving = false;
    private MovementDirection currentDirection = MovementDirection.Down;

    private void Update()
    {
        HandleMovementInput();
    }

    private void HandleMovementInput()
    { 
        if (!isMoving && Input.GetMouseButtonDown(0))
        {
            targetPosition = tileSelection.GetHighlightedTilePosition();
            Vector2Int clickedTile = GridUtils.WorldToGrid(targetPosition);

            if (!obstacleTilemap.IsTileObstacle(clickedTile))
            {
                if (targetPosition != Vector2.zero)
                {
                    FindPathToTargetPosition();
                }
            }
        }

        if (isMoving)
        {
            MoveTowardsTarget();
        }
    }

    private void FindPathToTargetPosition()
    {
        Vector2 startPosition = GridUtils.GridToWorld(GridUtils.WorldToGrid(transform.position));
        List<Vector2> path = AStar.FindPath(startPosition, targetPosition, gridSize, obstacleTilemap.IsTileObstacle);

        if (path != null && path.Count > 0)
        {
            StartCoroutine(MoveAlongPath(path));
        }
    }

    private IEnumerator MoveAlongPath(List<Vector2> path)
    {
        isMoving = true;
        int currentWaypointIndex = 0;

        while (currentWaypointIndex < path.Count)
        {
            targetPosition = path[currentWaypointIndex] + gridSize / 2;

            while ((Vector2)transform.position != targetPosition)
            {
                float step = moveSpeed * Time.fixedDeltaTime;
                transform.position = Vector3.MoveTowards(transform.position, targetPosition, step);

                yield return new WaitForFixedUpdate();
            }

            currentWaypointIndex++;
        }

        isMoving = false;
    }

    private void MoveTowardsTarget()
    {
        Vector2 direction = (targetPosition - (Vector2)transform.position).normalized;

        if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
        {
            if (direction.x > 0)
                currentDirection = MovementDirection.Right;
            else
                currentDirection = MovementDirection.Left;
        }
        else
        {
            if (direction.y > 0)
                currentDirection = MovementDirection.Up;
            else
                currentDirection = MovementDirection.Down;
        }
    }
}
