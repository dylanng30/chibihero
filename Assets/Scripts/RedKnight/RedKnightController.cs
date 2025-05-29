using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(StateManager))]
public class RedKnightController : MonoBehaviour
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

    //States
    [SerializeField] private StateManager stateManager;

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
    }
    private void LoadStates()
    {
        stateManager = GetComponent<StateManager>();


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

    //States
    public StateManager StateManager
    {
        get
        {
            return stateManager;
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
