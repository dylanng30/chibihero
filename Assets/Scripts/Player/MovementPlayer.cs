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
        GetDashing();

        if (dashPressed)
        {
            Dash();
            dashPressed = false;
        }

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
    }
    protected void GetDashing()
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

    private void Update()
    {
        GetDirectionMove?.Invoke();
        GetJumpingMove?.Invoke();
        GetDashingMove?.Invoke();
    }

    public virtual void Moving()
    {
        GetDirectionMove?.Invoke();

        int speed = playerController.PlayerStats.MoveSpeed;
        
        // Play walking sound when moving
        if (direction.magnitude > 0 && playerController.CollisionPlayer.IsGrounded())
        {
            // Giảm tần suất phát âm thanh từ 0.5s xuống 0.7s để không spam
            if (Time.time % 0.7f < 0.1f)
            {
                AudioManager.PlayPlayerWalk(transform.position);
            }
        }
        
        if (playerController.PhysicsPlayer.Mode == PlayerMode.TopDown)
            playerController.PhysicsPlayer.Rigidbody2D.velocity = new Vector2(direction.normalized.x * speed, direction.normalized.y * speed);
        else
            playerController.PhysicsPlayer.Rigidbody2D.velocity = new Vector2(direction.normalized.x * speed, playerController.PhysicsPlayer.Rigidbody2D.velocity.y);
    }

    public virtual void Jumping()
    {
        GetJumpingMove?.Invoke();
        // Play jump sound
        AudioManager.PlayPlayerJump(transform.position);
        playerController.PhysicsPlayer.Rigidbody2D.velocity = new Vector2(playerController.PhysicsPlayer.Rigidbody2D.velocity.y, playerController.PlayerStats.JumpPower);
    }

    protected virtual void Flip()
    {
        if (direction.x > 0)
            playerController.transform.localScale = new Vector3(1, 1, 1);
        if (direction.x < 0)
            playerController.transform.localScale = new Vector3(-1, 1, 1);
    }

    public void FlipToEnemy()
    {
        if(playerController.AbilitySkill.NearestEnemy == null)
            return;

        Vector2 dir = playerController.AbilitySkill.NearestEnemy.position - playerController.transform.position;
        playerController.transform.localScale = dir.x > 0 ? new Vector3(1, 1, 1) : new Vector3(-1, 1, 1);

    }

    public void Dash()
    {
        GetDashingMove?.Invoke();

        float dashForce = playerController.PlayerStats.DashForce;
        float dashDuration = playerController.PlayerStats.DashTime;
        float dashCooldown = playerController.PlayerStats.DashCooldown;
        dashDirection = playerController.transform.localScale;
        dashDirection.y = 0;

        if (!canDash || isDashing)
            return;
            
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
