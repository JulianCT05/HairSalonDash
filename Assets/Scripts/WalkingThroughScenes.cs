using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MainMenu : MonoBehaviour
{
    public void Play()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void Quit()
    {
        Application.Quit();
        Debug.Log("Player Has Quit the Game");
    }


    public void LoadTutorial()
    {
        SceneManager.LoadScene("Tutorial");
    }

    public void LoadExplain()
    {
        SceneManager.LoadScene("Explaination");
    }

    public void RestartGame()
    {
        Time.timeScale = 1f; 

        
        if (RandomLevelLoader.Instance != null)
        {
            RandomLevelLoader.Instance.FullyResetGameState(); // see below
        }

        
        SceneManager.LoadScene("MainMenu"); 
    }
}
