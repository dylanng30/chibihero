using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerTest : PersistentSingleton<GameManagerTest>
{
    private GameObject currentEnemy;
    private GameState previousState; // Store state before pausing
    public static event Action<GameState> OnBeforeStateChanged;
    public static event Action<GameState> OnAfterStateChanged;

    public GameState CurrentState { get; private set; }

    protected override void Awake()
    {
        base.Awake();
    }
    void Start()
    {
        CurrentState = GameState.Menu;
        HandleMenuState();
    }

    void Update()
    {
        // Pause logic is now handled by PauseManager
    }

    public void ChangeState(GameState newState)
    {
        if (CurrentState == newState) return;

        OnBeforeStateChanged?.Invoke(CurrentState);

        CurrentState = newState;
        switch (CurrentState)
        {
            case GameState.Menu:
                HandleMenuState();
                break;
            case GameState.Starting:
                HandleStartingState();
                break;
            case GameState.Exploring:
                HandleExploringState();
                break;
            case GameState.Fighting:
                HandleFightingState();
                break;
            case GameState.GameOver:
                HandleGameOverState();
                break;
            case GameState.Paused:
                HandlePausedState();
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(CurrentState), CurrentState, null);
        }

        OnAfterStateChanged?.Invoke(CurrentState);

        Debug.Log("Current State: " + CurrentState);
    }
    private void HandleMenuState()
    {
        // Logic for starting state
        Time.timeScale = 1f; // Ensure game is running in menu
        UIManager.Instance.ShowMenu();
    }
    private void HandleStartingState()
    {
        
    }
    private void HandleExploringState()
    {
        Time.timeScale = 1f; // Ensure game is running
        StartCoroutine(HandleExploringStateCoroutine());
    }

    private IEnumerator HandleExploringStateCoroutine()
    {
        LevelManager.Instance.LoadScene("MainTopDown");
        yield return new WaitUntil(() => PlayerController.Instance != null && PlayerController.Instance.PhysicsPlayer != null);
        EnemyManager.Instance.ActivatePool();
        UIManager.Instance.ShowEXPBar();
        PlayerController.Instance.PhysicsPlayer.SetMode(PlayerMode.TopDown);
        EXPManager.Instance.Apply();
              
    }
    private void HandleFightingState()
    {
        // Logic for fighting state
        Time.timeScale = 1f; // Ensure game is running
        EnemyManager.Instance.DeactivatePool();
        UIManager.Instance.ShowEXPBar();
        PlayerController.Instance.PhysicsPlayer.SetMode(PlayerMode.Platform);
    }
        
    private void HandleGameOverState()
    {
        UIManager.Instance.ShowPlayerDied();
        StartCoroutine(HandleGameOverStateCoroutine());
        // Logic for game over state
    }
    private IEnumerator HandleGameOverStateCoroutine()
    {
        yield return new WaitForSeconds(3f);
        this.ChangeState(GameState.Exploring);
    }
    private void HandlePausedState()
    {
        // Logic for paused state - UI is handled by PauseManager
        UIManager.Instance.ShowPauseMenu();
    }

    public void PauseGame()
    {
        if (CurrentState != GameState.Exploring && CurrentState != GameState.Fighting)
            return;
            
        previousState = CurrentState;
        ChangeState(GameState.Paused);
    }

    public void ResumeGame()
    {
        if (CurrentState != GameState.Paused)
            return;
            
        UIManager.Instance.HidePauseMenu();
        ChangeState(previousState);
    }

    public void ChangeStateWithScene(string sceneName)
    {
        if (sceneName.Contains("MainTopDown"))
            ChangeState(GameState.Exploring);
        else
            ChangeState(GameState.Fighting);
    }
    public void NextScene(string nextScene, GameObject currentEnemy)
    {
        this.currentEnemy = currentEnemy;
        LevelManager.Instance.LoadScene(nextScene);
        ChangeStateWithScene(nextScene);
    }

    public void CompleteMap(bool CheckedComplete)
    {
        if (CheckedComplete)
        {
            EnemyManager.Instance.UnregisterEnemy(this.currentEnemy);
            ChangeState(GameState.Exploring);
        }
        else
            ChangeState(GameState.GameOver);
    }


}