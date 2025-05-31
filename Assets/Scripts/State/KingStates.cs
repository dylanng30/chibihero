using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KingChasePlayerState : IState
{
    private KingController kingController;
    private string currentState = "Run";
    public KingChasePlayerState(KingController kingController)
    {
        this.kingController = kingController;
    }
    public void Enter()
    {
        kingController.AnimationManager.SetAnimation(currentState);
        kingController.KingAI.SetStateBeforeHit(this);
    }
    public void Execute()
    {
        kingController.MovementKing.FLipToPlayer();

        if (kingController.AbiDetectKing.NextToWall())
        {
            kingController.MovementKing.Flee();
            return;
        }
        else if (kingController.AbiDetectKing.DetectObstacle() && !kingController.AbiDetectKing.NextToWall())
        {
            kingController.MovementKing.Jump();
            return;
        }

        if (kingController.AbiKingNormalATK.CanAttack())
            kingController.StateManager.ChangeState(kingController.NormalATKState);

        kingController.MovementKing.Moving();
    }
    public void Exit()
    {

    }
}
public class KingRunToDoorState : IState
{
    private KingController kingController;
    private string currentState = "Run";
    public KingRunToDoorState(KingController kingController)
    {
        this.kingController = kingController;
    }
    public void Enter()
    {
        kingController.AnimationManager.SetAnimation(currentState);
        kingController.KingAI.SetStateBeforeHit(this);
    }
    public void Execute()
    {
        kingController.MovementKing.Flip();

        if (kingController.AbiDetectKing.NextToWall())
        {
            kingController.MovementKing.Moving();
            return;
        }
        else if (kingController.AbiDetectKing.DetectObstacle() && !kingController.AbiDetectKing.NextToWall())
        {
            kingController.MovementKing.Jump();
            return;
        }

        kingController.MovementKing.Flee();
    }
    public void Exit()
    {

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
        if(kingController.AnimationManager.FinishAnimation(currentState))
            kingController.AbiKingNormalATK.NormalATK();
    }
    public void Exit()
    {

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
        if(kingController.AnimationManager.FinishAnimation(currentState))
            kingController.StateManager.ChangeState(kingController.DoorOutState);
    }
    public void Exit()
    {

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
        if (kingController.AnimationManager.FinishAnimation(currentState))
            kingController.StateManager.ChangeState(kingController.ChasePlayerState);
    }
    public void Exit()
    {

    }
}

public class KingHitState : IState
{
    private KingController kingController;
    private string currentState = "Hit";
    public KingHitState(KingController kingController)
    {
        this.kingController = kingController;
    }
    public void Enter()
    {
        kingController.AnimationManager.SetAnimation(currentState);
    }
    public void Execute()
    {
        if(kingController.AnimationManager.FinishAnimation(currentState))
            kingController.StateManager.ChangeState(kingController.KingAI.StateBeforeHit);

    }
    public void Exit()
    {

    }
}

