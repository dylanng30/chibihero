using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KingIdleState : IState
{
    private KingController kingController;
    private string currentState = "Idle";
    public KingIdleState(KingController kingController)
    {
        this.kingController = kingController;
    }

    public void Enter()
    {
        kingController.AnimationManager.SetAnimation(currentState);
    }

    public void Execute()
    {
        throw new System.NotImplementedException();
    }

    public void Exit()
    {
        throw new System.NotImplementedException();
    }
}
public class KingRunState : IState
{
    private KingController kingController;
    private string currentState = "Run";
    public KingRunState(KingController kingController)
    {
        this.kingController = kingController;
    }
    public void Enter()
    {
        kingController.AnimationManager.SetAnimation(currentState);
    }
    public void Execute()
    {
        throw new System.NotImplementedException();
    }
    public void Exit()
    {
        throw new System.NotImplementedException();
    }
}
public class KingFleeState : IState
{
    private KingController kingController;
    private string currentState = "Run";
    public KingFleeState(KingController kingController)
    {
        this.kingController = kingController;
    }
    public void Enter()
    {
        kingController.AnimationManager.SetAnimation(currentState);
    }
    public void Execute()
    {
        throw new System.NotImplementedException();
    }
    public void Exit()
    {
        throw new System.NotImplementedException();
    }
}
public class KingJumpState : IState
{
    private KingController kingController;
    private string currentState = "Jump";
    public KingJumpState(KingController kingController)
    {
        this.kingController = kingController;
    }
    public void Enter()
    {
        kingController.AnimationManager.SetAnimation(currentState);
    }
    public void Execute()
    {
        throw new System.NotImplementedException();
    }
    public void Exit()
    {
        throw new System.NotImplementedException();
    }
}
public class KingFallState : IState
{
    private KingController kingController;
    private string currentState = "Fall";
    public KingFallState(KingController kingController)
    {
        this.kingController = kingController;
    }
    public void Enter()
    {
        kingController.AnimationManager.SetAnimation(currentState);
    }
    public void Execute()
    {
        throw new System.NotImplementedException();
    }
    public void Exit()
    {
        throw new System.NotImplementedException();
    }
}

public class KingNormalATKState : IState
{
    private KingController kingController;
    private string currentState = "NormalATK";
    public KingNormalATKState(KingController kingController)
    {
        this.kingController = kingController;
    }
    public void Enter()
    {
        kingController.AnimationManager.SetAnimation(currentState);
    }
    public void Execute()
    {
        throw new System.NotImplementedException();
    }
    public void Exit()
    {
        throw new System.NotImplementedException();
    }
}
public class KingShootState : IState
{
    private KingController kingController;
    private string currentState = "Shoot";
    public KingShootState(KingController kingController)
    {
        this.kingController = kingController;
    }
    public void Enter()
    {
        kingController.AnimationManager.SetAnimation(currentState);
    }
    public void Execute()
    {
        throw new System.NotImplementedException();
    }
    public void Exit()
    {
        throw new System.NotImplementedException();
    }
}
public class KingDoorInState : IState
{
    private KingController kingController;
    private string currentState = "DoorIn";
    public KingDoorInState(KingController kingController)
    {
        this.kingController = kingController;
    }
    public void Enter()
    {
        kingController.AnimationManager.SetAnimation(currentState);
    }
    public void Execute()
    {
        throw new System.NotImplementedException();
    }
    public void Exit()
    {
        throw new System.NotImplementedException();
    }
}
public class KingDoorOutState : IState
{
    private KingController kingController;
    private string currentState = "DoorOut";
    public KingDoorOutState(KingController kingController)
    {
        this.kingController = kingController;
    }
    public void Enter()
    {
        kingController.AnimationManager.SetAnimation(currentState);
    }
    public void Execute()
    {
        throw new System.NotImplementedException();
    }
    public void Exit()
    {
        throw new System.NotImplementedException();
    }
}

