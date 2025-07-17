using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class RandomLevelLoader : MonoBehaviour
{
    public static RandomLevelLoader Instance;

    public List<string> levelSceneNames = new List<string> { "Maze1", "Maze2", "Maze3" };
    private List<string> unplayedLevels = new List<string>();

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            ResetUnplayedLevels();
        }
        else
        {
            Destroy(gameObject); // Prevent duplicates
        }
    }

    public void LoadRandomLevel()
    {
        if (unplayedLevels.Count == 0)
        {
            Debug.Log("All mazes played. Resetting...");
            ResetUnplayedLevels();
        }

        int index = Random.Range(0, unplayedLevels.Count);
        string chosenLevel = unplayedLevels[index];
        unplayedLevels.RemoveAt(index);

        Debug.Log("Loading: " + chosenLevel);
        SceneManager.LoadScene(chosenLevel);
    }

    private void ResetUnplayedLevels()
    {
        unplayedLevels = new List<string>(levelSceneNames);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
