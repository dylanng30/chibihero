using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class CollisionPlayer : CollisionBase
{
    [SerializeField] protected PlayerController playerController;
    private bool wasGrounded = false;
    
    protected override void Start()
    {
        base.Start();
        wasGrounded = IsGrounded();
    }
    
    private void Update()
    {
        bool currentlyGrounded = IsGrounded();
        
        // If player just landed (wasn't grounded but now is)
        if (!wasGrounded && currentlyGrounded)
        {
            AudioManager.StopPlayerJump();
        }
        
        wasGrounded = currentlyGrounded;
    }
    
    public override void LoadComponent()
    {
        base.LoadComponent();
    }

    public override void LoadController()
    {
        if (playerController != null) return;
        playerController = GetComponentInParent<PlayerController>();
    }
    
    public override void LoadHitBox()
    {
        base.LoadHitBox();
    }
}
