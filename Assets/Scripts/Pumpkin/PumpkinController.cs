using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PumpkinController : MonoBehaviour, IDamagable
{
    [SerializeField] protected CollisionPumpkin collision;
    [SerializeField] protected DamageManagerPumpkin damageManager;
    [SerializeField] protected PumpkinStats stats;
    [SerializeField] protected AnimationManager animationManager;
    [SerializeField] protected ATKAbiPumpkin atkAbi;
    [SerializeField] protected PumpkinAI pumpkinAI;
    [SerializeField] protected HealthBar healthBar;

    //States
    [SerializeField] protected StateManager stateManager;

    void Awake()
    {
        Load();
    }
    private void Load()
    {
        this.LoadComponent();
        this.LoadState();
    }
    public void TakeDamage(int damage, GameObject attacker)
    {
        damageManager.TakeDamage(damage, attacker);
    }
    protected void LoadState()
    {
        stateManager = this.GetComponent<StateManager>();

        //stateManager.ChangeState(doorOutState);
    }

    private void LoadComponent()
    {
        LoadStats();
        LoadAnimation();
        LoadCollision();        
        LoadDamageManager();
        LoadAbilityNormalATK();
        LoadAI();
        LoadHeathBar();
    }

    protected virtual void LoadHeathBar()
    {
        if (this.healthBar != null) return;
        healthBar = GetComponentInChildren<HealthBar>();
    }
    protected virtual void LoadAnimation()
    {
        if (this.animationManager != null) return;
        this.animationManager = this.GetComponentInChildren<AnimationManager>();
    }
    protected virtual void LoadCollision()
    {
        if (this.collision != null) return;
        this.collision = this.GetComponentInChildren<CollisionPumpkin>();
    }
    protected virtual void LoadStats()
    {
        if (this.stats != null) return;
        this.stats = this.GetComponentInChildren<PumpkinStats>();
    }
    protected virtual void LoadDamageManager()
    {
        if (this.damageManager != null) return;
        this.damageManager = this.GetComponentInChildren<DamageManagerPumpkin>();
    }
    protected virtual void LoadAbilityNormalATK()
    {
        if (this.atkAbi != null) return;
        this.atkAbi = this.GetComponentInChildren<ATKAbiPumpkin>();
    }
    protected virtual void LoadAI()
    {
        if (this.pumpkinAI != null) return;
        this.pumpkinAI = this.GetComponentInChildren<PumpkinAI>();
    }

    public HealthBar HealthBar
    {
        get { return this.healthBar; }
    }
    public AnimationManager AnimationManager
    {
        get { return this.animationManager; }
    }
    public CollisionPumpkin Collision
    {
        get { return this.collision; }
    }
    public DamageManagerPumpkin DamageManager
    {
        get { return this.damageManager; }
    }
    public PumpkinStats Stats
    {
        get { return this.stats; }
    }
    public ATKAbiPumpkin ATKAbi
    {
        get { return this.atkAbi; }
    }
    public PumpkinAI AI
    {
        get { return this.pumpkinAI; }
    }

    //States
    public StateManager StateManager
    {
        get { return this.stateManager; }
    }


    // Target is the player
    public GameObject Target
    {
        get { return FindObjectOfType<PlayerController>().gameObject; }
    }
}
