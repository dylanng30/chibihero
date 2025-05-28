using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LowEnemyController : MonoBehaviour, IDamagable
{
    [SerializeField] protected PhysicsEnemy physicsEnemy;
    [SerializeField] protected CollisionEnemy collisionEnemy;
    [SerializeField] protected DamageManagerEnemy damageManager;
    [SerializeField] protected EnemyStats enemyStats;
    [SerializeField] protected MovementEnemy movementEnemy;
    [SerializeField] protected AnimationManager animationEnemy;
    [SerializeField] protected AbilityEnemyNormalATK abilityNormalATK;
    [SerializeField] protected EnemyDetectObstacle enemyDetectObstacle;

    private EIdleState idleState;
    private ERunState runState;
    private ENormalATKState normalATKState;
    private StateManager stateManager;



    void Awake()
    {
        this.LoadComponent();
        this.LoadState();
    }
    protected void LoadState()
    {
        stateManager = this.GetComponent<StateManager>();
        idleState = new EIdleState(this);
        runState = new ERunState(this);
        normalATKState = new ENormalATKState(this);
        this.stateManager.ChangeState(idleState);
    }

    private void LoadComponent()
    {
        LoadPhysicsEnemy();
        LoadCollisionEnemy();
        LoadEnemyStats();
        LoadDamageManagerEnemy();        
        LoadMovementEnemy();
        LoadAnimationEnemy();
        LoadAbilityEnemyNormalATK();
        LoadEnemyDetectObstacle();
    }

    //Load Component
    protected virtual void LoadPhysicsEnemy()
    {
        if (this.physicsEnemy != null) return;
        this.physicsEnemy = this.GetComponent<PhysicsEnemy>();
    }
    protected virtual void LoadCollisionEnemy()
    {
        if (this.collisionEnemy != null) return;
        this.collisionEnemy = this.GetComponentInChildren<CollisionEnemy>();
    }
    protected virtual void LoadDamageManagerEnemy()
    {
        if (this.damageManager != null) return;
        this.damageManager = this.GetComponentInChildren<DamageManagerEnemy>();
    }
    protected virtual void LoadEnemyStats()
    {
        if (this.enemyStats != null) return;
        this.enemyStats = this.GetComponentInChildren<EnemyStats>();
    }
    protected virtual void LoadMovementEnemy()
    {
        if (this.movementEnemy != null) return;
        this.movementEnemy = this.GetComponentInChildren<MovementEnemy>();
    }
    protected virtual void LoadAnimationEnemy()
    {
        if (this.animationEnemy != null) return;
        this.animationEnemy = this.GetComponentInChildren<AnimationManager>();
    }
    protected void LoadAbilityEnemyNormalATK()
    {
        if (this.abilityNormalATK != null) return;
        this.abilityNormalATK = this.GetComponentInChildren<AbilityEnemyNormalATK>();
    }
    protected void LoadEnemyDetectObstacle()
    {
        if (this.enemyDetectObstacle != null) return;
        this.enemyDetectObstacle = this.GetComponentInChildren<EnemyDetectObstacle>();
    }

    public void TakeDamage(int damage, GameObject attacker)
    {
        damageManager.TakeDamage(damage, attacker);
    }
    //Components
    public PhysicsEnemy PhysicsEnemy
    {
        get { return physicsEnemy; }
    }
    public CollisionEnemy CollisionPlayer
    {
        get { return collisionEnemy; }
    }
    public DamageManagerEnemy DamageManager
    {
        get { return damageManager; }
    }
    public EnemyStats EnemyStats
    {
        get { return enemyStats; }
    }
    public MovementEnemy MovementEnemy
    {
        get { return movementEnemy; }
    }
    public AnimationManager AnimationEnemy
    {
        get { return animationEnemy; }
    }
    public AbilityEnemyNormalATK AbilityNormalATK
    {
        get { return abilityNormalATK; }
    }
    public EnemyDetectObstacle EnemyDetectObstacle
    {
        get { return enemyDetectObstacle; }
    }
    //States    
    public StateManager StateManager
    {
        get { return stateManager; }
    }
    public EIdleState IdleState
    {
        get { return idleState; }
    }
    public ERunState RunState
    {
        get { return runState; }
    }
    public ENormalATKState NormalATKState
    {
        get { return normalATKState; }
    }
    //Target
    public GameObject Target
    {
        get { return GameObject.FindGameObjectWithTag("Player"); }
    }
}
