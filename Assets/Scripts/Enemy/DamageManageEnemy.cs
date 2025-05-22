using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageManagerEnemy : DamageBase
{
    [SerializeField] protected LowEnemyController lowEnemyController;

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
        if (this.lowEnemyController != null)
            return;
        this.lowEnemyController = this.GetComponentInParent<LowEnemyController>();
    }
    protected override void LoadStats()
    {
        base.LoadStats();
        this.maxHP = this.lowEnemyController.EnemyStats.MaxHP;        
        this.currentHP = maxHP;
        //Debug.Log($"Đã load stats cho Enemy - HP: {maxHP}");
    }

    public override void TakeDamage(int damage, GameObject enemy)
    {
        base.TakeDamage(damage, enemy);
        lowEnemyController.PhysicsEnemy.KnockBack(enemy);
        CheckEnemyDied();
        //Debug.Log($"Player took {damage} damage. Current HP: {currentHP}");
    }
    private void CheckEnemyDied()
    {
        if (currentHP > 0)
            isDead = false;
        else
        {
            isDead = true;
            Destroy(lowEnemyController.gameObject);
            //Debug.Log("Player is dead");
            //playerController.GetStateManager().ChangeState(playerController.GetDeathState());
        }
    }
    public override void Heal(int amount)
    {
        base.Heal(amount);
    }
}
