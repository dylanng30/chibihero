using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KingAI : MonoBehaviour
{
    [SerializeField] private KingController kingController;

    private IState currentState;

    protected virtual void Awake()
    {
        LoadComponent();

    }
    protected virtual void LoadComponent()
    {
        LoadController();
    }
    protected virtual void LoadController()
    {
        if (kingController != null) return;
        kingController = GetComponentInParent<KingController>();
        currentState = kingController.IdleState;
    }
    public virtual void RandomState(IState state)
    {
        int randomState;
        do
        {
            //Debug.Log("trung state truoc");
            randomState = Random.Range(0, kingController.States.Count);
        } while (state == GetStateByIndex(randomState));

        this.currentState = GetStateByIndex(randomState);
        NextState(this.currentState);

    }

    private IState GetStateByIndex(int index)
    {
        switch (index)
        {
            case 0: return kingController.IdleState;
            case 1: return kingController.RunState;
            case 2: return kingController.FleeState;
            default: return kingController.IdleState;
        }
    }

    private void NextState(IState nextState)
    {
        //Debug.Log("Next State: " + nextState.GetType().Name);
        kingController.StateManager.ChangeState(nextState);
    }
}
