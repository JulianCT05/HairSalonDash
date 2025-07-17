using UnityEngine;
using UnityEngine.SceneManagement;  

public class GridPlayerMovement : MonoBehaviour
{
    public CountdownTimer m_Timer;
    public float moveTime = 0.2f;
    public LayerMask obstacleLayer;
    public int moveCount = 0;
    public GameObject trailPrefab;

    private bool isMoving = false;
    private Vector2 targetPosition;

    private bool gameOverTriggered = false;  

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

 
    private void OnTriggerEnter2D(Collider2D other)   // NEW
    {
    
        if (other.CompareTag("Collectable"))
        {
        
            Destroy(other.gameObject);
            return;
        }

        
        if (other.CompareTag("Hair"))
        {
            TriggerGameOver();
        }
    }


    private void TriggerGameOver()
    {
        if (gameOverTriggered) return;
        gameOverTriggered = true;

        Debug.Log("Player stepped on hair hazard.");

        
        if (m_Timer != null)
        {
            m_Timer.GameOver();
        }
        else
        {

            CountdownTimer timer = FindFirstObjectByType<CountdownTimer>();
            if (timer != null)
            {
                timer.GameOver();
                return;
            }
            else
            {
                Debug.LogWarning("No CountdownTimer found; loading GameOver scene as fallback.");
                SceneManager.LoadScene("GameOver");
            }
        }
    }

}
