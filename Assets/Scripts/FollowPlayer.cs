using UnityEngine;

public class TrailSpawner : MonoBehaviour
{
    public GameObject trailPrefab;      // Assign a sprite prefab in the inspector
    public bool snapToGrid = false;      // Optionally snap trail to whole numbers
    public bool onlySpawnOnMove = true; // Avoids spawning every frame
    private Vector2 lastPosition;

    void Start()
    {
        lastPosition = transform.position;
        SpawnTrail(); // Leave one at the start
    }

    void Update()
    {
        Vector2 currentPosition = transform.position;

        // If player has moved to a new position (with some margin)
        if (onlySpawnOnMove && Vector2.Distance(currentPosition, lastPosition) >= 0.9f)
        {
            SpawnTrail();
            lastPosition = currentPosition;
        }
    }

    void SpawnTrail()
    {
        Vector2 spawnPosition = transform.position;

        if (snapToGrid)
        {
            spawnPosition = new Vector2(Mathf.Round(spawnPosition.x), Mathf.Round(spawnPosition.y));
        }

        Instantiate(trailPrefab, spawnPosition, Quaternion.identity);
    }
}
