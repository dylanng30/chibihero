using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(StateManager))]
public class DoorController : MonoBehaviour
{
    [SerializeField] protected AnimationManager animationManager;
    [SerializeField] protected CollisionDoor collisionDoor;
    [SerializeField] protected TransformKingAbi transformKingAbi;

    [SerializeField] protected StateManager stateManager;
    [SerializeField] protected DoorIdleState idleState;
    [SerializeField] protected DoorOpenState openState;
    [SerializeField] protected DoorCloseState closeState;

    void Start()
    {
        LoadComponents();
        LoadStates();
    }
    public void PlayerInFrontOfDoor(bool isOpened)
    {
        if (isOpened)
            this.stateManager.ChangeState(openState);
        else
            this.stateManager.ChangeState(idleState);
    }   

    private void LoadStates()
    {
        LoadStateManager();
        LoadIdleState();
        LoadOpenState();
        LoadCloseState();

    }
    private void LoadStateManager()
    {
        if (this.stateManager != null) return;
        this.stateManager = this.GetComponent<StateManager>();
    }
    private void LoadIdleState()
    {
        if (this.idleState != null) return;
        this.idleState = new DoorIdleState(this);
        this.stateManager.ChangeState(idleState);
    }
    private void LoadOpenState()
    {
        if (this.openState != null) return;
        this.openState = new DoorOpenState(this);
    }
    private void LoadCloseState()
    {
        if (this.closeState != null) return;
        this.closeState = new DoorCloseState(this);
    }

    private void LoadComponents()
    {
        LoadAnimation();
        LoadCollision();
        LoadTransformKingAbi();
    }
    protected virtual void LoadAnimation()
    {
        if (this.animationManager != null) return;
        this.animationManager = this.GetComponentInChildren<AnimationManager>();
    }
    protected virtual void LoadCollision()
    {
        if (this.collisionDoor != null) return;
        this.collisionDoor = this.GetComponentInChildren<CollisionDoor>();
    }
    protected virtual void LoadTransformKingAbi()
    {
        if (this.transformKingAbi != null) return;
        this.transformKingAbi = this.GetComponentInChildren<TransformKingAbi>();
    }
    public TransformKingAbi TransformKingAbi
    {
        get { return transformKingAbi; }
    }

    public CollisionDoor CollisionDoor
    {
        get { return collisionDoor; }
    }
    public AnimationManager AnimationManager
    {
        get { return animationManager; }
    }
    public StateManager StateManager
    {
        get { return stateManager; }
    }
    public DoorIdleState IdleState
    {
        get { return idleState; }
    }
    public DoorOpenState OpenState
    {
        get { return openState; }
    }
    public DoorCloseState CloseState
    {
        get { return closeState; }
    }
}
