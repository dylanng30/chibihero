using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PhysicsPirate : PhysicsBase
{
    [SerializeField] protected PirateController pirateController;
    protected override void Awake()
    {
        base.Awake();
    }
    public override void LoadComponent()
    {
        base.LoadComponent();
    }
    public override void LoadRigidBody2D()
    {
        base.LoadRigidBody2D();
        SetRigidBody2D();
    }
    public override void LoadController()
    {
        if (pirateController != null) return;
        pirateController = GetComponent<PirateController>();
    }
    public void SetRigidBody2D()
    {
        rb.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
        rb.interpolation = RigidbodyInterpolation2D.Interpolate;
        rb.sleepMode = RigidbodySleepMode2D.NeverSleep;
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        rb.gravityScale = 1;
    }
    public void KnockBack(GameObject player)
    {
        Vector2 dir = pirateController.transform.position - player.transform.position;
        int scaleKB = 200;
        if (dir.x > 0)
            this.rb.AddForce(new Vector2(1, 1) * scaleKB);
        else
            this.rb.AddForce(new Vector2(-1, 1) * scaleKB);
    }
}
