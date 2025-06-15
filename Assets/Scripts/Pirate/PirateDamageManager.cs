using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PirateDamageManager : DamageBase
{
    [SerializeField] protected PirateController pirateController;
    [SerializeField] protected GameObject skull;
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
        yield return new WaitUntil(() => pirateController.PirateStats.MaxHP != 0);

        Debug.Log(pirateController.PirateStats.MaxHP);
        this.maxHP = pirateController.PirateStats.MaxHP;
        this.currentHP = maxHP;
        this.armor = pirateController.PirateStats.Armor;
    }

    public override void TakeDamage(int damage, GameObject enemy)
    {
        base.TakeDamage(damage, enemy);
        pirateController.HealthBar.UpdateHeathBar(currentHP, maxHP);
        CreateFloatingText(damage);
        pirateController.PhysicsPirate.KnockBack(enemy);
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

        GameObject HPText = Instantiate(floatingText, pirateController.transform.position, Quaternion.identity);
        HPText.GetComponent<TextMeshPro>().text = damage.ToString();
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
