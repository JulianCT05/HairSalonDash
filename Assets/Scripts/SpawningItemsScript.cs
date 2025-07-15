using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public Transform[] spawnPoints;         // Can be more than number of items
    public GameObject[] itemPrefabs;        // Each item will spawn once only

    void Start()
    {
        SpawnUniqueItems();
    }

    void SpawnUniqueItems()
    {
        // Create a list of spawn point indices and shuffle them
        List<int> spawnIndices = new List<int>();
        for (int i = 0; i < spawnPoints.Length; i++)
        {
            spawnIndices.Add(i);
        }

        Shuffle(spawnIndices); // Shuffle to randomize which points are used

        // Loop through items and assign them to unique spawn points
        for (int i = 0; i < itemPrefabs.Length && i < spawnPoints.Length; i++)
        {
            Instantiate(itemPrefabs[i], spawnPoints[spawnIndices[i]].position, Quaternion.identity);
        }
    }

    // Fisher-Yates shuffle
    void Shuffle(List<int> list)
    {
        for (int i = list.Count - 1; i > 0; i--)
        {
            int rnd = Random.Range(0, i + 1);
            int temp = list[i];
            list[i] = list[rnd];
            list[rnd] = temp;
        }
    }
}
