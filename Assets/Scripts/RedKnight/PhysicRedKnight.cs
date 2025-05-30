using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicRedKnight : PhysicsBase
{
    [SerializeField] private RedKnightController redKnightController;
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
        if (redKnightController != null) return;
        redKnightController = GetComponent<RedKnightController>();
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
        Vector2 dir = redKnightController.transform.position - player.transform.position;

        int scaleKB = 200;
        if (dir.x > 0)
            this.rb.AddForce(new Vector2(1, 1) * scaleKB);
        else
            this.rb.AddForce(new Vector2(-1, 1) * scaleKB);
            
    }
}
