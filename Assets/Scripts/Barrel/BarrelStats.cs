using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrelStats : MonoBehaviour
{
    [SerializeField] private EnemyType enemyType;
    [SerializeField] protected ScriptableEnemy enemy;
    [SerializeField] protected BarrelController controller;

    protected int moveSpeed;
    protected float range;
    protected int atkPower;

    void Start()
    {
        LoadComponent();
    }

    protected void LoadComponent()
    {
        LoadController();
        LoadEnemyStats();
    }

    protected virtual void LoadEnemyStats()
    {
        StartCoroutine(LoadEnemyStatsCouroutine());
    }
    private IEnumerator LoadEnemyStatsCouroutine()
    {
        yield return new WaitUntil(() => Systems.Instance != null && Systems.Instance.ResourceSystem != null);

        enemy = Systems.Instance.ResourceSystem.GetEnemy(enemyType);
        moveSpeed = enemy._stats.Speed;
        atkPower = enemy._stats.Attack;
        range = enemy._stats.ATKRange;
    }

    protected virtual void LoadController()
    {
        if (this.controller != null)
            return;
        this.controller = this.GetComponentInParent<BarrelController>();
    }
    public float Range
    {
        get
        {
            return range;
        }
    }
    public int MoveSpeed
    {
        get
        {
            return moveSpeed;
        }
    }
    public int ATKPower
    {
        get
        {
            return atkPower;
        }
    }
    public EnemyType EnemyType
    {
        get
        {
            return enemyType;
        }
    }
}
