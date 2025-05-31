using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorIdleState : IState
{
    private DoorController doorController;
    private string currentState = "Idle";
    public DoorIdleState(DoorController doorController)
    {
        this.doorController = doorController;
    }
    public void Enter()
    {
        doorController.AnimationManager.SetAnimation(currentState);
    }

    public void Execute()
    {

    }

    public void Exit()
    {

    }
}
public class DoorOpenState : IState
{
    private DoorController doorController;
    private string currentState = "Open";
    public DoorOpenState(DoorController doorController)
    {
        this.doorController = doorController;
    }
    public void Enter()
    {
        doorController.AnimationManager.SetAnimation(currentState);
    }

    public void Execute()
    {
        if(doorController.AnimationManager.FinishAnimation(currentState))
        {
            doorController.StateManager.ChangeState(doorController.CloseState);
        }
    }

    public void Exit()
    {
        //doorController.TransformKingAbi
    }
}
public class DoorCloseState : IState
{
    private DoorController doorController;
    private string currentState = "Close";
    public DoorCloseState(DoorController doorController)
    {
        this.doorController = doorController;
    }
    public void Enter()
    {
        doorController.AnimationManager.SetAnimation(currentState);
    }

    public void Execute()
    {
        if (doorController.AnimationManager.FinishAnimation(currentState))
        {
            doorController.StateManager.ChangeState(doorController.IdleState);
        }
    }

    public void Exit()
    {

    }
}
