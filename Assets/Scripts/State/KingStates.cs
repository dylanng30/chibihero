using System.Collections;
using UnityEngine;

public class KingChasePlayerState : IState
{
    private KingController kingController;
    private string currentState = "Run";
    public KingChasePlayerState(KingController kingController)
    {
        this.kingController = kingController;
    }
    public void Enter()
    {
        //Debug.Log("KingChasePlayerState: Enter");
        kingController.AnimationManager.SetAnimation(currentState);
        DoorManager.Instance.LockDoor(true);
    }
    public void Execute()
    {
        kingController.MovementKing.FLipToPlayer();

        if (kingController.AbiDetectKing.NextToWall())
        {
            kingController.MovementKing.Flee();
            return;
        }

        if (kingController.AbiDetectKing.DetectObstacle() && !kingController.AbiDetectKing.NextToWall())
        {
            kingController.MovementKing.Jump();
            return;
        }

        if (kingController.AbiKingNormalATK.CanAttack())
            kingController.StateManager.ChangeState(kingController.NormalATKState);

        kingController.MovementKing.Moving();
    }
    public void Exit()
    {

    }
}

public class KingNormalATKState : IState
{
    private KingController kingController;
    private string currentState = "NormalATK";
    public KingNormalATKState(KingController kingController)
    {
        this.kingController = kingController;
    }
    public void Enter()
    {
        //Debug.Log("KingNormalATKState: Enter");
        kingController.AnimationManager.SetAnimation(currentState);
        kingController.PhysicsKing.Rigidbody2D.velocity = Vector2.zero;
    }
    public void Execute()
    {
        if (kingController.AnimationManager.FinishAnimation(currentState))
            kingController.StateManager.ChangeState(kingController.RunToDoorState);

    }
    public void Exit()
    {
        kingController.AbiKingNormalATK.NormalATK();
    }
}

public class KingRunToDoorState : IState
{
    private KingController kingController;
    private string currentState = "Run";
    public KingRunToDoorState(KingController kingController)
    {
        this.kingController = kingController;
    }
    public void Enter()
    {
        //Debug.Log("KingRunToDoorState: Enter");
        kingController.AnimationManager.SetAnimation(currentState);

        DoorManager.Instance.LockDoor(false);
        DoorManager.Instance.KingIsReadyToTransform(true);
    }
    public void Execute()
    {
        kingController.MovementKing.Flip();

        if (kingController.AbiDetectKing.NextToWall())
        {
            kingController.MovementKing.Moving();
            return;
        }
        else if (kingController.AbiDetectKing.DetectObstacle() && !kingController.AbiDetectKing.NextToWall())
        {
            kingController.MovementKing.Jump();
            return;
        }

        kingController.MovementKing.MoveToNearestDoor();
    }
    public void Exit()
    {

    }
}
public class KingDoorInState : IState
{
    private KingController kingController;
    private string currentState = "DoorIn";
    public KingDoorInState(KingController kingController)
    {
        this.kingController = kingController;
    }
    public void Enter()
    {
        //Debug.Log("KingDoorInState: Enter");
        kingController.AnimationManager.SetAnimation(currentState);
        kingController.PhysicsKing.Rigidbody2D.velocity = Vector2.zero;
        DoorController lastDoor = kingController.AbiDetectKing.NearestDoor;
        lastDoor.StateManager.ChangeState(lastDoor.OpenState);
    }
    public void Execute()
    {
        if (kingController.AnimationManager.FinishAnimation(currentState))
            kingController.StateManager.ChangeState(kingController.DoorOutState);  
                 
    }
    public void Exit()
    {
        
        kingController.MovementKing.TeleportToAnotherDoor();
    }
}
public class KingDoorOutState : IState
{
    private KingController kingController;
    private string currentState = "DoorOut";
    public KingDoorOutState(KingController kingController)
    {
        this.kingController = kingController;
    }
    public void Enter()
    {
        //Debug.Log("KingDoorOutState: Enter");
        kingController.AnimationManager.SetAnimation(currentState);
        DoorManager.Instance.KingIsReadyToTransform(false);
    }
    public void Execute()
    {
        if (kingController.AnimationManager.FinishAnimation(currentState))
            kingController.StateManager.ChangeState(kingController.ChasePlayerState);
    }
    public void Exit()
    {
        DoorManager.Instance.LockDoor(true);        
    }
}

public class KingHitState : IState
{
    private KingController kingController;
    private string currentState = "Hit";
    public KingHitState(KingController kingController)
    {
        this.kingController = kingController;
    }
    public void Enter()
    {
        kingController.AnimationManager.SetAnimation(currentState);
    }
    public void Execute()
    {

    }
    public void Exit()
    {

    }
}

