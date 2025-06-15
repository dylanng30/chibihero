using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : PersistentSingleton<GameManager>
{
    private GameObject currentEnemy;
    private Vector3 lastPosition;
    private GameState previousState;

    public static event Action<GameState> OnBeforeStateChanged;
    public static event Action<GameState> OnAfterStateChanged;
    public GameState CurrentState { get; private set; }

    protected override void Awake()
    {
        base.Awake();
    }
    void Start()
    {
        //CurrentState = GameState.Menu;
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
                StartCoroutine(HandleExploringStateCoroutine());
                break;
            case GameState.Fighting:
                HandleFightingState();
                break;
            case GameState.GameOver:
                StartCoroutine(HandleGameOverStateCoroutine());
                break;
            case GameState.Win:
                StartCoroutine(HandleWinState());
                break;
            default:
                break;
        }        

        OnAfterStateChanged?.Invoke(CurrentState);

        ObserverManager.Instance.ChangeMap(CurrentState);

        Debug.Log("Current State: " + CurrentState);
    }
    private void HandleMenuState()
    {
        // Logic for starting state
        UIManager.Instance.DeactivateAllUIs();        
    }
    private void HandleStartingState()
    {
        
    }
    private IEnumerator HandleExploringStateCoroutine()
    {
        LevelManager.Instance.LoadScene("MainTopDown");
        yield return new WaitUntil(() => PlayerController.Instance != null && PlayerController.Instance.PhysicsPlayer != null);
        MineManager.Instance.MineSpawn();
        PlayerController.Instance.transform.position = lastPosition;
        UIManager.Instance.ShowEXPBar();
        PlayerController.Instance.PhysicsPlayer.SetMode(PlayerMode.TopDown);
        EXPManager.Instance.Apply();
        
        // Start invincibility when returning to top-down map (respawn scenario)
        if (previousState == GameState.GameOver ||
            previousState == GameState.Win)
        {
            PlayerController.Instance.DamageManager.StartInvincibility();
        }
    }
    private void HandleFightingState()
    {
        // Logic for fighting state
        //EnemyManager.Instance.DeactivatePool();
        UIManager.Instance.ShowEXPBar();
        PlayerController.Instance.PhysicsPlayer.SetMode(PlayerMode.Platform);
    }
        
    private IEnumerator HandleGameOverStateCoroutine()
    {
        UIManager.Instance.ShowGameOverPanel();
        yield return new WaitForSeconds(3f);
        previousState = GameState.GameOver; // Store previous state for respawn logic
        this.ChangeState(GameState.Exploring);
        PlayerController.Instance.AnimationPlayer.SpriteRenderer.enabled = true;
    }
    private IEnumerator HandleWinState()
    {
        UIManager.Instance.ShowWinPanel();
        yield return new WaitForSeconds(3f);
        previousState = GameState.Win; // Store previous state to avoid triggering invincibility
        ChangeState(GameState.Exploring);
    }

    public void ChangeStateWithScene(string sceneName)
    {
        Debug.Log("ChangeStateWithScene: " + sceneName);
        if (sceneName.Contains("TopDown"))
            ChangeState(GameState.Exploring);
        else
            ChangeState(GameState.Fighting);
    }
    public void NextScene(string nextScene, GameObject currentEnemy)
    {
        this.currentEnemy = currentEnemy;
        this.lastPosition = currentEnemy.transform.position;
        LevelManager.Instance.LoadScene(nextScene);
        ChangeStateWithScene(nextScene);
    }
    public void NextScene(string nextScene)
    {
        LevelManager.Instance.LoadScene(nextScene);
        ChangeStateWithScene(nextScene);
    }

    public void CompleteMap(bool CheckedComplete)
    {
        if (CheckedComplete)
        {
            EnemyManager.Instance.UnregisterEnemy(this.currentEnemy);
            ChangeState(GameState.Win);
        }
        else
            ChangeState(GameState.GameOver);
    }


}