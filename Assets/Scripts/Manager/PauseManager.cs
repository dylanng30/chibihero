using UnityEngine;

public class PauseManager : Singleton<PauseManager>
{
    private bool isPaused = false;
    private float timeScaleBeforePause = 1f;

    public bool IsPaused => isPaused;    protected override void Awake()
    {
        base.Awake();
        Debug.Log("PauseManager initialized. Press ESC to pause during gameplay.");
    }

    void Update()
    {
        // Handle pause input
        if (InputManager.Instance != null && InputManager.Instance.PausePressed)
        {
            if (CanTogglePause())
            {
                TogglePause();
            }
        }
    }

    private bool CanTogglePause()
    {
        // Only allow pause during gameplay states
        GameState currentState = GameManagerTest.Instance.CurrentState;
        return currentState == GameState.Exploring || 
               currentState == GameState.Fighting || 
               currentState == GameState.Paused;
    }

    public void TogglePause()
    {
        if (isPaused)
        {
            ResumeGame();
        }
        else
        {
            PauseGame();
        }
    }

    public void PauseGame()
    {
        if (isPaused) return;

        timeScaleBeforePause = Time.timeScale;
        Time.timeScale = 0f;
        isPaused = true;

        // Tell GameManager to change to pause state
        GameManagerTest.Instance.PauseGame();
    }

    public void ResumeGame()
    {
        if (!isPaused) return;

        Time.timeScale = timeScaleBeforePause;
        isPaused = false;

        // Tell GameManager to resume
        GameManagerTest.Instance.ResumeGame();
    }

    public void ForceResume()
    {
        // Force resume without checking pause state (for menu transitions)
        Time.timeScale = 1f;
        isPaused = false;
    }
}
