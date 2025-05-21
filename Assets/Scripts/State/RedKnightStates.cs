using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RKIdleState : IState
{
    RedKnight redKnight;

    private string currentAnimation = "Idle";
    public RKIdleState(RedKnight redKnight)
    {
        this.redKnight = redKnight;
    }
    public void Enter()
    {
        //Debug.Log("Idle");
        //redKnight.AnimationPlayer.SetAnimation(currentAnimation);
    }
    public void Execute()
    {
        /*if (redKnight.AbilityNormalATK.ATKTrigger)
            redKnight.StateManager.ChangeState(redKnight.NormalATKState);
        else if (Input.GetMouseButton(1))
            redKnight.StateManager.ChangeState(redKnight.SkillState);
        else if (_player.MovementPlayer.DirectionMove != Vector2.zero)
            redKnight.StateManager.ChangeState(redKnight.RunState);*/
    }
    public void Exit()
    {

    }
}
public class RKRunState : IState
{
    RedKnight redKnight;

    private string currentAnimation = "Run";
    public RKRunState(RedKnight redKnight)
    {
        this.redKnight = redKnight;
    }
    public void Enter()
    {
        //Debug.Log("Idle");
        //redKnight.AnimationPlayer.SetAnimation(currentAnimation);
    }
    public void Execute()
    {
        /*if (redKnight.AbilityNormalATK.ATKTrigger)
            redKnight.StateManager.ChangeState(redKnight.NormalATKState);
        else if (Input.GetMouseButton(1))
            redKnight.StateManager.ChangeState(redKnight.SkillState);
        else if (_player.MovementPlayer.DirectionMove != Vector2.zero)
            redKnight.StateManager.ChangeState(redKnight.RunState);*/
    }
    public void Exit()
    {

    }
}

public class RKNormalATKState : IState
{
    RedKnight redKnight;
    private string currentAnimation = "Attack";
    public RKNormalATKState(RedKnight redKnight)
    {
        this.redKnight = redKnight;
    }
    public void Enter()
    {
        //Debug.Log("Idle");
        //redKnight.AnimationPlayer.SetAnimation(currentAnimation);
    }
    public void Execute()
    {
        /*if (redKnight.AbilityNormalATK.ATKTrigger)
            redKnight.StateManager.ChangeState(redKnight.NormalATKState);
        else if (Input.GetMouseButton(1))
            redKnight.StateManager.ChangeState(redKnight.SkillState);
        else if (_player.MovementPlayer.DirectionMove != Vector2.zero)
            redKnight.StateManager.ChangeState(redKnight.RunState);*/
    }
    public void Exit()
    {
    }
}

public class RKSkillState : IState
{
    RedKnight redKnight;
    private string currentAnimation = "Skill";
    public RKSkillState(RedKnight redKnight)
    {
        this.redKnight = redKnight;
    }
    public void Enter()
    {
        //Debug.Log("Idle");
        //redKnight.AnimationPlayer.SetAnimation(currentAnimation);
    }
    public void Execute()
    {
        /*if (redKnight.AbilityNormalATK.ATKTrigger)
            redKnight.StateManager.ChangeState(redKnight.NormalATKState);
        else if (Input.GetMouseButton(1))
            redKnight.StateManager.ChangeState(redKnight.SkillState);
        else if (_player.MovementPlayer.DirectionMove != Vector2.zero)
            redKnight.StateManager.ChangeState(redKnight.RunState);*/
    }
    public void Exit()
    {
    }
}
