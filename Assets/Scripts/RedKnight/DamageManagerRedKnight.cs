using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DamageManagerRedKnight : DamageBase
{
    [SerializeField] protected RedKnightController redKnightController;
    [SerializeField] private GameObject skull, floatingText;

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
        if (this.redKnightController != null)
            return;
        this.redKnightController = this.GetComponentInParent<RedKnightController>();
    }
    protected override void LoadStats()
    {
        base.LoadStats();
        StartCoroutine(LoadStatsCoroutine());
    }
    protected IEnumerator LoadStatsCoroutine()
    {
        yield return new WaitUntil(() => redKnightController != null && redKnightController.RedKnightStats != null);
        this.maxHP = redKnightController.RedKnightStats.MaxHP;
        this.currentHP = maxHP;
    }

    public override void TakeDamage(int damage, GameObject enemy)
    {
        base.TakeDamage(damage, enemy);
        redKnightController.HealthBar.UpdateHeathBar(currentHP, maxHP);
        CreateFloatingText(damage);
        redKnightController.PhysicRedKnight.KnockBack(enemy);
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

        GameObject HPText = Instantiate(floatingText, redKnightController.transform.position, Quaternion.identity);
        HPText.GetComponent<TextMeshPro>().text = damage.ToString();
    }
    private void CheckEnemyDied()
    {
        if (currentHP > 0)
            isDead = false;
        else
        {
            isDead = true;
            Instantiate(skull, redKnightController.transform.position, Quaternion.identity);
            Destroy(redKnightController.gameObject);
        }
    }
    public override void Heal(int amount)
    {
        base.Heal(amount);
    }
}
