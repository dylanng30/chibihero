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
        float dir = pirateController.transform.position.x - player.transform.position.x;
        this.rb.AddForce(new Vector2(dir * pirateController.PirateStats.MoveSpeed, pirateController.PirateStats.JumpPower / 2));
    }
}
