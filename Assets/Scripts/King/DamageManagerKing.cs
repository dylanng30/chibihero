using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DamageManagerKing : DamageBase
{
    [SerializeField] protected KingController kingController;
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
        if (this.kingController != null)
            return;
        this.kingController = this.GetComponentInParent<KingController>();
    }
    protected override void LoadStats()
    {
        base.LoadStats();
        StartCoroutine(LoadStatsCoroutine());
    }
    protected IEnumerator LoadStatsCoroutine()
    {
        yield return new WaitUntil(() => kingController != null && kingController.KingStats != null);
        yield return new WaitUntil(() => kingController.KingStats.MaxHP != 0);

        this.maxHP = kingController.KingStats.MaxHP;
        this.currentHP = maxHP;
        this.armor = kingController.KingStats.Armor;
    }

    public override void TakeDamage(int damage, GameObject enemy)
    {
        base.TakeDamage(damage, enemy);
        kingController.HealthBar.UpdateHeathBar(currentHP, maxHP);
        CreateFloatingText(damage);
        kingController.PhysicsKing.KnockBack(enemy);
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

        GameObject HPText = Instantiate(floatingText, kingController.transform.position, Quaternion.identity);
        HPText.GetComponent<TextMeshPro>().text = damage.ToString();
    }
    private void CheckEnemyDied()
    {
        if (currentHP > 0)
            isDead = false;
        else
        {
            isDead = true;
            ObserverManager.Instance.KingIsDead();
            Instantiate(skull, kingController.transform.position, Quaternion.identity);
            Destroy(kingController.gameObject);
            //Debug.Log("Player is dead");
            //playerController.GetStateManager().ChangeState(playerController.GetDeathState());
        }
    }
    public override void Heal(int amount)
    {
        base.Heal(amount);
    }
}
