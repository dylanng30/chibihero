using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsKing : PhysicsBase
{
    [SerializeField] private KingController kingController;
    protected override void Awake()
    {
        base.Awake();
    }
    public override void LoadComponent()
    {
        base.LoadComponent();
    }
    public override void LoadController()
    {
        base.LoadController();
        if (kingController != null) return;
        kingController = GetComponent<KingController>();
    }
    public override void LoadRigidBody2D()
    {
        base.LoadRigidBody2D();
        SetRigidBody2D();
    }
    private void SetRigidBody2D()
    {
        rb.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
        rb.interpolation = RigidbodyInterpolation2D.Interpolate;
        rb.sleepMode = RigidbodySleepMode2D.NeverSleep;
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        rb.gravityScale = 1;
    }

    public void KnockBack(GameObject player)
    {
        Vector2 dir = kingController.transform.position - player.transform.position;
        if (dir.x > 0)
            this.rb.AddForce(Vector2.right * 50);
        else
            this.rb.AddForce(Vector2.left * 50);
    }
}
