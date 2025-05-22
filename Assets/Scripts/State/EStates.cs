using UnityEngine;


public class EIdleState : IState
{
    LowEnemyController enemyController;
    private string currentAnimation = "Idle";
    public EIdleState(LowEnemyController enemyController)
    {
        this.enemyController = enemyController;
    }
    public void Enter()
    {
        //Debug.Log("Idle State");
        enemyController.AnimationEnemy.SetAnimation(currentAnimation);
    }
    public void Execute()
    {
        if (!enemyController.AnimationEnemy.FinishAnimation(currentAnimation))
            return;

        if (!enemyController.AbilityNormalATK.PlayerInATKRange())
            enemyController.StateManager.ChangeState(enemyController.RunState);
        else
            enemyController.StateManager.ChangeState(enemyController.NormalATKState);

    }
    public void Exit()
    {

    }
}
public class ERunState : IState
{
    LowEnemyController enemyController;
    private string currentAnimation = "Run";
    public ERunState(LowEnemyController enemyController)
    {
        this.enemyController = enemyController;
    }
    public void Enter()
    {
        //Debug.Log("Run State");
        enemyController.AnimationEnemy.SetAnimation(currentAnimation);
    }
    public void Execute()
    {        
        if (enemyController.EnemyDetectObstacle.NextToWall())
        {
            enemyController.MovementEnemy.Flee();
            return;
        }            
        else if(enemyController.EnemyDetectObstacle.DetectObstacle() && !enemyController.EnemyDetectObstacle.NextToWall())
        {
            enemyController.MovementEnemy.Jump();
            return;
        }
        else if (enemyController.AbilityNormalATK.PlayerInATKRange())
        {
            enemyController.StateManager.ChangeState(enemyController.NormalATKState);
            return;
        }

        enemyController.MovementEnemy.Moving();
    }
    public void Exit()
    {

    }
}
public class ENormalATKState : IState
{
    LowEnemyController enemyController;
    private string currentAnimation = "NormalATK";
    public ENormalATKState(LowEnemyController enemyController)
    {
        this.enemyController = enemyController;
    }
    public void Enter()
    {
        //Debug.Log("Normal ATK State");
        enemyController.AnimationEnemy.SetAnimation(currentAnimation);
        enemyController.PhysicsEnemy.Rigidbody2D.velocity = Vector2.zero;
    }
    public void Execute()
    {
        if(enemyController.AnimationEnemy.FinishAnimation(currentAnimation))
            enemyController.StateManager.ChangeState(enemyController.IdleState);

    }
    public void Exit()
    {
        enemyController.AbilityNormalATK.NormalATK();
    }
}



