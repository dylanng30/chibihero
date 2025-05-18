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
            //playerCtrl.AnimationPlayer.SetAnimationRuning(false);
            playerController.PhysicsPlayer.Rigidbody2D.velocity = Vector2.zero;
            return;
        }
        //playerController.AnimationPlayer.SetAnimationRuning(true);
        direction = direction.normalized;
        int speed = playerController.PlayerStats.MoveSpeed;
        playerController.PhysicsPlayer.Rigidbody2D.velocity = new Vector2(direction.x * speed, direction.y * speed);
    }

}
