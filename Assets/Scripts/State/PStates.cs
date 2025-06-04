using Unity.VisualScripting;
using UnityEngine;
using System.Collections;

public class IdleState : IState
{
    PlayerController _player;

    private string currentAnimation = "Idle";
    public IdleState(PlayerController player)
    {
        this._player = player;
    }
    public void Enter()
    {
        Debug.Log("Idle");
        _player.AnimationPlayer.SetAnimation(currentAnimation);
    }
    public void Execute()
    {
        if (_player.AbilityNormalATK.ATKTrigger)
            _player.StateManager.ChangeState(_player.NormalATKState);
        else if (Input.GetMouseButton(1))
            _player.StateManager.ChangeState(_player.SkillState);
        else if (_player.MovementPlayer.DirectionMove != Vector2.zero)
            _player.StateManager.ChangeState(_player.RunState);

        if (Input.GetKey(KeyCode.M))
            _player.MovementPlayer.Dash();   
    }
    public void Exit()
    {

    }
}
public class RunState : IState
{
    PlayerController _player;
    private float leftRight;
    private string currentAnimation = "Run";
    public RunState(PlayerController player)
    {
        this._player = player;
    }
    public void Enter()
    {
        _player.AnimationPlayer.SetAnimation(currentAnimation);
       // Debug.Log("Run");
    }
    public void Execute()
    {
        if (_player.MovementPlayer.DirectionMove == Vector2.zero)
            _player.StateManager.ChangeState(_player.IdleState);
        else if (_player.AbilityNormalATK.ATKTrigger)
            _player.StateManager.ChangeState(_player.NormalATKState);
        else if (Input.GetMouseButton(1))
            _player.StateManager.ChangeState(_player.SkillState);
            
        if (Input.GetKey(KeyCode.M))
            _player.MovementPlayer.Dash();   
    }
    public void Exit()
    {

    }
}
public class NormalATKState : IState
{
    PlayerController _player;
    private string currentAnimation = "NormalATK";
    public NormalATKState(PlayerController player)
    {
        this._player = player;
    }
    public void Enter()
    {
        _player.AnimationPlayer.SetAnimation(currentAnimation);
        //Debug.Log("NormalATK");
    }
    public void Execute()
    {
        if(_player.AnimationPlayer.FinishAnimation(currentAnimation))
            _player.StateManager.ChangeState(_player.IdleState);
    }
    public void Exit()
    {
        _player.AbilityNormalATK.NormalATK();
    }
}
public class SkillState : IState
{
    PlayerController _player;
    private string currentAnimation = "Skill1";
    public SkillState(PlayerController player)
    {
        this._player = player;
    }
    public void Enter()
    {
        //Debug.Log("Skill1");
        _player.AnimationPlayer.SetAnimation(currentAnimation);
    }
    public void Execute()
    {
        if (_player.AnimationPlayer.FinishAnimation(currentAnimation))
            _player.StateManager.ChangeState(_player.IdleState);
    }
    public void Exit()
    {
        Debug.Log(_player.AbilitySkill);
        _player.AbilitySkill.Skill();
    }
}

