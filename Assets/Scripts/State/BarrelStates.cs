using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrelIdle : IState
{
    private BarrelController controller;
    private string state = "Idle";
    public BarrelIdle(BarrelController controller)
    { 
        this.controller = controller;
    }
    public void Enter()
    {
        controller.AnimationManager.SetAnimation(state);
    }

    public void Execute()
    {
        if(controller.Movement.InRange)
            controller.StateManager.ChangeState(controller.RunState);
    }

    public void Exit()
    {

    }
}

public class BarrelRun : IState
{
    private BarrelController controller;
    private string state = "Run";
    public BarrelRun(BarrelController controller)
    {
        this.controller = controller;
    }
    public void Enter()
    {
        controller.AnimationManager.SetAnimation(state);
    }

    public void Execute()
    {
        controller.Movement.Move();
    }

    public void Exit()
    {

    }
}

public class BarrelPreExplosion : IState
{
    private BarrelController controller;
    private string state = "PreExplosion";
    public BarrelPreExplosion(BarrelController controller)
    {
        this.controller = controller;
    }
    public void Enter()
    {
        controller.AnimationManager.SetAnimation(state);
    }

    public void Execute()
    {
        if (controller.AnimationManager.FinishAnimation(state))
            controller.StateManager.ChangeState(controller.ExplosionState);
    }

    public void Exit()
    {

    }
}
public class BarrelExplosion : IState
{
    private BarrelController controller;
    private string state = "Explosion";
    public BarrelExplosion(BarrelController controller)
    {
        this.controller = controller;
    }
    public void Enter()
    {
        controller.AnimationManager.SetAnimation(state);
    }

    public void Execute()
    {
        if (controller.AnimationManager.FinishAnimation(state))
            controller.BoomAbi.Boom();
    }

    public void Exit()
    {

    }
}