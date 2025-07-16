using UnityEngine;

public class GridPlayerMovement : MonoBehaviour
{
    public float moveTime = 0.2f;
    public LayerMask obstacleLayer;
    public int moveCount = 0;
    public GameObject trailPrefab; // Assign your trail prefab here

    // if you wanted to limit the steps change [public int moveCount = 0;] to:
    // public int maxSteps = 10; // Set in Inspector or via code
    // and add:
    // public int MoveCount { get; private set; } = 0;

    private bool isMoving = false;
    private Vector2 targetPosition;

    void Start()
    {
        targetPosition = transform.position;
    }

    void Update()
    {
        if (!isMoving)
        {
            Vector2 input = Vector2.zero;

            if (Input.GetKeyDown(KeyCode.W)) input = Vector2.up;
            else if (Input.GetKeyDown(KeyCode.S)) input = Vector2.down;
            else if (Input.GetKeyDown(KeyCode.A)) input = Vector2.left;
            else if (Input.GetKeyDown(KeyCode.D)) input = Vector2.right;

            if (input != Vector2.zero)
            {
                Vector2 newPosition = targetPosition + input;

                if (!IsBlocked(newPosition))
                {
                    StartCoroutine(MoveToPosition(newPosition));
                }

                else
                {
                    Debug.Log("Blocked at: " + newPosition);
                }
            }
        }
        // part 2 for adding the step counter limit, add this:
        // else if (MoveCount >= maxSteps)
        // {
        // Debug.Log("Step limit reached!");
        // Optionally trigger loss state or UI here
        // }
    }

    System.Collections.IEnumerator MoveToPosition(Vector2 newPosition)
    {
        isMoving = true;
        Vector2 start = transform.position;
        float elapsed = 0f;

        while (elapsed < moveTime)
        {
            transform.position = Vector2.Lerp(start, newPosition, elapsed / moveTime);
            elapsed += Time.deltaTime;
            yield return null;
        }

        transform.position = newPosition;

        // ✅ Spawn trail at the previous position (rounded to grid)
        if (trailPrefab != null)
        {
            Vector2 roundedPos = new Vector2(start.x, start.y);
            Instantiate(trailPrefab, roundedPos, Quaternion.identity);
        }
        moveCount++;
        Debug.Log("Moves made: " + moveCount);

        targetPosition = newPosition;
        isMoving = false;

    }

    private bool IsBlocked(Vector2 position)
    {
        return Physics2D.OverlapCircle(position, 0.1f, obstacleLayer) != null;
        
    }
}

