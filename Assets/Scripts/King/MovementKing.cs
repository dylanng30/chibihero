using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementKing : MonoBehaviour
{
    [SerializeField] protected KingController kingController;

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
        Vector2 origin = kingController.transform.position;
        Vector2 target = kingController.Target.transform.position;
        int speed = kingController.KingStats.MoveSpeed;
        float atkRange = kingController.KingStats.ATKRange;

        //Debug.Log($"King is moving towards target: {target}, current position: {origin}, speed: {speed}, attack range: {atkRange}");
        /*if (Vector2.Distance(origin, target) < atkRange)
        {
*//*            kingController.PhysicsKing.Rigidbody2D.velocity = Vector2.zero;*//*
            Debug.Log("King is within attack range, stopping movement.");
            return;
        }*/
        Vector2 direction = (target - origin).normalized;
        kingController.PhysicsKing.Rigidbody2D.velocity = new Vector2(direction.x * speed, kingController.PhysicsKing.Rigidbody2D.velocity.y);
    }
    public void MoveToNearestDoor()
    {
        DoorManager doorManager = GameObject.FindObjectOfType<DoorManager>();
        Vector2 door = doorManager.FindNearestDoor(kingController.transform).transform.position;
        Vector2 origin = kingController.transform.position;
        Vector2 direction = (door - origin).normalized;
        int speed = kingController.KingStats.MoveSpeed;
        kingController.PhysicsKing.Rigidbody2D.velocity = new Vector2(direction.x * speed, kingController.PhysicsKing.Rigidbody2D.velocity.y);

    }
    public void Flee()
    {
        Vector2 origin = kingController.transform.position;
        Vector2 target = kingController.Target.transform.position;
        int speed = kingController.KingStats.MoveSpeed;
        Vector2 direction = (origin - target).normalized;
        kingController.PhysicsKing.Rigidbody2D.velocity = new Vector2(direction.x * speed, kingController.PhysicsKing.Rigidbody2D.velocity.y);
    }

    public void Jump()
    {
        int jumpPower = kingController.KingStats.JumpPower;
        kingController.PhysicsKing.Rigidbody2D.velocity = new Vector2(kingController.PhysicsKing.Rigidbody2D.velocity.x, jumpPower);
    }
    public void Flip()
    {
        float dirX = kingController.PhysicsKing.Rigidbody2D.velocity.x;
        if (dirX > 0)
            kingController.transform.localScale = new Vector3(1, 1, 1);
        if (dirX < 0)
            kingController.transform.localScale = new Vector3(-1, 1, 1);
    }
    public void FLipToPlayer()
    {
        Vector2 origin = kingController.transform.position;
        Vector2 target = kingController.Target.transform.position;
        Vector2 direction = (target - origin).normalized;
        if (direction.x > 0)
            kingController.transform.localScale = new Vector3(1, 1, 1);
        if (direction.x < 0)
            kingController.transform.localScale = new Vector3(-1, 1, 1);
    }
}
