using UnityEngine;

public class PhysicsEnemy : PhysicsBase
{
    [SerializeField] protected LowEnemyController lowEnemyController;
    protected override void Awake()
    {
        base.Awake();
    }
    public override void LoadComponent()
    {
        base.LoadComponent();
    }
    private void Update()
    {
        
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

    public void KnockBack(GameObject player)
    {
        float dir = lowEnemyController.transform.position.x - player.transform.position.x;
        this.rb.AddForce(new Vector2(dir * lowEnemyController.EnemyStats.MoveSpeed, lowEnemyController.EnemyStats.JumpPower / 2));
    }
}
