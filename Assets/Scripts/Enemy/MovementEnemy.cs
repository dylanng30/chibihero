using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementEnemy : MonoBehaviour
{
    [SerializeField] protected LowEnemyController lowEnemyController;

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
        lowEnemyController.PhysicsEnemy.Rigidbody2D.velocity = new Vector2(lowEnemyController.PhysicsEnemy.Rigidbody2D.velocity.x, lowEnemyController.PhysicsEnemy.Rigidbody2D.velocity.y);
        //playerController.PhysicsPlayer.Rigidbody2D.velocity = new Vector2(direction.normalized.x * speed, playerController.PhysicsPlayer.Rigidbody2D.velocity.y);
    }

}
