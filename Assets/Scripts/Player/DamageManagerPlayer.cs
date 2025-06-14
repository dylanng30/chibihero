using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DamageManagerPlayer : DamageBase
{
    [SerializeField] protected PlayerController playerController;
    [SerializeField] private GameObject floatingText;
    [SerializeField] protected GameObject skull;

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
    }

    public override void TakeDamage(int damage, GameObject enemy)
    {
        base.TakeDamage(damage, enemy);
        
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
            // AudioManager.PlayPlayerDeath(transform.position);
            // Instantiate(skull, playerController.transform.position, Quaternion.identity);
            // playerController.AnimationPlayer.SpriteRenderer.enabled = false;
            // playerController.PlayerStats.Reset();
            // GameManager.Instance.CompleteMap(false);
        }
    }
    public override void Heal(int amount)
    {
        base.Heal(amount);
    }
    
}
