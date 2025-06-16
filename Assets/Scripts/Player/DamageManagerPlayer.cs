using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DamageManagerPlayer : DamageBase
{
    [SerializeField] protected PlayerController playerController;
    [SerializeField] private GameObject floatingText;
    [SerializeField] protected GameObject skull;
    
    // Invincibility system
    private bool isInvincible = false;
    private float invincibilityDuration = 5f; // 5 seconds invincibility after respawn
    private Coroutine invincibilityCoroutine;

    protected override void Awake()
    {
        base.Awake();
    }
    protected override void Start()
    {
        base.Start();
    }
    public override void LoadComponent()
    {
        base.LoadComponent();
        LoadStats();
    }
    protected override void LoadController()
    {
        base.LoadController();
        if (this.playerController != null)
            return;
        this.playerController = GetComponentInParent<PlayerController>();
    }
    protected override void LoadStats()
    {
        base.LoadStats();
        StartCoroutine(LoadStatsCoroutine());
    }
    protected IEnumerator LoadStatsCoroutine()
    {
        yield return new WaitUntil(() => playerController != null && playerController.PlayerStats != null);
        this.maxHP = playerController.PlayerStats.MaxHP;
        this.currentHP = maxHP;
        this.armor = playerController.PlayerStats.Armor;
    }

    public override void TakeDamage(int damage, GameObject enemy)
    {
        // Check if player is invincible
        if (isInvincible)
        {
            Debug.Log("Player is invincible, damage ignored!");
            return;
        }

        base.TakeDamage(damage, enemy);
        playerController.StateManager.ChangeState(playerController.HitState);

        // Play hurt sound
        AudioManager.PlayPlayerHurt(transform.position);
        
        CreateFloatingText(damage);
        playerController.PhysicsPlayer.KnockBack(enemy);
        playerController.PlayerStats.SetCurrentHP(currentHP);
        CheckPlayerDied();
        Debug.Log($"Player took {damage} damage from {enemy}. Current HP: {currentHP}");
    }
    private void CreateFloatingText(int damage)
    {
        if (floatingText == null)
        {
            Debug.Log("No Floating Text");
            return;
        }
            
        GameObject HPText = Instantiate(floatingText, playerController.transform.position, Quaternion.identity);
        HPText.GetComponent<TextMeshPro>().text = damage.ToString();
    }

    private void CheckPlayerDied()
    {
        if(currentHP > 0)
            isDead = false;
        else
        {
            isDead = true;
            // Play death sound
            AudioManager.PlayPlayerDeath(transform.position);
            Instantiate(skull, playerController.transform.position, Quaternion.identity);
            playerController.AnimationPlayer.SpriteRenderer.enabled = false;
            
            // Only reset HP, don't start invincibility here
            // GameManager will handle invincibility when respawning
            this.currentHP = maxHP;
            playerController.PlayerStats.SetCurrentHP(maxHP);
            
            GameManager.Instance.CompleteMap(false);
        }
    }

    public void StartInvincibility()
    {
        if (invincibilityCoroutine != null)
        {
            StopCoroutine(invincibilityCoroutine);
        }
        invincibilityCoroutine = StartCoroutine(InvincibilityCoroutine());
    }

    private IEnumerator InvincibilityCoroutine()
    {
        isInvincible = true;
        //Debug.Log("Player is now invincible for " + invincibilityDuration + " seconds");
        
        // Visual feedback - make player blink
        StartCoroutine(BlinkEffect());
        
        yield return new WaitForSeconds(invincibilityDuration);
        
        isInvincible = false;
        //Debug.Log("Player invincibility ended");
    }

    private IEnumerator BlinkEffect()
    {
        SpriteRenderer spriteRenderer = playerController.AnimationPlayer.SpriteRenderer;
        float blinkInterval = 0.1f;
        float elapsedTime = 0f;

        while (isInvincible && elapsedTime < invincibilityDuration)
        {
            spriteRenderer.color = new Color(1f, 1f, 1f, 0.5f); // Semi-transparent
            yield return new WaitForSeconds(blinkInterval);
            spriteRenderer.color = Color.white; // Full opacity
            yield return new WaitForSeconds(blinkInterval);
            elapsedTime += blinkInterval * 2;
        }

        // Ensure player is visible at the end
        spriteRenderer.color = Color.white;
    }
    public override void Heal(int amount)
    {
        base.Heal(amount);
    }

    // Public properties and methods for invincibility system
    public bool IsInvincible
    {
        get { return isInvincible; }
    }

    public void SetInvincible(bool invincible)
    {
        isInvincible = invincible;
        if (!invincible && invincibilityCoroutine != null)
        {
            StopCoroutine(invincibilityCoroutine);
            invincibilityCoroutine = null;
            // Reset sprite to normal
            playerController.AnimationPlayer.SpriteRenderer.color = Color.white;
        }
    }
    
}
