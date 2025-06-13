using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityNormalATK : MonoBehaviour
{
    [SerializeField] protected PlayerController playerController;
    [SerializeField] protected Transform ATKPoint;
    
    [Header("Animation Settings")]
    [SerializeField] private bool enableAttackAnimation = true;
    [SerializeField] private string attackAnimationName = "Attack";

    private Action GetATKTrigger;
    private bool atkTrigger;

    protected void GetATKing()
    {
        atkTrigger = InputManager.Instance.AttackPressed;
        if (atkTrigger)
        {
            Debug.Log("🎯 Normal Attack input detected! Calling NormalATK()");
        }
    }

    void Start()
    {
        LoadComponent();
        GetATKTrigger += GetATKing;
    }
    void Update()
    {
        GetATKTrigger?.Invoke();
        
        // Auto call NormalATK() when atkTrigger is true
        if (atkTrigger)
        {
            NormalATK();
            atkTrigger = false; // Reset để tránh spam
        }
    }

    protected void LoadComponent()
    {
        LoadPlayerController();
    }
    protected void LoadPlayerController()
    {
        if (this.playerController != null)
            return;
        this.playerController = transform.GetComponentInParent<PlayerController>();
    }

    public void NormalATK()
    {
        Debug.Log("🎯 NormalATK() method called!");
        GetATKTrigger?.Invoke();

        // Play attack animation (optional - only if available)
        if (enableAttackAnimation && playerController.AnimationPlayer != null)
        {
            // Try to play attack animation - AnimationManager will handle errors
            playerController.AnimationPlayer.SetAnimation(attackAnimationName);
            Debug.Log($"🎯 Playing attack animation: {attackAnimationName}");
        }

        // Play attack sound FIRST (before any mode checks)
        if (AudioManager.Instance != null)
        {
            AudioManager.Instance.PlayAttackSound();
            Debug.Log("🎯 Normal Attack triggered - playing attack sound");
        }
        else
        {
            Debug.LogWarning("❌ AudioManager.Instance is null during normal attack!");
        }

        // TopDown mode doesn't use melee attack, only sound and animation
        if (playerController.PhysicsPlayer.Mode == PlayerMode.TopDown)
        {
            Debug.Log("🎯 TopDown mode - playing sound and animation, no melee damage");
            return;
        }

        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(ATKPoint.position, 0.8f, LayerMask.GetMask("Enemy"));
        foreach (Collider2D enemy in hitEnemies)
        {
            var e = enemy.GetComponentInParent<IDamagable>();
            Debug.Log(e);
            e.TakeDamage(playerController.PlayerStats.AttackPower, playerController.gameObject);
            playerController.EXPManager.AddEXP(1000);
        }
       
    }

    public bool ATKTrigger
    {
        get
        {
            return atkTrigger;
        }
    }

    // Debug and setup methods
    [ContextMenu("Auto Detect Attack Animation")]
    private void AutoDetectAttackAnimation()
    {
        if (playerController?.AnimationPlayer?.Animator == null)
        {
            Debug.LogWarning("❌ PlayerController or AnimationManager not found!");
            return;
        }

        var animator = playerController.AnimationPlayer.Animator;
        if (animator.runtimeAnimatorController == null)
        {
            Debug.LogWarning("❌ No AnimatorController assigned!");
            return;
        }

        // Common attack animation names to try
        string[] attackNames = { "Attack", "attack", "Attack1", "BasicAttack", "NormalAttack", "Melee", "Hit" };
        
        Debug.Log("🔍 Auto-detecting attack animation...");
        foreach (string name in attackNames)
        {
            if (animator.HasState(0, Animator.StringToHash(name)))
            {
                attackAnimationName = name;
                Debug.Log($"✅ Found attack animation: {name}");
                return;
            }
        }

        Debug.LogWarning("❌ No common attack animation found. Available clips:");
        var clips = animator.runtimeAnimatorController.animationClips;
        foreach (var clip in clips)
        {
            Debug.Log($"   - {clip.name}");
        }
    }

    [ContextMenu("Test Attack")]
    private void TestAttack()
    {
        Debug.Log("🧪 Testing normal attack...");
        NormalATK();
    }

    [ContextMenu("Check Attack Setup")]
    private void CheckAttackSetup()
    {
        Debug.Log("=== Attack Setup Check ===");
        Debug.Log($"Enable Attack Animation: {enableAttackAnimation}");
        Debug.Log($"Attack Animation Name: {attackAnimationName}");
        Debug.Log($"PlayerController: {(playerController != null ? "✅" : "❌")}");
        Debug.Log($"AnimationPlayer: {(playerController?.AnimationPlayer != null ? "✅" : "❌")}");
        Debug.Log($"AudioManager: {(AudioManager.Instance != null ? "✅" : "❌")}");

        if (playerController?.AnimationPlayer?.Animator != null)
        {
            bool hasAttackState = playerController.AnimationPlayer.Animator.HasState(0, Animator.StringToHash(attackAnimationName));
            Debug.Log($"Attack State '{attackAnimationName}': {(hasAttackState ? "✅" : "❌")}");
        }
    }
}
