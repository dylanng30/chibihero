using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageManagerPlayer : DamageBase
{
    [SerializeField] protected PlayerController playerController;

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
        this.maxHP = playerController.PlayerStats.MaxHP;
        this.currentHP = maxHP;
    }

    public override void TakeDamage(int damage, GameObject enemy)
    {
        base.TakeDamage(damage, enemy);
        playerController.PhysicsPlayer.KnockBack(enemy);
        CheckPlayerDied();
        Debug.Log($"Player took {damage} damage. Current HP: {currentHP}");
    }
    private void CheckPlayerDied()
    {
        if(currentHP > 0)
            isDead = false;
        else
        {
            isDead = true;
            //Debug.Log("Player is dead");
            //playerController.GetStateManager().ChangeState(playerController.GetDeathState());
        }
    }
    public override void Heal(int amount)
    {
        base.Heal(amount);
    }
    
}
