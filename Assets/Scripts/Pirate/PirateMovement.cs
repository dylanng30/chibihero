using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PirateMovement : MonoBehaviour
{
    [SerializeField] protected PirateController pirateController;

    private void Start()
    {
        LoadComponent();
    }
    protected void LoadComponent()
    {
        LoadController();
    }
    protected virtual void LoadController()
    {
        if (this.pirateController != null)
            return;
        this.pirateController = GetComponentInParent<PirateController>();
    }
    public void Moving()
    {
        Vector2 origin = pirateController.transform.position;
        Vector2 target = pirateController.Target.transform.position;
        int speed = pirateController.PirateStats.MoveSpeed;
        float atkRange = pirateController.PirateStats.ATKRange;

        if (Vector2.Distance(origin, target) < atkRange)
        {
            pirateController.PhysicsPirate.Rigidbody2D.velocity = Vector2.zero;
            return;
        }
        Vector2 direction = (target - origin).normalized;
        pirateController.PhysicsPirate.Rigidbody2D.velocity = new Vector2(direction.x * speed, pirateController.PhysicsPirate.Rigidbody2D.velocity.y);
    }
    public void Flee()
    {
        Vector2 origin = pirateController.transform.position;
        Vector2 target = pirateController.Target.transform.position;
        int speed = pirateController.PirateStats.MoveSpeed;
        Vector2 direction = (origin - target).normalized;
        pirateController.PhysicsPirate.Rigidbody2D.velocity = new Vector2(direction.x * speed, pirateController.PhysicsPirate.Rigidbody2D.velocity.y);
    }

    public void Jump()
    {
        int jumpPower = pirateController.PirateStats.JumpPower;
        pirateController.PhysicsPirate.Rigidbody2D.velocity = new Vector2(pirateController.PhysicsPirate.Rigidbody2D.velocity.x, jumpPower);
    }
    public void Flip()
    {
        float dirX = pirateController.PhysicsPirate.Rigidbody2D.velocity.x;
        if (dirX > 0)
            pirateController.transform.localScale = new Vector3(1, 1, 1);
        if (dirX < 0)
            pirateController.transform.localScale = new Vector3(-1, 1, 1);        
    }
    public void FLipToPlayer()
    {
        Vector2 origin = pirateController.transform.position;
        Vector2 target = pirateController.Target.transform.position;
        Vector2 direction = (target - origin).normalized;
        if (direction.x > 0)
            pirateController.transform.localScale = new Vector3(1, 1, 1);
        if (direction.x < 0)
            pirateController.transform.localScale = new Vector3(-1, 1, 1);
    }
}
