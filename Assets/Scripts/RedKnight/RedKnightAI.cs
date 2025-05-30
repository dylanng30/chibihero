using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedKnightAI : MonoBehaviour
{
    [SerializeField] private RedKnightController redKnightController;

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
        if (redKnightController != null) return;
        redKnightController = GetComponentInParent<RedKnightController>();
        currentState = redKnightController.IdleState;
    }
    public virtual void RandomState(IState state)
    {
        int randomState;
        do
        {
            Debug.Log("trung state truoc");
            randomState = Random.Range(0, redKnightController.States.Count);
        } while (state == GetStateByIndex(randomState));

        this.currentState = GetStateByIndex(randomState);
        NextState(this.currentState);

    }

    private IState GetStateByIndex(int index)
    {
        switch (index)
        {
            case 0: return redKnightController.IdleState;
            case 1: return redKnightController.RunState;
            case 2: return redKnightController.FleeState;
            case 3: return redKnightController.ShootState;
            case 4: return redKnightController.ShootState;
            default: return redKnightController.IdleState;
        }
    }

    private void NextState(IState nextState)
    {
        //Debug.Log("Next State: " + nextState.GetType().Name);
        redKnightController.StateManager.ChangeState(nextState);
    }
}
