using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;

[RequireComponent(typeof(StateManager))]

public class PirateController : MonoBehaviour, IDamagable
{
    [SerializeField] protected AnimationManager animationPirate;
    [SerializeField] protected CollisionPirate collisionPirate;
    [SerializeField] protected PirateStats pirateStats;
    [SerializeField] protected PhysicsPirate physicsPirate;
    [SerializeField] protected PirateATKAbility pirateATKAbility;
    [SerializeField] protected PirateMovement pirateMovement;
    [SerializeField] protected PirateDetectObstacle pirateDetectObstacle;
    [SerializeField] protected PirateDamageManager pirateDamageManager;

    [SerializeField] protected StateManager stateManager;
    [SerializeField] protected PirateIdleState idleState;
    [SerializeField] protected PirateRunState runState;
    [SerializeField] protected PirateFleeState fleeState;
    [SerializeField] protected PirateNormalATKState normalATKState;
    [SerializeField] protected PirateRangeATKState rangeATKState;


    void Awake()
    {
        this.LoadComponent();
        this.LoadState();
    }
    protected void LoadState()
    {
        stateManager = this.GetComponent<StateManager>();
        idleState = new PirateIdleState(this);
        runState = new PirateRunState(this);
        fleeState = new PirateFleeState(this);
        normalATKState = new PirateNormalATKState(this);
        rangeATKState = new PirateRangeATKState(this);
        this.stateManager.ChangeState(idleState);
    }

    private void LoadComponent()
    {
        LoadAnimationPirate();
        LoadCollisionPirate();
        LoadPirateStats();
        LoadPhysicsPirate();
        LoadPirateDetectObstacle();
        LoadDamageManagerPirate();
        LoadAbilityPirateATK();
        LoadMovementPirate();
    }
    protected virtual void LoadAnimationPirate()
    {
        if (this.animationPirate != null) return;
        this.animationPirate = this.GetComponentInChildren<AnimationManager>();
    }
    protected virtual void LoadCollisionPirate()
    {
        if (this.collisionPirate != null) return;
        this.collisionPirate = this.GetComponentInChildren<CollisionPirate>();
    }
    protected virtual void LoadPirateStats()
    {
        if (this.pirateStats != null) return;
        this.pirateStats = this.GetComponentInChildren<PirateStats>();
    }
    protected virtual void LoadPhysicsPirate()
    {
        if (this.physicsPirate != null) return;
        this.physicsPirate = this.GetComponentInChildren<PhysicsPirate>();
    }
    protected virtual void LoadDamageManagerPirate()
    {
        if (this.pirateDamageManager != null) return;
        this.pirateDamageManager = this.GetComponentInChildren<PirateDamageManager>();
    }
    protected virtual void LoadMovementPirate()
    {
        if (this.pirateMovement != null) return;
        this.pirateMovement = this.GetComponentInChildren<PirateMovement>();
    }
    protected virtual void LoadAbilityPirateATK()
    {
        if (this.pirateATKAbility != null) return;
        this.pirateATKAbility = this.GetComponentInChildren<PirateATKAbility>();
    }
    protected virtual void LoadPirateDetectObstacle()
    {
        if (this.pirateDetectObstacle != null) return;
        this.pirateDetectObstacle = this.GetComponentInChildren<PirateDetectObstacle>();
    }

    public void TakeDamage(int damage, GameObject attacker)
    {
        pirateDamageManager.TakeDamage(damage, attacker);
    }

    public AnimationManager AnimationPirate
    {
        get { return this.animationPirate; }
    }
    public CollisionPirate CollisionPirate
    {
        get { return this.collisionPirate; }
    }
    public PirateDamageManager PirateDamageManager
    {
        get { return pirateDamageManager; }
    }
    public PirateStats PirateStats
    {
        get { return this.pirateStats; }
    }
    public PhysicsPirate PhysicsPirate
    {
        get { return this.physicsPirate; }
    }
    public PirateDetectObstacle PirateDetectObstacle
    {
        get { return this.pirateDetectObstacle; }
    }
    public PirateMovement PirateMovement
    {
        get { return this.pirateMovement; }
    }
    public PirateATKAbility PirateATKAbility
    {
        get { return this.pirateATKAbility; }
    }

    //States
    public StateManager StateManager
    {
        get { return this.stateManager; }
    }
    public PirateIdleState IdleState
    {
        get { return this.idleState; }
    }
    public PirateRunState RunState
    {
        get { return this.runState; }
    }
    public PirateFleeState FleeState
    {
        get { return this.fleeState; }
    }
    public PirateNormalATKState NormalATKState
    {
        get { return this.normalATKState; }
    }
    public PirateRangeATKState RangeATKState
    {
        get { return this.rangeATKState; }
    }

    // Target is the player
    public GameObject Target
    {
        get { return GameObject.FindGameObjectWithTag("Player"); }
    }

}
