using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerTest : PersistentSingleton<GameManagerTest>
{
    private GameObject currentEnemy;
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
        UIManager.Instance.ShowMenu();
    }
    private void HandleStartingState()
    {
        // Logic for starting state
        //Set up environment, initialize variables, etc.
        UIManager.Instance.ShowMenu();
    }
    private void HandleExploringState()
    {
        StartCoroutine(HandleExploringStateCoroutine());
    }

    private IEnumerator HandleExploringStateCoroutine()
    {
        LevelManager.Instance.LoadScene("MainTopDown");
        yield return new WaitUntil(() => PlayerController.Instance != null && PlayerController.Instance.PhysicsPlayer != null);
        EnemyManager.Instance.ActivatePool();
        UIManager.Instance.DeactivateAllUIs();
        PlayerController.Instance.PhysicsPlayer.SetMode(PlayerMode.TopDown);
    }
    private void HandleFightingState()
    {
        // Logic for fighting state
        EnemyManager.Instance.DeactivatePool();
        PlayerController.Instance.PhysicsPlayer.SetMode(PlayerMode.Platform);
    }
        
    private void HandleGameOverState()
    {
        // Logic for game over state
    }
    private void HandlePausedState()
    {
        // Logic for paused state
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