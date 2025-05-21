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
    void FixedUpdate()
    {
        Moving();
    }
    protected void LoadComponent()
    {
        LoadController();
    }
    protected virtual void LoadController()
    {
        if (this.lowEnemyController != null)
            return;
        this.lowEnemyController = transform.GetComponentInParent<LowEnemyController>();
    }
    public virtual void Moving()
    {
        lowEnemyController.PhysicsEnemy.Rigidbody2D.velocity = new Vector2(this.SetSpeed(), lowEnemyController.PhysicsEnemy.Rigidbody2D.velocity.y);
    }
    private int SetSpeed()
    {
        Vector2 direction = lowEnemyController.Target.transform.position - lowEnemyController.transform.position;
        float distance = direction.magnitude;
        int speed = lowEnemyController.EnemyStats.MoveSpeed;
        if (direction.x > 0 && distance > lowEnemyController.EnemyStats.ATKRange)
            speed = lowEnemyController.EnemyStats.MoveSpeed;
        else if (direction.x < 0 && distance > lowEnemyController.EnemyStats.ATKRange)
            speed = -lowEnemyController.EnemyStats.MoveSpeed;
        else
        {
            speed = 0;
        }

        return speed;
    }

}
