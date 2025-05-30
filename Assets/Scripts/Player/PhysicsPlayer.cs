using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Rigidbody2D))]

public class PhysicsPlayer : PhysicsBase
{
    [SerializeField] protected PlayerController playerController;

    private PlayerMode currentMode;
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
        this.SetRigidBody2D();
    }
    public override void LoadController()
    {
        if (playerController != null) return;
        playerController = GetComponent<PlayerController>();
    }

    public void SetRigidBody2D()
    {
        rb.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
        rb.interpolation = RigidbodyInterpolation2D.Interpolate;
        rb.sleepMode = RigidbodySleepMode2D.NeverSleep;
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        SetMode(PlayerMode.Platform);
    }
    public void SetMode(PlayerMode mode)
    {
        this.currentMode = mode;
        //Debug.Log("Set Player Mode: " + mode);
        if (mode == PlayerMode.TopDown)
            rb.gravityScale = 0;
        else if (mode == PlayerMode.Platform)
            rb.gravityScale = 1;
    }

    public void KnockBack(GameObject enemy)
    {
        Vector2 dir = playerController.gameObject.transform.position - enemy.transform.position;
        int scaleKB = 200;
        if (dir.x > 0)
            this.rb.AddForce(new Vector2(10, 1) * scaleKB);
        else
            this.rb.AddForce(new Vector2(-10, 1) * scaleKB);
    }

    public PlayerMode Mode
    {
        get { return currentMode; }
    }


}
