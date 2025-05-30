using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(StateManager))]
public class RedKnightController : MonoBehaviour, IDamagable
{
    //Components
    [SerializeField] private AnimationManager animationManager;
    [SerializeField] private PhysicRedKnight physicRedKnight;
    [SerializeField] private CollisionRedKnight collisionRedKnight;
    [SerializeField] private AbiDetectRedKnight abiDetectRedKnight;
    [SerializeField] private RedKnightStats redKnightStats;
    [SerializeField] private AbiRangeATKRedKnight abiRangeATKRedKnight;
    [SerializeField] private AbiNormalATKRedKnight abiNormalATKRedKnight;
    [SerializeField] private MovementRedKnight movementRedKnight;
    [SerializeField] private RedKnightAI redKnightAI;
    [SerializeField] private DamageManagerRedKnight damageManager;

    //States
    [SerializeField] private StateManager stateManager;
    [SerializeField] private RKIdleState idleState;
    [SerializeField] private RKRunState runState;
    [SerializeField] private RKShootState shootState;
    [SerializeField] private RKNormalATKState normalATKState;
    [SerializeField] private RKFleeState fleeState;

    private List<IState> states = new List<IState>();

    private void Awake()
    {
        LoadComponents();
        LoadStates();
    }
    private void LoadComponents()
    {
        LoadAnimator();
        LoadPhysicRedKnight();
        LoadCollisionRedKnight();
        LoadAbiDetectRedKnight();
        LoadRedKnightStats();
        LoadAbiRangeATKRedKnight();
        LoadAbiNormalATKRedKnight();
        LoadMovementRedKnight();
        LoadRedKnightAI();
        LoadDamageManager();
    }
    private void LoadStates()
    {
        stateManager = GetComponent<StateManager>();
        idleState = new RKIdleState(this);
        runState = new RKRunState(this);
        shootState = new RKShootState(this);
        normalATKState = new RKNormalATKState(this);
        fleeState = new RKFleeState(this);

        states.Add(runState);
        states.Add(fleeState);
        states.Add(shootState);
        states.Add(idleState);

        stateManager.ChangeState(idleState);
    }

    public void TakeDamage(int damage, GameObject attacker)
    {
        damageManager.TakeDamage(damage, attacker);
    }

    //Load Components
    protected virtual void LoadAnimator()
    {
        if (animationManager != null) return;
        animationManager = GetComponentInChildren<AnimationManager>();
    }
    protected virtual void LoadPhysicRedKnight()
    {
        if (physicRedKnight != null) return;
        physicRedKnight = GetComponent<PhysicRedKnight>();
    }
    protected virtual void LoadCollisionRedKnight()
    {
        if (collisionRedKnight != null) return;
        collisionRedKnight = GetComponentInChildren<CollisionRedKnight>();
    }
    protected virtual void LoadAbiDetectRedKnight()
    {
        if (abiDetectRedKnight != null) return;
        abiDetectRedKnight = GetComponentInChildren<AbiDetectRedKnight>();
    }
    protected virtual void LoadRedKnightStats()
    {
        if (redKnightStats != null) return;
        redKnightStats = GetComponentInChildren<RedKnightStats>();
    }
    protected virtual void LoadAbiRangeATKRedKnight()
    {
        if (abiRangeATKRedKnight != null) return;
        abiRangeATKRedKnight = GetComponentInChildren<AbiRangeATKRedKnight>();
    }
    protected virtual void LoadAbiNormalATKRedKnight()
    {
        if (abiNormalATKRedKnight != null) return;
        abiNormalATKRedKnight = GetComponentInChildren<AbiNormalATKRedKnight>();
    }
    protected virtual void LoadMovementRedKnight()
    {
        if (movementRedKnight != null) return;
        movementRedKnight = GetComponentInChildren<MovementRedKnight>();
    }
    protected virtual void LoadRedKnightAI()
    {
        if (redKnightAI != null) return;
        redKnightAI = GetComponentInChildren<RedKnightAI>();
    }
    protected virtual void LoadDamageManager()
    {
        if (damageManager != null) return;
        damageManager = GetComponentInChildren<DamageManagerRedKnight>();
    }

    public RedKnightStats RedKnightStats
    {
        get
        {
            return redKnightStats;
        }
    }
    public AnimationManager AnimationManager
    {
        get
        {
            return animationManager;
        }
    }
    public PhysicRedKnight PhysicRedKnight
    {
        get
        {
            return physicRedKnight;
        }
    }
    public CollisionRedKnight CollisionRedKnight
    {
        get
        {
            return collisionRedKnight;
        }
    }
    public AbiDetectRedKnight AbiDetectRedKnight
    {
        get
        {
            return abiDetectRedKnight;
        }
    }
    public AbiRangeATKRedKnight AbiRangeATKRedKnight
    {
        get
        {
            return abiRangeATKRedKnight;
        }
    }
    public AbiNormalATKRedKnight AbiNormalATKRedKnight
    {
        get
        {
            return abiNormalATKRedKnight;
        }
    }
    public MovementRedKnight MovementRedKnight
    {
        get
        {
            return movementRedKnight;
        }
    }
    public RedKnightAI RedKnightAI
    {
        get
        {
            return redKnightAI;
        }
    }
    public DamageManagerRedKnight DamageManager
    {
        get
        {
            return damageManager;
        }
    }

    //States
    public StateManager StateManager
    {
        get
        {
            return stateManager;
        }
    }
    public RKIdleState IdleState
    {
        get
        {
            return idleState;
        }
    }
    public RKRunState RunState
    {
        get
        {
            return runState;
        }
    }
    public RKShootState ShootState
    {
        get
        {
            return shootState;
        }
    }
    public RKNormalATKState NormalATKState
    {
        get
        {
            return normalATKState;
        }
    }
    public RKFleeState FleeState
    {
        get
        {
            return fleeState;
        }
    }

    public List<IState> States
    {
        get
        {
            return states;
        }
    }

    //Target
    public GameObject Target
    {
        get
        {
            return GameObject.FindGameObjectWithTag("Player");
        }
    }

}
