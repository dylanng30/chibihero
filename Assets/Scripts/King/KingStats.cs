using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KingStats : ObjectStats
{
    [SerializeField] protected KingController kingController;

    void Start()
    {
        LoadComponent();
    }

    protected void LoadComponent()
    {
        LoadController();
        LoadStats();
    }
    protected virtual void LoadController()
    {
        if (this.kingController != null)
            return;
        this.kingController = this.GetComponentInParent<KingController>();
    }

    protected virtual void LoadStats()
    {
        StartCoroutine(LoadStatsCouroutine());
    }
    protected IEnumerator LoadStatsCouroutine()
    {
        yield return new WaitUntil(() => Systems.Instance != null && Systems.Instance.ResourceSystem != null);

        enemy = Systems.Instance.ResourceSystem.GetEnemy(enemyType);
        maxHP = enemy._stats.Health;
        currentHP = maxHP;
        attackPower = enemy._stats.Attack;
        armor = enemy._stats.Armor;
        moveSpeed = enemy._stats.Speed;
        jumpPower = enemy._stats.JumpPower;
        atkRange = enemy._stats.ATKRange;
        Debug.Log($"Đã load stats cho {enemyType} - HP: {maxHP}, ATK: {attackPower}, Armor: {armor}, Speed: {moveSpeed}");
    }    
}
