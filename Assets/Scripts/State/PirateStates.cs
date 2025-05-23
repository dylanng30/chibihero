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
        Debug.Log("Idle");
        pirateController.AnimationPirate.SetAnimation(currentState);
    }

    public void Execute()
    {
        if(pirateController.AnimationPirate.FinishAnimation(currentState))
            pirateController.StateManager.ChangeState(pirateController.RunState);
    }

    public void Exit()
    {

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
        Debug.Log("Run");
        pirateController.AnimationPirate.SetAnimation(currentState);
    }

    public void Execute()
    {
        if (pirateController.PirateDetectObstacle.NextToWall())
        {
            pirateController.PirateMovement.Flee();
            return;
        }
        else if (pirateController.PirateDetectObstacle.DetectObstacle() && !pirateController.PirateDetectObstacle.NextToWall())
        {
            pirateController.PirateMovement.Jump();
            return;
        }
        else if (pirateController.PirateNormalATKAbility.PlayerInNormalATKRange())
        {
            pirateController.StateManager.ChangeState(pirateController.NormalATKState);
            return;
        }

        pirateController.PirateMovement.Moving();
    }

    public void Exit()
    {

    }
}
public class PirateFleeState : IState
{
    private PirateController pirateController;
    private string currentState = "Run";
    public PirateFleeState(PirateController pirateController)
    {
        this.pirateController = pirateController;
    }
    public void Enter()
    {
        Debug.Log("Flee");
        pirateController.AnimationPirate.SetAnimation(currentState);
    }

    public void Execute()
    {
        if(pirateController.AnimationPirate.CoolDown(currentState))
            pirateController.StateManager.ChangeState(pirateController.RangeATKState);
        pirateController.PirateMovement.Flee();
    }

    public void Exit()
    {

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
        Debug.Log("NormalATK");
        pirateController.AnimationPirate.SetAnimation(currentState);
    }

    public void Execute()
    {
        if (pirateController.AnimationPirate.FinishAnimation(currentState))
            pirateController.StateManager.ChangeState(pirateController.FleeState);
    }

    public void Exit()
    {

    }
}
public class PirateRangeATKState : IState
{
    private PirateController pirateController;
    private string currentState = "RangeATK";
    public PirateRangeATKState(PirateController pirateController)
    {
        this.pirateController = pirateController;
    }
    public void Enter()
    {
        Debug.Log("RangeATK");
        pirateController.AnimationPirate.SetAnimation(currentState);
    }

    public void Execute()
    {
        if (pirateController.AnimationPirate.FinishAnimation(currentState))
            pirateController.StateManager.ChangeState(pirateController.IdleState);
    }

    public void Exit()
    {

    }
}
