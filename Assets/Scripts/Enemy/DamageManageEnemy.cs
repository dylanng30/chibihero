using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DamageManagerEnemy : DamageBase
{
    [SerializeField] protected LowEnemyController lowEnemyController;
    [SerializeField] private GameObject skull;
    [SerializeField] private GameObject floatingText;

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
        StartCoroutine(LoadStatsCoroutine());
    }
    protected IEnumerator LoadStatsCoroutine()
    {
        yield return new WaitUntil(() => lowEnemyController != null && lowEnemyController.EnemyStats != null);
        yield return new WaitUntil(() => lowEnemyController.EnemyStats.MaxHP != 0);

        this.maxHP = lowEnemyController.EnemyStats.MaxHP;
        this.currentHP = maxHP;        
    }

    public override void TakeDamage(int damage, GameObject enemy)
    {
        base.TakeDamage(damage, enemy);
        lowEnemyController.StateManager.ChangeState(lowEnemyController.HitState);
        lowEnemyController.HealthBar.UpdateHeathBar(currentHP, maxHP);
        CreateFloatingText(damage);
        lowEnemyController.PhysicsEnemy.KnockBack(enemy);
        CheckEnemyDied();
        Debug.Log($"Enemy took {damage} damage. Current HP: {currentHP}");
    }
    private void CreateFloatingText(int damage)
    {
        if (floatingText == null)
        {
            Debug.Log("No Floating Text");
            return;
        }

        GameObject HPText = Instantiate(floatingText, lowEnemyController.transform.position, Quaternion.identity);
        HPText.GetComponent<TextMeshPro>().text = damage.ToString();
    }
    private void CheckEnemyDied()
    {
        if (currentHP > 0)
            isDead = false;
        else
        {
            isDead = true;
            Instantiate(skull, lowEnemyController.transform.position, Quaternion.identity);
            Destroy(lowEnemyController.gameObject);
        }
    }
    public override void Heal(int amount)
    {
        base.Heal(amount);
    }
}
