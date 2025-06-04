using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MovementPlayer : MonoBehaviour
{
    [SerializeField] protected PlayerController playerController;

    [SerializeField] protected Vector2 direction;
    [SerializeField] protected bool jumpPressed;
    [SerializeField] protected bool atkTrigger;

    [SerializeField] private bool isDashing = false;
    [SerializeField] private bool canDash = true;
    [SerializeField] private Vector2 dashDirection;

    [SerializeField] private GameObject dashEffect;


    private Action GetDirectionMove;
    private Action GetJumpingMove;
    void Start()
    {
        LoadComponent();
        GetDirectionMove += GetDirection;
        GetJumpingMove += GetJumping;
    }

    void FixedUpdate()
    {
        Moving();

        GetJumpingMove?.Invoke();
        if (jumpPressed && playerController.CollisionPlayer.IsGrounded())
        {
            Jumping();
            jumpPressed = false;
        }
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
    protected void GetJumping()
    {
        jumpPressed = InputManager.Instance.JumpPressed;
        //Debug.Log("Jump Pressed: " + jumpPressed);
    }
    public Vector2 DirectionMove
    {
        get
        {
            return direction;
        }
    }
    public bool JumpPressed
    {
        get
        {
            return jumpPressed;
        }
    }

    public virtual void Moving()
    {
        GetDirectionMove?.Invoke();

        int speed = playerController.PlayerStats.MoveSpeed;
        if (playerController.PhysicsPlayer.Mode == PlayerMode.TopDown)
            playerController.PhysicsPlayer.Rigidbody2D.velocity = new Vector2(direction.normalized.x * speed, direction.normalized.y * speed);
        else
            playerController.PhysicsPlayer.Rigidbody2D.velocity = new Vector2(direction.normalized.x * speed, playerController.PhysicsPlayer.Rigidbody2D.velocity.y);
    }

    public virtual void Jumping()
    {
        GetJumpingMove?.Invoke();
        playerController.PhysicsPlayer.Rigidbody2D.velocity = new Vector2(playerController.PhysicsPlayer.Rigidbody2D.velocity.y, playerController.PlayerStats.JumpPower);
    }

    protected virtual void Flip()
    {
        if (direction.x > 0)
            playerController.transform.localScale = new Vector3(1, 1, 1);
        else if (direction.x < 0)
            playerController.transform.localScale = new Vector3(-1, 1, 1);
    }



    public void Dash()
    {
        float dashForce = playerController.PlayerStats.DashForce;
        float dashDuration = playerController.PlayerStats.DashTime;
        float dashCooldown = playerController.PlayerStats.DashCooldown;
        if (isDashing || !canDash)
        {
            Debug.Log("Cannot dash right now.");
            return;
        }
        StartCoroutine(DashCoroutine(dashForce, dashDuration, dashCooldown));
    }
    private IEnumerator DashCoroutine(float dashForce, float dashDuration, float dashCooldown)
    {
        Debug.Log("dash");
        isDashing = true;
        canDash = false;

        

        float startTime = Time.time;

        while (Time.time < startTime + dashDuration)
        {
            dashDirection = playerController.transform.localScale;
            dashDirection.y = 0;  
            playerController.PhysicsPlayer.Rigidbody2D.velocity += dashDirection * dashForce;          
            StartCoroutine(DashEffectCoroutine());
            Debug.Log(playerController.PhysicsPlayer.Rigidbody2D.velocity);
            yield return new WaitForSeconds((dashDuration / 5f)- 1f);
        }

        isDashing = false;

        yield return new WaitForSeconds(dashCooldown);
        canDash = true;
    }

    private IEnumerator DashEffectCoroutine()
    {        
        GameObject GhostEffect = Instantiate(dashEffect, playerController.transform.position, Quaternion.identity);
        GhostEffect.transform.localScale = playerController.transform.localScale;
        yield return new WaitForSeconds(0.5f);
        Destroy(GhostEffect);       
    }

}
