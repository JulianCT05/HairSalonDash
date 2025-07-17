using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class RandomLevelLoader : MonoBehaviour
{
    public static RandomLevelLoader Instance;

    [Header("Random Maze Pool (names must match Build Settings)")]
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
            Destroy(gameObject);
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

        Debug.Log("Loading random maze: " + chosenLevel);
        SceneManager.LoadScene(chosenLevel);
    }

    
    private void ResetUnplayedLevels()
    {
        unplayedLevels = new List<string>(levelSceneNames);
    }

    public void FullyResetGameState()
    {
        Debug.Log("RandomLevelLoader: full reset.");
        ResetUnplayedLevels();
        
    }


    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    public void NextScene()
    {
        int currentBuildIndex = SceneManager.GetActiveScene().buildIndex;
        int total = SceneManager.sceneCountInBuildSettings;

        if (total <= 0)
        {
            Debug.LogError("No scenes in Build Settings!");
            return;
        }

        int nextIndex = (currentBuildIndex + 1) % total;
        Debug.Log($"Loading next build scene (index {nextIndex})...");
        SceneManager.LoadScene(nextIndex);
    }

    public void NextSceneInList()
    {
        string currentName = SceneManager.GetActiveScene().name;
        int idx = levelSceneNames.IndexOf(currentName);
        if (idx < 0)
        {
            Debug.LogWarning($"Current scene '{currentName}' not found in levelSceneNames. Loading first in list.");
            if (levelSceneNames.Count > 0)
                SceneManager.LoadScene(levelSceneNames[0]);
            else
                Debug.LogError("levelSceneNames list is empty—cannot load next.");
            return;
        }

        int nextIdx = (idx + 1) % levelSceneNames.Count;
        string nextName = levelSceneNames[nextIdx];
        Debug.Log($"Loading next scene in list: {nextName}");
        SceneManager.LoadScene(nextName);
    }
}
