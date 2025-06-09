using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PirateDamageManager : DamageBase
{
    [SerializeField] protected PirateController pirateController;
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
        if (this.pirateController != null)
            return;
        this.pirateController = this.GetComponentInParent<PirateController>();
    }
    protected override void LoadStats()
    {
        base.LoadStats();
        StartCoroutine(LoadStatsCoroutine());
    }
    protected IEnumerator LoadStatsCoroutine()
    {
        yield return new WaitUntil(() => pirateController != null && pirateController.PirateStats != null);
        this.maxHP = pirateController.PirateStats.MaxHP;
        this.currentHP = maxHP;
    }

    public override void TakeDamage(int damage, GameObject enemy)
    {
        base.TakeDamage(damage, enemy);
        pirateController.PhysicsPirate.KnockBack(enemy);
        CheckEnemyDied();
        Debug.Log($"Enemy took {damage} damage. Current HP: {currentHP}");
    }
    private void CheckEnemyDied()
    {
        if (currentHP > 0)
            isDead = false;
        else
        {
            isDead = true;
            Instantiate(skull, pirateController.transform.position, Quaternion.identity);
            Destroy(pirateController.gameObject);
        }
    }
    public override void Heal(int amount)
    {
        base.Heal(amount);
    }
}
