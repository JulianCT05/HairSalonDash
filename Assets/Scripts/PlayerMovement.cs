using UnityEngine;

public class GridPlayerMovement : MonoBehaviour
{
    public float moveTime = 0.2f;
    public LayerMask obstacleLayer;
    public GameObject trailPrefab; // Assign your trail prefab here

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

        targetPosition = newPosition;
        isMoving = false;
    }

    private bool IsBlocked(Vector2 position)
    {
        return Physics2D.OverlapCircle(position, 0.1f, obstacleLayer) != null;
    }
}

