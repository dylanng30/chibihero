using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MineState
{
    Active,
    Inactive,
    Destroy,
}

public class MineController : MonoBehaviour
{
    private MineState currentState;
    private string mineName;

    private void Start()
    {
        mineName = "MapMineTopDownFake";
        currentState = MineState.Inactive;
    }
    public void SetMap(string mineName)
    {
        this.mineName = mineName;
    }

    public void SetState(MineState state)
    {
        this.currentState = state;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (currentState == MineState.Active && collision.gameObject.CompareTag("Player"))
        {
            GameManager.Instance.NextScene(mineName);
        }
    }
    public MineState CurrentMineState { get { return currentState; } }
}
