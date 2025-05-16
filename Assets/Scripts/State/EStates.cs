using UnityEngine;


public class EIdleState : IState
{
    LowEnemy _enemy;
    public EIdleState(LowEnemy enemy)
    {
        this._enemy = enemy;
    }
    public void Enter()
    {
        _enemy.GetAnim().Play("Idle");
    }
    public void Execute()
    {
        AnimatorStateInfo stateInfo = _enemy.GetAnim().GetCurrentAnimatorStateInfo(0);
        if (stateInfo.IsName("Idle") && stateInfo.normalizedTime >= 2f)
            _enemy.GetStateManager().ChangeState(_enemy.GetRunState());
    }
    public void Exit()
    {

    }
}
public class ERunState : IState
{
    LowEnemy _enemy;
    public ERunState(LowEnemy enemy)
    {
        this._enemy = enemy;
    }
    public void Enter()
    {
        _enemy.GetAnim().Play("Run");
    }
    public void Execute()
    {
        _enemy.ChasePlayer();
        if (_enemy.NextToWall())
            _enemy.SetSpeed(-_enemy.GetSpeed());
        else if (_enemy.DetectObstacle() && !_enemy.NextToWall())
        {
            _enemy.Jump();
            return;
        }            
        else if (_enemy.InATKRange())
        {
            _enemy.GetStateManager().ChangeState(_enemy.GetNormalATKState());
            return;
        }        
        _enemy.Move();
    }
    public void Exit()
    {

    }
}
public class ENormalATKState : IState
{
    LowEnemy _enemy;
    public ENormalATKState(LowEnemy enemy)
    {
        this._enemy = enemy;
    }
    public void Enter()
    {
        _enemy.NormalATK();
        _enemy.GetAnim().Play("NormalATK");
        _enemy.GetRb().velocity = Vector2.zero;

    }
    public void Execute()
    {
        AnimatorStateInfo stateInfo = _enemy.GetAnim().GetCurrentAnimatorStateInfo(0);
        if (stateInfo.IsName("NormalATK") && stateInfo.normalizedTime >= 1f)
            _enemy.GetStateManager().ChangeState(_enemy.GetIdleState());

    }
    public void Exit()
    {
        
    }
}



