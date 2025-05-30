using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RKIdleState : IState
{
    RedKnightController redKnightController;
    private string currentAnimation = "Idle";
    public RKIdleState(RedKnightController redKnightController)
    {
        this.redKnightController = redKnightController;
    }
    public void Enter()
    {
        //Debug.Log("Idle");
        redKnightController.AnimationManager.SetAnimation(currentAnimation);
    }
    public void Execute()
    {
        if (redKnightController.AnimationManager.CoolDown(currentAnimation, 2f))
            return;

        redKnightController.RedKnightAI.RandomState(redKnightController.IdleState);
    }
    public void Exit()
    {

    }
}
public class RKRunState : IState
{
    RedKnightController redKnightController;
    private string currentAnimation = "Run";
    public RKRunState(RedKnightController redKnightController)
    {
        this.redKnightController = redKnightController;
    }
    public void Enter()
    {
        //Debug.Log("Run");
        redKnightController.AnimationManager.SetAnimation(currentAnimation);
    }
    public void Execute()
    {
        redKnightController.MovementRedKnight.FLipToPlayer();
        
        if (redKnightController.AbiDetectRedKnight.NextToGround())
        {
            redKnightController.MovementRedKnight.Flee();
            return;
        }
        else if (redKnightController.AbiDetectRedKnight.DetectGround() && !redKnightController.AbiDetectRedKnight.NextToGround())
        {
            redKnightController.MovementRedKnight.Jump();
            return;
        }

        if(redKnightController.AbiNormalATKRedKnight.CanAttack())
            redKnightController.StateManager.ChangeState(redKnightController.NormalATKState);

        redKnightController.MovementRedKnight.Moving();        
    }
    public void Exit()
    {

    }
}

public class RKFleeState : IState
{
    RedKnightController redKnightController;
    private string currentAnimation = "Run";
    public RKFleeState(RedKnightController redKnightController)
    {
        this.redKnightController = redKnightController;
    }
    public void Enter()
    {
        //Debug.Log("Flee");
        redKnightController.AnimationManager.SetAnimation(currentAnimation);
    }
    public void Execute()
    {
        redKnightController.MovementRedKnight.Flip();
        
        if (redKnightController.AbiDetectRedKnight.NextToGround())
        {
            redKnightController.MovementRedKnight.Moving();
            return;
        }
        else if (redKnightController.AbiDetectRedKnight.DetectGround() && !redKnightController.AbiDetectRedKnight.NextToGround())
        {
            redKnightController.MovementRedKnight.Jump();
            return;
        }

        redKnightController.MovementRedKnight.Flee();

        if (!redKnightController.AnimationManager.CoolDown(currentAnimation, 5f))
            return;

        redKnightController.RedKnightAI.RandomState(redKnightController.FleeState);        
    }
    public void Exit()
    {
        
    }
}

public class RKNormalATKState : IState
{
    RedKnightController redKnightController;
    private string currentAnimation = "NormalATK";
    public RKNormalATKState(RedKnightController redKnightController)
    {
        this.redKnightController = redKnightController;
    }
    public void Enter()
    {
        //Debug.Log("NormalATK");
        redKnightController.AnimationManager.SetAnimation(currentAnimation);
        redKnightController.PhysicRedKnight.Rigidbody2D.velocity = Vector2.zero;
    }
    public void Execute()
    {
        redKnightController.MovementRedKnight.FLipToPlayer();

        if (redKnightController.AnimationManager.FinishAnimation(currentAnimation))
            redKnightController.RedKnightAI.RandomState(redKnightController.RunState);
    }
    public void Exit()
    {
        redKnightController.AbiNormalATKRedKnight.NormalATK();
    }
}

public class RKShootState : IState
{
    RedKnightController redKnightController;
    private string currentAnimation = "Shoot";
    public RKShootState(RedKnightController redKnightController)
    {
        this.redKnightController = redKnightController;
    }
    public void Enter()
    {
        //Debug.Log("Shoot");
        redKnightController.AnimationManager.SetAnimation(currentAnimation);
        redKnightController.PhysicRedKnight.Rigidbody2D.velocity = Vector2.zero;
    }
    public void Execute()
    {
        redKnightController.MovementRedKnight.FLipToPlayer();

        if (redKnightController.AnimationManager.FinishAnimation(currentAnimation))
            redKnightController.RedKnightAI.RandomState(redKnightController.ShootState);
    }
    public void Exit()
    {
        redKnightController.AbiRangeATKRedKnight.Shoot();
    }
}
