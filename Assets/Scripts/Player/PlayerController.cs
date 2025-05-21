using System.Runtime.InteropServices;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;

[RequireComponent(typeof(StateManager))]
public class PlayerController : PersistentSingleton<PlayerController>
{
    [SerializeField] protected PhysicsPlayer physicsPlayer;
    [SerializeField] protected CollisionPlayer collisionPlayer;
    [SerializeField] protected AnimationPlayer animationPlayer;
    [SerializeField] protected PlayerStats playerStats;
    [SerializeField] protected DamageManagerPlayer damageManager;
    [SerializeField] protected MovementPlayer movementPlayer;
    [SerializeField] protected AbilityNormalATK abilityNormalATK;
    [SerializeField] protected AbilitySkill abilitySkill;

    [SerializeField] Transform ATKPoint;
    [SerializeField] float PSpeed;
    [SerializeField] GameObject prefab;

    private IdleState idleState;
    private RunState runState;
    private NormalATKState normalATKState;
    private SkillState skillState;
    private StateManager stateManager;

    private bool Skill1Locked = true;
    
    void Start()
    {
        this.LoadComponent();
        this.LoadState();    
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
    public AbilityNormalATK AbilityNormalATK
    {
        get { return abilityNormalATK; }
    }
    public AbilitySkill AbilitySkill
    {
        get { return abilitySkill; }
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
