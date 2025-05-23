using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;

[RequireComponent(typeof(StateManager))]

public class PirateController : MonoBehaviour
{
    [SerializeField] protected AnimationManager animationPirate;
    [SerializeField] protected CollisionPirate collisionPirate;
    [SerializeField] protected PirateStats pirateStats;
    [SerializeField] protected PhysicsPirate physicsPirate;

    [SerializeField] protected StateManager stateManager;
    [SerializeField] protected PirateIdleState idleState;
    [SerializeField] protected PirateRunState runState;
    [SerializeField] protected PirateJumpState jumpState;
    [SerializeField] protected PirateFallState fallState;
    [SerializeField] protected PirateHitState hitState;
    [SerializeField] protected PirateNormalATKState normalATKState;
    [SerializeField] protected PirateAttackUpState attackUpState;
    [SerializeField] protected PirateAttackDownState attackDownState;
    [SerializeField] protected PirateRangeAttackState rangeAttackState;


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
        jumpState = new PirateJumpState(this);
        fallState = new PirateFallState(this);
        hitState = new PirateHitState(this);
        normalATKState = new PirateNormalATKState(this);
        attackUpState = new PirateAttackUpState(this);
        attackDownState = new PirateAttackDownState(this);
        rangeAttackState = new PirateRangeAttackState(this);
        this.stateManager.ChangeState(idleState);
    }

    private void LoadComponent()
    {
        LoadAnimationPirate();
        LoadCollisionPirate();
        /*LoadPhysicsEnemy();
        LoadCollisionEnemy();
        LoadEnemyStats();
        LoadDamageManagerEnemy();
        LoadMovementEnemy();
        LoadAnimationEnemy();
        LoadAbilityEnemyNormalATK();
        LoadEnemyDetectObstacle();*/
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
    protected virtual void LoadPhysicsPirate()
    {
        if (this.physicsPirate != null) return;
        this.physicsPirate = this.GetComponentInChildren<PhysicsPirate>();
    }


    public AnimationManager AnimationPirate
    {
        get { return this.animationPirate; }
    }
    public CollisionPirate CollisionPirate
    {
        get { return this.collisionPirate; }
    }
    public PirateStats PirateStats
    {
        get { return this.pirateStats; }
    }

}
