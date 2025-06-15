using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PumpkinStats : ObjectStats
{
    [SerializeField] protected PumpkinController controller;

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
        if (this.controller != null)
            return;
        this.controller = this.GetComponentInParent<PumpkinController>();
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
