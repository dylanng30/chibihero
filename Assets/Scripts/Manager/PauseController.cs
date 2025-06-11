using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseController : MonoBehaviour
{
    [SerializeField] private bool canPause = true;
    
    void Update()
    {
        // Check for pause input
        if (InputManager.Instance != null && InputManager.Instance.PausePressed && canPause)
        {
            HandlePauseInput();
        }
    }

    private void HandlePauseInput()
    {
        if (GameManager.Instance == null) return;
        
        GameState currentState = GameManager.Instance.CurrentState;
        
        // Toggle pause state
        if (currentState == GameState.Paused)
        {
            // Resume game - return to previous state
            ResumeGame();
        }
        else if (CanPauseInCurrentState(currentState))
        {
            // Pause game only during allowed gameplay states
            PauseGame();
        }
    }

    private bool CanPauseInCurrentState(GameState state)
    {
        // Only allow pausing during gameplay states
        return state == GameState.Exploring || state == GameState.Fighting;
    }

    private void PauseGame()
    {
        Debug.Log("Game Paused");
        GameManager.Instance.ChangeState(GameState.Paused);
    }

    private void ResumeGame()
    {
        Debug.Log("Game Resumed");
        // Find the pause menu component and resume
        PauseMenu pauseMenu = FindObjectOfType<PauseMenu>();
        if (pauseMenu != null)
        {
            pauseMenu.Resume();
        }
        else
        {
            // Fallback resume method
            GameManager.Instance.ResumeFromPause();
        }
    }

    public void SetCanPause(bool canPause)
    {
        this.canPause = canPause;
    }
}
