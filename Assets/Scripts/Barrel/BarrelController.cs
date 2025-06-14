using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrelController : MonoBehaviour
{
    private float moveRange = 5f;

    //Component
    private Rigidbody2D physics;
    private AnimationManager animationManager;
    private MovementBarrel movementBarrel;
    private BoomBarrel boomAbi;
    private BarrelStats barrelStats;

    //States
    private StateManager stateManager;
    private BarrelIdle idleState;
    private BarrelRun runState;
    private BarrelPreExplosion preExplosionState;
    private BarrelExplosion explosionState;

    private void Start()
    {
        LoadComponents();
        LoadStates();
    }
    private void LoadComponents()
    {
        LoadAnimator();
        LoadPhysics();
        LoadBoomAbi();
        LoadMovement();
        LoadStats();
    }
    private void LoadStates()
    {
        LoadStateManager();
        LoadIdleState();
        LoadRunState();
        LoadPreExplosionState();
        LoadExplosionState();
        stateManager .ChangeState(idleState);
    }

    //States
    private void LoadStateManager()
    {
        if (stateManager != null) return;
        stateManager = GetComponent<StateManager>();        
    }
    private void LoadIdleState()
    {
        if (idleState != null) return;
        idleState = new BarrelIdle(this);
    }
    private void LoadRunState()
    {
        if (runState != null) return;
        runState = new BarrelRun(this);
    }
    private void LoadPreExplosionState()
    {
        if (preExplosionState != null) return;
        preExplosionState = new BarrelPreExplosion(this);
    }
    private void LoadExplosionState()
    {
        if (explosionState != null) return;
        explosionState = new BarrelExplosion(this);
    }

    //Components
    private void LoadAnimator()
    {
        if (animationManager != null) return;
        animationManager = GetComponentInChildren<AnimationManager>();
    }
    private void LoadPhysics()
    {
        if (physics != null) return;
        physics = GetComponent<Rigidbody2D>();
    }
    private void LoadBoomAbi()
    {
        if(boomAbi != null) return;
        boomAbi = GetComponentInChildren<BoomBarrel>();
    }
    private void LoadMovement()
    {
        if (movementBarrel != null) return;
        movementBarrel = GetComponentInChildren<MovementBarrel>();
    }
    private void LoadStats()
    {
        if(barrelStats != null) return;
        barrelStats = GetComponentInChildren<BarrelStats>();
    }

    public AnimationManager AnimationManager { get { return animationManager; } }
    public Rigidbody2D Rigidbody2D { get { return this.physics; } }
    public MovementBarrel Movement { get { return this.movementBarrel; } }
    public BoomBarrel BoomAbi { get { return boomAbi; } }
    public BarrelStats BarrelStats { get { return barrelStats; } }
    
    public StateManager StateManager {  get { return this.stateManager; } }
    public BarrelIdle IdleState {  get { return this.idleState; } }
    public BarrelRun RunState { get { return this.runState; } }
    public BarrelPreExplosion PreExplosionState { get { return this.preExplosionState; } }
    public BarrelExplosion ExplosionState { get { return this.explosionState; } }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(this.transform.position, moveRange);
    }
}
