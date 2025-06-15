using Unity.VisualScripting;
using UnityEngine;
using System.Collections;

public class IdleState : IState
{
    PlayerController _player;

    private string currentAnimation = "Idle";
    public IdleState(PlayerController player)
    {
        this._player = player;    }
    
    public void Enter()
    {
        //Debug.Log("Idle");
        _player.AnimationPlayer.SetAnimation(currentAnimation);
        // Stop walk sound when entering idle state
        AudioManager.StopPlayerWalk();
        // Stop attack sound if any is still playing from previous attack
        // AudioManager.StopPlayerAttack(); // Only stop if we want to interrupt long attack sounds
    }
    public void Execute()
    {
        if (_player.AbilityNormalATK.ATKTrigger)
            _player.StateManager.ChangeState(_player.NormalATKState);
        else if (Input.GetMouseButton(1))
            _player.StateManager.ChangeState(_player.SkillState);
        else if (_player.MovementPlayer.DirectionMove != Vector2.zero)
            _player.StateManager.ChangeState(_player.RunState);            
    }
    public void Exit()
    {

    }
}
public class RunState : IState
{
    PlayerController _player;
    private string currentAnimation = "Run";
    public RunState(PlayerController player)
    {
        this._player = player;
    }    
    public void Enter()
    {
        _player.AnimationPlayer.SetAnimation(currentAnimation);
        // Play walk sound when entering run state
        AudioManager.PlayPlayerWalk(_player.transform.position);
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
    }
    public void Exit()
    {
        // Stop walk sound when exiting run state
        AudioManager.StopPlayerWalk();
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
        // Stop any previous attack sound when starting new attack
        AudioManager.StopPlayerAttack();
        
        // Play attack sound immediately when entering attack state
        AudioManager.PlayPlayerAttack(_player.AbilityNormalATK.GetCurrentAttackIndex(), _player.transform.position);
        //Debug.Log("NormalATK");
    }
    public void Execute()
    {
        if(_player.AnimationPlayer.FinishAnimation(currentAnimation))
            _player.StateManager.ChangeState(_player.IdleState);    }
    
    public void Exit()
    {
        _player.AbilityNormalATK.NormalATK();
        // Don't stop attack sound immediately - let it play out
        // AudioManager.StopPlayerAttack(); // Removed this line
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
        _player.MovementPlayer.FlipToEnemy();
        _player.AbilitySkill.Skill();
    }
}

public class HitState : IState
{
    PlayerController _player;
    private string currentAnimation = "Hit";
    public HitState(PlayerController player)
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

    }
}


