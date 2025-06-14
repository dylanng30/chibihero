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
    private MineState currentState = MineState.Inactive;
    [SerializeField] private string mineName = "MapMineTopDownFake";

    private Animator animator;

    private void Start()
    {
        LoadAnimator();
        SetState(currentState);
    }

    private void LoadAnimator()
    {
        if (this.animator != null) return;
        this.animator = GetComponent<Animator>();
    }
    public void KingIsDead()
    {
        SetState(MineState.Active);
    }

    public void IsRealMap(MineType type)
    {
        mineName = (type == MineType.Real) ? "MapMineTopDown" : "MapMineTopDownFake";
        //Debug.Log(this.gameObject + ": " + type);
    }

    public void SetState(MineState state)
    {
        this.currentState = state;
        if (this.animator == null) return;

        switch (state)
        {
            case MineState.Active:
                animator.Play("Active");
                break;
            case MineState.Inactive:
                animator.Play("Inactive");
                break;
            case MineState.Destroy:
                animator.Play("Destroy");
                break;
            default:
                break;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (currentState == MineState.Active && collision.gameObject.CompareTag("Player"))
        {
            GameManager.Instance.NextScene(mineName);
        }
    }
    public MineState CurrentMineState
    {
        get
        {
            return currentState;
        } 
    }
    public Animator Animator
    {
        get { return animator; }
    }
}
