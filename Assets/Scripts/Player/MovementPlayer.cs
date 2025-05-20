using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementPlayer : MonoBehaviour
{
    [SerializeField] protected PlayerController playerController;

    [SerializeField] protected Vector2 direction;

    private Action GetDirectionMove;
    void Start()
    {
        LoadComponent();
        GetDirectionMove += GetDirection;        
    }

    void FixedUpdate()
    {
        Moving();
        Flip();
    }
    protected void LoadComponent()
    {
        LoadPlayerController();
    }
    protected virtual void LoadPlayerController()
    {
        if (this.playerController != null)
            return;
        this.playerController = transform.GetComponentInParent<PlayerController>();
    }

    protected void GetDirection()
    {
        direction.x = InputManager.Instance.KeyHorizontal;
        direction.y = InputManager.Instance.KeyVertical;
    }

    protected virtual void Moving()
    {
        GetDirectionMove?.Invoke();
        if (direction == Vector2.zero)
        {
            playerController.AnimationPlayer.SetAnimation("Idle");
            playerController.PhysicsPlayer.Rigidbody2D.velocity = Vector2.zero;
            return;
        }
        playerController.AnimationPlayer.SetAnimation("Run");
        direction = direction.normalized;
        int speed = playerController.PlayerStats.MoveSpeed;
        playerController.PhysicsPlayer.Rigidbody2D.velocity = new Vector2(direction.x * speed, direction.y * speed);
    }

    protected virtual void Flip()
    {
        if (direction.x > 0)
            playerController.transform.localScale = new Vector3(1, 1, 1);
        else if (direction.x < 0)
            playerController.transform.localScale = new Vector3(-1, 1, 1);
    }

}
