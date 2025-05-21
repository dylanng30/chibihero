using UnityEngine;

public class PhysicsEnemy : PhysicsBase
{
    [SerializeField] protected LowEnemyController lowEnemyController;
    protected override void Start()
    {
        base.Start();
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
        if (lowEnemyController != null) return;
        lowEnemyController = GetComponent<LowEnemyController>();
    }

    public void SetRigidBody2D()
    {
        rb.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
        rb.interpolation = RigidbodyInterpolation2D.Interpolate;
        rb.sleepMode = RigidbodySleepMode2D.NeverSleep;
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        rb.gravityScale = 1;
    }

    public void KnockBack()
    {
        PlayerController playerController = GameObject.FindObjectOfType<PlayerController>();
        Vector2 dir = playerController.transform.position - this.gameObject.transform.position;
        dir.y = 0.3f;
        this.rb.AddForce(dir.normalized * 100);
    }
}
