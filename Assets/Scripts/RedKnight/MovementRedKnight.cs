using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementRedKnight : MonoBehaviour
{
    [SerializeField] protected RedKnightController redKnightController;
    protected virtual void Start()
    {
        LoadComponent();
    }
    private void Update()
    {
        Flip();
    }
    protected virtual void LoadComponent()
    {
        LoadController();
    }
    protected virtual void LoadController()
    {
        if (this.redKnightController != null)
            return;
        this.redKnightController = GetComponentInParent<RedKnightController>();
    }

    public void Moving()
    {
        Vector2 origin = redKnightController.transform.position;
        Vector2 target = redKnightController.Target.transform.position;
        int speed = redKnightController.RedKnightStats.MoveSpeed;
        float atkRange = redKnightController.RedKnightStats.ATKRange;

        if (Vector2.Distance(origin, target) < atkRange)
        {
            redKnightController.PhysicRedKnight.Rigidbody2D.velocity = Vector2.zero;
            return;
        }
        Vector2 direction = (target - origin).normalized;
        redKnightController.PhysicRedKnight.Rigidbody2D.velocity = new Vector2(direction.x * speed, redKnightController.PhysicRedKnight.Rigidbody2D.velocity.y);
    }
    public void Flee()
    {
        Vector2 origin = redKnightController.transform.position;
        Vector2 target = redKnightController.Target.transform.position;
        int speed = redKnightController.RedKnightStats.MoveSpeed;
        Vector2 direction = (origin - target).normalized;
        redKnightController.PhysicRedKnight.Rigidbody2D.velocity = new Vector2(direction.x * speed, redKnightController.PhysicRedKnight.Rigidbody2D.velocity.y);
    }

    public void Jump()
    {
        int jumpPower = redKnightController.RedKnightStats.JumpPower;
        redKnightController.PhysicRedKnight.Rigidbody2D.velocity = new Vector2(redKnightController.PhysicRedKnight.Rigidbody2D.velocity.x, jumpPower);
    }
    public void Flip()
    {
        Vector2 origin = redKnightController.transform.position;
        Vector2 target = redKnightController.Target.transform.position;
        Vector2 direction = (target - origin).normalized;
        if (direction.x > 0)
            redKnightController.transform.localScale = new Vector3(1, 1, 1);
        if (direction.x < 0)
            redKnightController.transform.localScale = new Vector3(-1, 1, 1);
    }
}
