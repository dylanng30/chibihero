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

        // ObserverManager.Instance.ChangeMap(CurrentState); // Temporarily disabled to fix double loading

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
        
        // Wait a bit more to ensure LevelManager has finished
        yield return new WaitForSeconds(0.1f);
        
        MineManager.Instance.MineSpawn();
        
        // Set player position for TopDown scene
        if (lastPosition != Vector3.zero)
        {
            // Returning from a fight - use saved position
            Debug.Log("Setting player to lastPosition: " + lastPosition);
            PlayerController.Instance.transform.position = lastPosition;
        }
        else
        {
            // First time or no saved position - use PlayerSpawn
            Debug.Log("No lastPosition, finding PlayerSpawn");
            GameObject spawnPoint = GameObject.Find("PlayerSpawn");
            if (spawnPoint != null)
            {
                PlayerController.Instance.transform.position = spawnPoint.transform.position;
                Debug.Log("Set player to PlayerSpawn: " + spawnPoint.transform.position);
            }
        }
        
        UIManager.Instance.ShowEXPBar();
        PlayerController.Instance.PhysicsPlayer.SetMode(PlayerMode.TopDown);
        EXPManager.Instance.Apply();
        
        // Start invincibility when returning to top-down map (both Win and Lose scenarios)
        if (previousState == GameState.GameOver)
        {
            PlayerController.Instance.DamageManager.StartInvincibility();
        }
        
        // Activate enemies for TopDown exploration
        EnemyManager.Instance.ActivatePool();
    }
    private void HandleFightingState()
    {
        // Logic for fighting state
        EnemyManager.Instance.DeactivatePool(); // Deactivate TopDown enemies
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
        Debug.Log("HandleWinState started");
        UIManager.Instance.ShowWinPanel();
        yield return new WaitForSeconds(3f);
        
        // Treat Win the same as GameOver for position logic
        previousState = GameState.GameOver; // This will trigger invincibility and position restore
        Debug.Log("HandleWinState - changing to Exploring state");
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
        Debug.Log("NextScene - Saving lastPosition: " + lastPosition + " from enemy: " + currentEnemy.name);
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
        Debug.Log("CompleteMap called with: " + CheckedComplete);
        
        if (CheckedComplete)
        {
            EnemyManager.Instance.UnregisterEnemy(this.currentEnemy);
            ChangeState(GameState.Win);
        }
        else
            ChangeState(GameState.GameOver);
    }


}