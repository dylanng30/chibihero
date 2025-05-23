using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PirateStats : ObjectStats
{
    [SerializeField] protected PirateController pirateController;    

    void Start()
    {
        LoadComponent();
    }

    protected void LoadComponent()
    {
        LoadPirateController();
        LoadEnemyStats();
    }

    protected virtual void LoadEnemyStats()
    {
        StartCoroutine(LoadEnemyStatsCouroutine());
    }
    protected IEnumerator LoadEnemyStatsCouroutine()
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
        //Debug.Log($"Đã load stats cho {enemyType} - HP: {maxHP}, ATK: {attackPower}, Armor: {armor}, Speed: {moveSpeed}");
    }

    protected virtual void LoadPirateController()
    {
        if (this.pirateController != null)
            return;
        this.pirateController = this.GetComponentInParent<PirateController>();
    }

}
