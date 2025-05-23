using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PirateIdleState : IState
{
    private PirateController pirateController;
    private string currentState = "Idle";
    public PirateIdleState(PirateController pirateController)
    {
        this.pirateController = pirateController;
    }
    public void Enter()
    {
        pirateController.AnimationPirate.SetAnimation(currentState);
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
public class PirateRunState : IState
{
    private PirateController pirateController;
    private string currentState = "Run";
    public PirateRunState(PirateController pirateController)
    {
        this.pirateController = pirateController;
    }
    public void Enter()
    {
        pirateController.AnimationPirate.SetAnimation(currentState);
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
public class PirateJumpState : IState
{
    private PirateController pirateController;
    private string currentState = "Jump";
    public PirateJumpState(PirateController pirateController)
    {
        this.pirateController = pirateController;
    }
    public void Enter()
    {
        pirateController.AnimationPirate.SetAnimation(currentState);
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
public class PirateFallState : IState
{
    private PirateController pirateController;
    private string currentState = "Fall";
    public PirateFallState(PirateController pirateController)
    {
        this.pirateController = pirateController;
    }
    public void Enter()
    {
        pirateController.AnimationPirate.SetAnimation(currentState);
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
public class PirateHitState : IState
{
    private PirateController pirateController;
    private string currentState = "Hit";
    public PirateHitState(PirateController pirateController)
    {
        this.pirateController = pirateController;
    }
    public void Enter()
    {
        pirateController.AnimationPirate.SetAnimation(currentState);
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
public class PirateNormalATKState : IState
{
    private PirateController pirateController;
    private string currentState = "NormalATK";
    public PirateNormalATKState(PirateController pirateController)
    {
        this.pirateController = pirateController;
    }
    public void Enter()
    {
        pirateController.AnimationPirate.SetAnimation(currentState);
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
public class PirateAttackUpState : IState
{
    private PirateController pirateController;
    private string currentState = "ATKUp";
    public PirateAttackUpState(PirateController pirateController)
    {
        this.pirateController = pirateController;
    }
    public void Enter()
    {
        pirateController.AnimationPirate.SetAnimation(currentState);
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
public class PirateAttackDownState : IState
{
    private PirateController pirateController;
    private string currentState = "ATKDown";
    public PirateAttackDownState(PirateController pirateController)
    {
        this.pirateController = pirateController;
    }
    public void Enter()
    {
        pirateController.AnimationPirate.SetAnimation(currentState);
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
public class PirateRangeAttackState : IState
{
    private PirateController pirateController;
    private string currentState = "RangeATK";
    public PirateRangeAttackState(PirateController pirateController)
    {
        this.pirateController = pirateController;
    }
    public void Enter()
    {
        pirateController.AnimationPirate.SetAnimation(currentState);
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
