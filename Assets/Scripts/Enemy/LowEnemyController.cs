using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LowEnemyController : MonoBehaviour
{
    [SerializeField] protected PhysicsEnemy physicsEnemy;
    [SerializeField] protected CollisionEnemy collisionEnemy;
    [SerializeField] protected DamageManagerEnemy damageManager;
    [SerializeField] protected EnemyStats enemyStats;
    [SerializeField] protected MovementEnemy movementEnemy;
    /*[SerializeField] protected AnimationPlayer animationPlayer;
    [SerializeField] protected PlayerStats playerStats;    
    [SerializeField] protected MovementPlayer movementPlayer;
    [SerializeField] protected AbilityNormalATK abilityNormalATK;
    [SerializeField] protected AbilitySkill abilitySkill;*/

    void Awake()
    {
        this.LoadComponent();
        //this.LoadState();
    }
    /*protected void LoadState()
    {
        stateManager = this.GetComponent<StateManager>();
        idleState = new IdleState(this);
        runState = new RunState(this);
        normalATKState = new NormalATKState(this);
        skillState = new SkillState(this);
        this.stateManager.ChangeState(idleState);
    }*/

    private void LoadComponent()
    {
        LoadPhysicsEnemy();
        LoadCollisionEnemy();
        LoadEnemyStats();
        LoadDamageManagerEnemy();        
        LoadMovementEnemy();
        /*LoadAnimationPlayer();
        LoadPlayerStat();        
        LoadMovementPlayer();
        LoadAbilityNormalATK();
        LoadAbilitySkill();*/
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
    /*protected virtual void LoadAnimationEnemy()
    {
        if (this.animationPlayer != null) return;
        this.collisionEnemy = this.GetComponentInChildren<CollisionEnemy>();
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
    }*/


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

    /*public AnimationEnemy AnimationPlayer
    {
        get { return animationPlayer; }
    }
    
    public EnemyPlayer MovementPlayer
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
    }*/

    public GameObject Target
    {
        get { return GameObject.FindGameObjectWithTag("Player"); }
    }
}
