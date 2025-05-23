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
        if (this.pirateController != null)
            return;
        this.pirateController = GetComponentInParent<PirateController>();
    }
    public void Moving()
    {
        Vector2 origin = pirateController.transform.position;
        Debug.Log(origin);
        Vector2 target = pirateController.Target.transform.position;
        Debug.Log(target);
        int speed = pirateController.PirateStats.MoveSpeed;
        float atkRange = pirateController.PirateStats.ATKRange;
        Debug.Log(atkRange);

        if (Vector2.Distance(origin, target) < atkRange)
        {
            pirateController.PhysicsPirate.Rigidbody2D.velocity = Vector2.zero;
            return;
        }
        Vector2 direction = (target - origin).normalized;
        Debug.Log(direction);
        pirateController.PhysicsPirate.Rigidbody2D.velocity = new Vector2(direction.x * speed, pirateController.PhysicsPirate.Rigidbody2D.velocity.y);
        Debug.Log(pirateController.PhysicsPirate.Rigidbody2D.velocity);
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
    private void Flip()
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
