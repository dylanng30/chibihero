using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementEnemy : MonoBehaviour
{
    [SerializeField] protected LowEnemyController lowEnemyController;

    private void Start()
    {
        LoadComponent();
    }
    private void Update()
    {
        Flip();
    }
    protected void LoadComponent()
    {
        LoadController();
    }
    protected virtual void LoadController()
    {
        if (this.lowEnemyController != null)
            return;
        this.lowEnemyController = GetComponentInParent<LowEnemyController>();
    }
    public void Moving()
    {
        Vector2 origin = lowEnemyController.transform.position;
        Vector2 target = lowEnemyController.Target.transform.position;
        int speed = lowEnemyController.EnemyStats.MoveSpeed;
        float atkRange = lowEnemyController.EnemyStats.ATKRange;

        if (Vector2.Distance(origin, target) < atkRange)
        {
            lowEnemyController.PhysicsEnemy.Rigidbody2D.velocity = Vector2.zero;
            return;
        }
        Vector2 direction = (target - origin).normalized;
        lowEnemyController.PhysicsEnemy.Rigidbody2D.velocity = new Vector2(direction.x * speed, lowEnemyController.PhysicsEnemy.Rigidbody2D.velocity.y);
    }
    public void Flee()
    {
        Vector2 origin = lowEnemyController.transform.position;
        Vector2 target = lowEnemyController.Target.transform.position;
        int speed = lowEnemyController.EnemyStats.MoveSpeed;
        Vector2 direction = (origin - target).normalized;
        lowEnemyController.PhysicsEnemy.Rigidbody2D.velocity = new Vector2(direction.x * speed, lowEnemyController.PhysicsEnemy.Rigidbody2D.velocity.y);
    }

    public void Jump()
    {
        int jumpPower = lowEnemyController.EnemyStats.JumpPower;
        lowEnemyController.PhysicsEnemy.Rigidbody2D.velocity = new Vector2(lowEnemyController.PhysicsEnemy.Rigidbody2D.velocity.x, jumpPower);
    }
    private void Flip()
    {
        Vector2 origin = lowEnemyController.transform.position;
        Vector2 target = lowEnemyController.Target.transform.position;
        Vector2 direction = (target - origin).normalized;
        if (direction.x > 0)
            lowEnemyController.transform.localScale = new Vector3(1, 1, 1);
        if (direction.x < 0)
            lowEnemyController.transform.localScale = new Vector3(-1, 1, 1);
    }


}
