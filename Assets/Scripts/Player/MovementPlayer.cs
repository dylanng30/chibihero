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
    [SerializeField] protected bool dashPressed;

    [SerializeField] private bool isDashing = false;
    [SerializeField] private bool canDash = true;
    [SerializeField] private Vector2 dashDirection;

    [SerializeField] private GameObject dashEffect;


    private Action GetDirectionMove;
    private Action GetJumpingMove;
    private Action GetDashingMove;
    void Start()
    {
        LoadComponent();
        GetDirectionMove += GetDirection;
        GetJumpingMove += GetJumping;
        GetDashingMove += GetDashing;
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
    public void GetDashing()
    {
        dashPressed = InputManager.Instance.DashPressed;
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
    public bool DashPressed
    {
        get
        {
            return dashPressed;
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
        GetDashingMove?.Invoke();

        float dashForce = playerController.PlayerStats.DashForce;
        float dashDuration = playerController.PlayerStats.DashTime;
        float dashCooldown = playerController.PlayerStats.DashCooldown;
        dashDirection = playerController.transform.localScale;
        dashDirection.y = 0;

        // if (!canDash || isDashing)
        //     return;
            
        StartCoroutine(DashNew(dashDirection, dashForce, dashDuration, dashCooldown));
        
    }

    public IEnumerator DashNew(Vector2 dashDirection, float dashForce, float dashDuration, float dashCooldown)
    {
        isDashing = true;
        canDash = false;
        int ghostCount = 5;
        float interval = dashDuration / ghostCount;

        playerController.PhysicsPlayer.Rigidbody2D.velocity = dashDirection.normalized * dashForce;

        for (int i = 0; i < ghostCount; i++)
        {
            // if (playerController.PhysicsPlayer.Rigidbody2D.velocity == Vector2.zero)
            //     yield break;

            SpawnGhost(dashEffect, transform.position);
            yield return new WaitForSeconds(interval);
        }

        isDashing = false;
        yield return new WaitForSeconds(dashDuration);
        canDash = true;
    }

    private void SpawnGhost(GameObject ghostPrefab, Vector3 pos)
    {
        GameObject ghost = Instantiate(ghostPrefab, pos, Quaternion.identity);
        ghost.transform.localScale = playerController.transform.localScale;    
        Destroy(ghost, 1f);
    }

}
