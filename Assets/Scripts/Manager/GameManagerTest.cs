using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerTest : Singleton<GameManagerTest>
{
    public static event Action<GameState> OnBeforeStateChanged;
    public static event Action<GameState> OnAfterStateChanged;

    public GameState CurrentState { get; private set; }

    void Start()
    {
        ChangeState(GameState.Exploring);
    }

    public void ChangeState(GameState newState)
    {
        if (CurrentState == newState) return;

        OnBeforeStateChanged?.Invoke(CurrentState);

        CurrentState = newState;
        switch (CurrentState)
        {
            case GameState.Starting:
                HandleStartingState();
                break;
            case GameState.SpawningPlayer:
                HandleSpawningPlayerState();
                break;
            case GameState.SpawningEnemies:
                HandleSpawningEnemiesState();
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

    private void HandleStartingState()
    {
        // Logic for starting state
        //Set up environment, initialize variables, etc.

        ChangeState(GameState.SpawningPlayer);
    }
    private void HandleSpawningPlayerState()
    {
        // Logic for spawning player state

        //UnitManager.Instance.SpawnPlayer();

        ChangeState(GameState.SpawningEnemies);
    }
    private void HandleSpawningEnemiesState()
    {
        // Logic for spawning enemies state
    }

    private void HandleExploringState()
    {
        // Logic for exploring state
    }
    private void HandleFightingState()
    {
        // Logic for fighting state
    }
    private void HandleGameOverState()
    {
        // Logic for game over state
    }
    private void HandlePausedState()
    {
        // Logic for paused state
    }


}