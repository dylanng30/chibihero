using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DamageManagerPumpkin : DamageBase
{
    [SerializeField] protected PumpkinController controller;
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
        if (this.controller != null)
            return;
        this.controller = this.GetComponentInParent<PumpkinController>();
    }
    protected override void LoadStats()
    {
        StartCoroutine(LoadStatsCoroutine());
    }
    protected IEnumerator LoadStatsCoroutine()
    {
        yield return new WaitUntil(() => controller != null && controller.Stats != null);
        yield return new WaitUntil(() => controller.Stats.MaxHP != 0);

        this.maxHP = controller.Stats.MaxHP;
        this.currentHP = maxHP;
    }

    public override void TakeDamage(int damage, GameObject enemy)
    {
        base.TakeDamage(damage, enemy);
        controller.HealthBar.UpdateHeathBar(currentHP, maxHP);
        CreateFloatingText(damage);
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

        GameObject HPText = Instantiate(floatingText, controller.transform.position, Quaternion.identity);
        HPText.GetComponent<TextMeshPro>().text = damage.ToString();
    }
    private void CheckEnemyDied()
    {
        if (currentHP > 0)
            isDead = false;
        else
        {
            isDead = true;
            Destroy(controller.gameObject);
        }
    }
    public override void Heal(int amount)
    {
        base.Heal(amount);
    }
}
