using System.Runtime.InteropServices;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;

[RequireComponent(typeof(StateManager))]
public class PlayerController : PersistentSingleton<PlayerController>, IDamagable
{
    [SerializeField] protected PhysicsPlayer physicsPlayer;
    [SerializeField] protected CollisionPlayer collisionPlayer;
    [SerializeField] protected AnimationManager animationPlayer;
    [SerializeField] protected PlayerStats playerStats;
    [SerializeField] protected DamageManagerPlayer damageManager;
    [SerializeField] protected MovementPlayer movementPlayer;
    [SerializeField] protected AbilityNormalATK abilityNormalATK;
    [SerializeField] protected AbilitySkill abilitySkill;
    [SerializeField] private EXPManager expManager;

    private IdleState idleState;
    private RunState runState;
    private NormalATKState normalATKState;
    private SkillState skillState;
    private StateManager stateManager;


    public void TakeDamage(int damage, GameObject attacker)
    {
        damageManager.TakeDamage(damage, attacker);
    }
    public void UpdateStats(int level)
    {
        Debug.Log("Updating stats for level: " + level);
    }

    protected override void Awake()
    {
        base.Awake();
        LoadComponent();
    }
    void Start()
    {
        LoadState();
    }

    protected void LoadState()
    {
        stateManager = this.GetComponent<StateManager>();
        idleState = new IdleState(this);
        runState = new RunState(this);
        normalATKState = new NormalATKState(this);
        skillState = new SkillState(this);
        this.stateManager.ChangeState(idleState);
    }

    private void LoadComponent()
    {
        LoadPhysicsPlayer();
        LoadCollisionPlayer();
        LoadAnimationPlayer();
        LoadPlayerStat();
        LoadDamageManagerPlayer();
        LoadMovementPlayer();
        LoadAbilityNormalATK();
        LoadAbilitySkill();
    }

    //Load Component
    public void LoadEXPManager(EXPManager expManager)
    {
        if (this.expManager != null)
            return;
        this.expManager = expManager;
        Debug.Log(this.expManager + " loaded successfully.");
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
        this.animationPlayer = this.GetComponentInChildren<AnimationManager>();
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
    protected virtual void LoadAbilityNormalATK()
    {
        if (this.abilityNormalATK != null) return;
        this.abilityNormalATK = this.GetComponentInChildren<AbilityNormalATK>();
    }
    protected virtual void LoadAbilitySkill()
    {
        if (this.abilitySkill != null) return;
        this.abilitySkill = this.GetComponentInChildren<AbilitySkill>();
    }

    


    //Components
    public PhysicsPlayer PhysicsPlayer
    {
        get { return physicsPlayer; }
    }
    public CollisionPlayer CollisionPlayer
    {
        get { return collisionPlayer; }
    }
    public AnimationManager AnimationPlayer
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
    public AbilityNormalATK AbilityNormalATK
    {
        get { return abilityNormalATK; }
    }
    public AbilitySkill AbilitySkill
    {
        get { return abilitySkill; }
    }
    public EXPManager EXPManager
    {
        get { return expManager; }
    }

    //State
    public StateManager StateManager
    {
        get { return stateManager; }
    }
    public IdleState IdleState
    {
        get { return idleState; }
    }
    public RunState RunState
    {
        get { return runState; }
    }
    public NormalATKState NormalATKState
    {
        get { return normalATKState; }
    }
    public SkillState SkillState
    {
        get { return skillState; }
    }
}
