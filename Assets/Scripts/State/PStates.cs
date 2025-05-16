using UnityEngine;

public class IdleState : IState
{
    PlayerController _player;
    public IdleState(PlayerController player)
    {
        this._player = player;
    }
    public void Enter()
    {
        _player.GetAnim().Play("Idle");
    }
    public void Execute()
    {
        if (Input.GetMouseButton(0))
            _player.GetStateManager().ChangeState(_player.GetNormalATKState());
        /*else if (Input.GetMouseButton(1))
            _player.GetStateManager().ChangeState(_player.GetSkill1State());*/
        else if (_player.IsGrounded() && Mathf.Abs(Input.GetAxis("Horizontal")) > 0)
            _player.GetStateManager().ChangeState(_player.GetRunState());
        if(_player.IsGrounded() && Input.GetKey(KeyCode.Space))
            _player.Jump();

    }
    public void Exit()
    {

    }
}
public class RunState : IState
{
    PlayerController _player;
    private float leftRight;
    public RunState(PlayerController player)
    {
        this._player = player;
    }
    public void Enter()
    {
        _player.GetAnim().Play("Run");
    }
    public void Execute()
    {
        leftRight = Input.GetAxis("Horizontal");
        if (_player.IsGrounded() && Mathf.Abs(leftRight) == 0)
            _player.GetStateManager().ChangeState(_player.GetIdleState());
        else if(Input.GetMouseButton(0))
            _player.GetStateManager().ChangeState(_player.GetNormalATKState());
        else if(Input.GetMouseButton(1))
            _player.GetStateManager().ChangeState(_player.GetSkill1State());
        if(_player.IsGrounded() && Input.GetKeyDown(KeyCode.Space))
            _player.Jump();
        _player.Move(leftRight);

    }
    public void Exit()
    {

    }
}
public class NormalATKState : IState
{
    PlayerController _player;
    private float leftRight;
    public NormalATKState(PlayerController player)
    {
        this._player = player;
    }
    public void Enter()
    {
        _player.GetAnim().Play("NormalATK");
    }
    public void Execute()
    {
        leftRight = Input.GetAxis("Horizontal");        
        AnimatorStateInfo stateInfo = _player.GetAnim().GetCurrentAnimatorStateInfo(0);
        if (stateInfo.IsName("NormalATK") && stateInfo.normalizedTime >= 1f)
            _player.GetStateManager().ChangeState(_player.GetIdleState());
        if(_player.IsGrounded() && Input.GetKeyDown(KeyCode.Space))
            _player.Jump();
        _player.Move(leftRight);

    }
    public void Exit()
    {
        _player.NormalATK();
    }
}
public class SkillState : IState
{
    PlayerController _player;
    private float leftRight;
    public SkillState(PlayerController player)
    {
        this._player = player;
    }
    public void Enter()
    {
        _player.GetAnim().Play("Skill1");
    }
    public void Execute()
    {
        AnimatorStateInfo stateInfo = _player.GetAnim().GetCurrentAnimatorStateInfo(0);
        if (stateInfo.IsName("Skill1") && stateInfo.normalizedTime >= 1f)
            _player.GetStateManager().ChangeState(_player.GetIdleState());
    }
    public void Exit()
    {
        _player.Skill1();
    }
}

