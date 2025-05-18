using System.Runtime.InteropServices;
using UnityEngine;

[RequireComponent(typeof(StateManager))]
public class PlayerController : Entity
{
    [SerializeField] PhysicsPlayer physicsPlayer;
    [SerializeField] CollisionPlayer collisionPlayer;
    [SerializeField] AnimationPlayer animationPlayer;
    [SerializeField] PlayerStats playerStats;
    [SerializeField] DamageManagerPlayer damageManager;
    [SerializeField] MovementPlayer movementPlayer;

    [SerializeField] Transform ATKPoint;
    [SerializeField] float PSpeed;
    [SerializeField] GameObject prefab;

    private IdleState idleState;
    private RunState runState;
    private NormalATKState normalATKState;
    private SkillState skillState;

    private bool Skill1Locked = true;
    
    void Start()
    {
        this.LoadComponent();
        //this.BackUp();      
    }
    private void BackUp()
    {
        targetLayer = LayerMask.GetMask("Enemy");
        this.SetComponents();
        _speed = PSpeed;
        idleState = new IdleState(this);
        runState = new RunState(this);
        normalATKState = new NormalATKState(this);
        skillState = new SkillState(this);
        this.GetStateManager().ChangeState(idleState);
    }

    public void Update()
    {
        //this.Flip();
    }
    private void FixedUpdate()
    {

    }

    private void LoadComponent()
    {
        LoadPhysicsPlayer();
        LoadCollisionPlayer();
        LoadAnimationPlayer();
        LoadPlayerStat();
        LoadDamageManagerPlayer();
        LoadMovementPlayer();

    }
    protected virtual void LoadPhysicsPlayer()
    {
        if (this.physicsPlayer != null) return;
        this.physicsPlayer = this.GetComponentInChildren<PhysicsPlayer>();
    }
    protected virtual void LoadCollisionPlayer()
    {
        if (this.collisionPlayer != null) return;
        this.collisionPlayer = this.GetComponentInChildren<CollisionPlayer>();
    }
    protected virtual void LoadAnimationPlayer()
    {
        if (this.animationPlayer != null) return;
        this.animationPlayer = this.GetComponentInChildren<AnimationPlayer>();
    }
    protected virtual void LoadPlayerStat()
    {
        if (this.playerStats != null) return;
        this.playerStats = this.GetComponentInChildren<PlayerStats>();
    }
    protected virtual void LoadDamageManagerPlayer()
    {
        if (this.damageManager != null) return;
        this.damageManager = this.GetComponentInChildren<DamageManagerPlayer>();
    }
    protected virtual void LoadMovementPlayer()
    {
        if (this.movementPlayer != null) return;
        this.movementPlayer = this.GetComponentInChildren<MovementPlayer>();
    }

    public PhysicsPlayer PhysicsPlayer
    {
        get { return physicsPlayer; }
    }
    public CollisionPlayer CollisionPlayer
    {
        get { return collisionPlayer; }
    }
    public AnimationPlayer AnimationPlayer
    {
        get { return animationPlayer; }
    }
    public PlayerStats PlayerStats
    {
        get { return playerStats; }
    }
    public DamageManagerPlayer DamageManager
    {
        get { return damageManager; }
    }
    public MovementPlayer MovementPlayer
    {
        get { return movementPlayer; }
    }



    public void Move(float leftRight)
    {
        GetRb().velocity = new Vector2(leftRight * _speed, GetRb().velocity.y);
    }    

    public void NormalATK()
    {
        if (ATKPoint != null)
        {
            Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(ATKPoint.position, ATKRange, targetLayer);
            foreach (Collider2D enemy in hitEnemies)
            {
                Enemy e = enemy.GetComponent<Enemy>();
                e.TakeDamage(_Damage, this.transform);
            }
        }
        else
        {
            Debug.LogError("ATKPoint kco");
        }
    }
    public void Skill1()
    {
        
    }

    public IdleState GetIdleState()
    {
        return idleState;
    }
    public RunState GetRunState()
    {
        return runState;
    }
    public NormalATKState GetNormalATKState()
    {
        return normalATKState;
    }
    public SkillState GetSkill1State()
    {
        return skillState;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(ATKPoint.position, ATKRange);
    }
}
