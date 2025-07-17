using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class CountdownTimer : MonoBehaviour
{
    public float timeRemaining = 60f; // Set your desired time in seconds
    public TMP_Text timerText; // Make sure to assign your TextMeshPro object in Inspector
    public GameObject gameOverPanel; // Assign your Game Over UI panel
    public AudioSource countdownAudio;//sound 1
    public AudioSource gameOverAudio;//sound 2
    public float soundVolume = 1f;

    private bool isGameOver = false;

    void Start()
    {
        gameOverPanel.SetActive(false);
        countdownAudio.volume = soundVolume;
        countdownAudio.Play(); // Start ticking sound
        UpdateTimerDisplay();

    }

    void Update()
    {
        if (!isGameOver)
        {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
                if (timeRemaining < 0) timeRemaining = 0f; // Clamp to 0
                UpdateTimerDisplay();
            }
            if (timeRemaining <= 0 && !isGameOver)
            {
                GameOver();

            }

            /*else
            {
                timeRemaining = 0;
                UpdateTimerDisplay();
                GameOver();
            }*/
        }
    }

    public void UpdateTimerDisplay()
    {
        int minutes = Mathf.FloorToInt(timeRemaining / 60f);
        int seconds = Mathf.FloorToInt(timeRemaining % 60f);

        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    public void GameOver()
    {
        isGameOver = true;
        gameOverPanel.SetActive(true);
        countdownAudio.Stop();      // Stop ticking
        gameOverAudio.Play();       // Play Game Over sound

        Time.timeScale = 0f; // Pause the game
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        CollectableManager.Instance?.ResetCollectables();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}

