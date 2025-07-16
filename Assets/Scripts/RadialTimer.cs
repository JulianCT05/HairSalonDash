using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class RadialTimer : MonoBehaviour
{
    public Image timerCircle;
    public float duration = 10f; // Seconds
    private float timeRemaining;

    void Start()
    {
        timeRemaining = duration;
        if (timerCircle != null)
            timerCircle.fillAmount = 1f;
    }

    void Update()
    {
        if (timeRemaining > 0)
        {
            timeRemaining -= Time.deltaTime;
            if (timerCircle != null)
                timerCircle.fillAmount = timeRemaining / duration;
        }
        else
        {
            TimerFinished();
        }
    }

    void TimerFinished()
    {
        Debug.Log("Timer done!");
        // Optionally switch scene or trigger event
        // SceneManager.LoadScene("GameOver");
        enabled = false; // Stop updating
    }
}
