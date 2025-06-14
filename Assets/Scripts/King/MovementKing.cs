using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementKing : MonoBehaviour
{
    [SerializeField] protected KingController kingController;

    private Vector2 target;
    private Vector2 origin;
    private Vector2 direction;

    private void Start()
    {
        LoadComponent();
    }
    protected void LoadComponent()
    {
        LoadController();
    }
    protected virtual void LoadController()
    {
        if (this.kingController != null)
            return;
        this.kingController = GetComponentInParent<KingController>();
    }
    public void Moving()
    {
        origin = kingController.transform.position;
        target = kingController.Target.transform.position;
        int speed = kingController.KingStats.MoveSpeed;
        direction = (target - origin).normalized;
        kingController.PhysicsKing.Rigidbody2D.velocity = new Vector2(direction.x * speed, kingController.PhysicsKing.Rigidbody2D.velocity.y);
        //Debug.Log(kingController.PhysicsKing.Rigidbody2D.velocity);
    }
    public void MoveToNearestDoor()
    {
        target = kingController.AbiDetectKing.NearestDoor.transform.position;
        origin = kingController.transform.position;
        direction = (target - origin).normalized;
        int speed = kingController.KingStats.MoveSpeed;
        kingController.PhysicsKing.Rigidbody2D.velocity = new Vector2(direction.x * speed, kingController.PhysicsKing.Rigidbody2D.velocity.y);
    }

    public void TeleportToAnotherDoor()
    {
        DoorController nextDoor = kingController.AbiDetectKing.NextDoor;
        kingController.transform.position = nextDoor.transform.position;
        nextDoor.StateManager.ChangeState(nextDoor.OpenState);
    }

    public void Flee()
    {
        origin = kingController.transform.position;
        target = kingController.Target.transform.position;
        int speed = kingController.KingStats.MoveSpeed;
        direction = (origin - target).normalized;
        kingController.PhysicsKing.Rigidbody2D.velocity = new Vector2(direction.x * speed, kingController.PhysicsKing.Rigidbody2D.velocity.y);
    }

    public void Jump()
    {
        if(!kingController.CollisionKing.IsGrounded())
            return;

        int jumpPower = kingController.KingStats.JumpPower;
        kingController.PhysicsKing.Rigidbody2D.velocity = new Vector2(kingController.PhysicsKing.Rigidbody2D.velocity.x, jumpPower);
    }
    public void Flip()
    {
        if (direction.x > 0)
            kingController.transform.localScale = new Vector3(1, 1, 1);
        if (direction.x < 0)
            kingController.transform.localScale = new Vector3(-1, 1, 1);
    }
    public void FLipToPlayer()
    {
        origin = kingController.transform.position;
        target = kingController.Target.transform.position;
        direction = (target - origin).normalized;
        if (direction.x > 0)
            kingController.transform.localScale = new Vector3(1, 1, 1);
        if (direction.x < 0)
            kingController.transform.localScale = new Vector3(-1, 1, 1);
    }
}
