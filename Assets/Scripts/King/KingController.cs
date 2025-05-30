using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KingController : MonoBehaviour
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

    //States
    [SerializeField] protected StateManager stateManager;
    [SerializeField] protected KingIdleState idleState;
    [SerializeField] protected KingRunState runState;
    [SerializeField] protected KingFleeState fleeState;
    [SerializeField] protected KingNormalATKState normalATKState;
    [SerializeField] protected KingJumpState jumpState;
    [SerializeField] protected KingShootState rangeATKState;
    [SerializeField] protected KingFallState fallState;
    [SerializeField] protected KingDoorInState doorInState;
    [SerializeField] protected KingDoorOutState doorOutState;

    private List<IState> states = new List<IState>();

    void Awake()
    {
        this.LoadComponent();
        this.LoadState();
    }
    protected void LoadState()
    {
        stateManager = this.GetComponent<StateManager>();
        idleState = new KingIdleState(this);
        runState = new KingRunState(this);
        states.Add(runState);
        fleeState = new KingFleeState(this);
        states.Add(fleeState);
        normalATKState = new KingNormalATKState(this);
        states.Add(normalATKState);
        jumpState = new KingJumpState(this);
        states.Add(jumpState);
        rangeATKState = new KingShootState(this);
        states.Add(rangeATKState);
        fallState = new KingFallState(this);
        doorInState = new KingDoorInState(this);
        doorOutState = new KingDoorOutState(this);
        stateManager.ChangeState(idleState);
    }

    private void LoadComponent()
    {
        LoadAnimation();
        LoadCollision();
        LoadStats();
        LoadPhysics();
        LoadDetectObstacle();
        LoadDamageManager();
        LoadAbilityNormalATK();
        LoadDetectObstacle();
        LoadKingAI();
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
        this.physicsKing = this.GetComponentInChildren<PhysicsKing>();
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
    public void TakeDamage(int damage, GameObject attacker)
    {
        damageManagerKing.TakeDamage(damage, attacker);
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
    public KingIdleState IdleState
    {
        get { return this.idleState; }
    }
    public KingRunState RunState
    {
        get { return this.runState; }
    }
    public KingFleeState FleeState
    {
        get { return this.fleeState; }
    }
    public KingNormalATKState NormalATKState
    {
        get { return this.normalATKState; }
    }
    public KingJumpState JumpState
    {
        get { return this.jumpState; }
    }
    public KingShootState RangeATKState
    {
        get { return this.rangeATKState; }
    }
    public KingFallState FallState
    {
        get { return this.fallState; }
    }
    public KingDoorInState DoorInState
    {
        get { return this.doorInState; }
    }
    public KingDoorOutState DoorOutState
    {
        get { return this.doorOutState; }
    }
    public List<IState> States
    {
        get { return this.states; }
    }

    // Target is the player
    public GameObject Target
    {
        get { return GameObject.FindGameObjectWithTag("Player"); }
    }



}
