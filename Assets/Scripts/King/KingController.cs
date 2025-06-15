using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KingController : MonoBehaviour, IDamagable
{
    [SerializeField] protected PhysicsKing physicsKing;
    [SerializeField] protected CollisionKing collisionKing;
    [SerializeField] protected DamageManagerKing damageManagerKing;
    [SerializeField] protected KingStats kingStats;
    [SerializeField] protected MovementKing movementKing;
    [SerializeField] protected AnimationManager animationManager;
    [SerializeField] protected AbiNormalATKKing abiKingNormalATK;
    [SerializeField] protected AbiDetectKing abiDetectKing;
    [SerializeField] protected KingAI kingAI;
    [SerializeField] protected HealthBar healthBar;

    //States
    [SerializeField] protected StateManager stateManager;
    [SerializeField] protected KingChasePlayerState chaseState;
    [SerializeField] protected KingRunToDoorState runToDoorState;
    [SerializeField] protected KingNormalATKState normalATKState;
    [SerializeField] protected KingDoorInState doorInState;
    [SerializeField] protected KingDoorOutState doorOutState;
    [SerializeField] protected KingHitState hitState;

    void Awake()
    {
        StartCoroutine(Load());
    }
    private IEnumerator Load()
    {
        yield return new WaitUntil(() => DoorManager.Instance != null);
        this.LoadComponent();
        this.LoadState();
    }
    public void TakeDamage(int damage, GameObject attacker)
    {
        damageManagerKing.TakeDamage(damage, attacker);
    }
    protected void LoadState()
    {
        stateManager = this.GetComponent<StateManager>();
        chaseState = new KingChasePlayerState(this);
        runToDoorState = new KingRunToDoorState(this);        
        normalATKState = new KingNormalATKState(this);        
        doorInState = new KingDoorInState(this);
        doorOutState = new KingDoorOutState(this);
        hitState = new KingHitState(this);

        stateManager.ChangeState(doorOutState);
    }

    private void LoadComponent()
    {
        LoadAnimation();
        LoadCollision();
        LoadStats();
        LoadPhysics();
        LoadDetectObstacle();
        LoadDamageManager();
        LoadMovement();
        LoadAbilityNormalATK();
        LoadDetectObstacle();
        LoadKingAI();
        LoadHeathBar();
    }

    protected virtual void LoadHeathBar()
    {
        if(this.healthBar != null) return;
        healthBar = GetComponentInChildren<HealthBar>();
    }
    protected virtual void LoadAnimation()
    {
        if (this.animationManager != null) return;
        this.animationManager = this.GetComponentInChildren<AnimationManager>();
    }
    protected virtual void LoadCollision()
    {
        if (this.collisionKing != null) return;
        this.collisionKing = this.GetComponentInChildren<CollisionKing>();
    }
    protected virtual void LoadStats()
    {
        if (this.kingStats != null) return;
        this.kingStats = this.GetComponentInChildren<KingStats>();
    }
    protected virtual void LoadPhysics()
    {
        if (this.physicsKing != null) return;
        this.physicsKing = this.GetComponent<PhysicsKing>();
    }
    protected virtual void LoadDamageManager()
    {
        if (this.damageManagerKing != null) return;
        this.damageManagerKing = this.GetComponentInChildren<DamageManagerKing>();
    }
    protected virtual void LoadMovement()
    {
        if (this.movementKing != null) return;
        this.movementKing = this.GetComponentInChildren<MovementKing>();
    }
    protected virtual void LoadAbilityNormalATK()
    {
        if (this.abiKingNormalATK != null) return;
        this.abiKingNormalATK = this.GetComponentInChildren<AbiNormalATKKing>();
    }
    protected virtual void LoadDetectObstacle()
    {
        if (this.abiDetectKing != null) return;
        this.abiDetectKing = this.GetComponentInChildren<AbiDetectKing>();
    }
    protected virtual void LoadKingAI()
    {
        if (this.kingAI != null) return;
        this.kingAI = this.GetComponentInChildren<KingAI>();
    }

    public HealthBar HealthBar
    {
        get { return this.healthBar; }
    }
    public AnimationManager AnimationManager
    {
        get { return this.animationManager; }
    }
    public CollisionKing CollisionKing
    {
        get { return this.collisionKing; }
    }
    public DamageManagerKing DamageManagerKing
    {
        get { return this.damageManagerKing; }
    }
    public KingStats KingStats
    {
        get { return this.kingStats; }
    }
    public PhysicsKing PhysicsKing
    {
        get { return this.physicsKing; }
    }
    public AbiDetectKing AbiDetectKing
    {
        get { return this.abiDetectKing; }
    }
    public MovementKing MovementKing
    {
        get { return this.movementKing; }
    }
    public AbiNormalATKKing AbiKingNormalATK
    {
        get { return this.abiKingNormalATK; }
    }
    public KingAI KingAI
    {
        get { return this.kingAI; }
    }

    //States
    public StateManager StateManager
    {
        get { return this.stateManager; }
    }
    public KingChasePlayerState ChasePlayerState
    {
        get { return this.chaseState; }
    }
    public KingRunToDoorState RunToDoorState
    {
        get { return this.runToDoorState; }
    }
    public KingNormalATKState NormalATKState
    {
        get { return this.normalATKState; }
    }
    public KingDoorInState DoorInState
    {
        get { return this.doorInState; }
    }
    public KingDoorOutState DoorOutState
    {
        get { return this.doorOutState; }
    }

    // Target is the player
    public GameObject Target
    {
        get { return FindObjectOfType<PlayerController>().gameObject; }
    }



}
