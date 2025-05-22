using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    [SerializeField] private EnemyType enemyType;
    [SerializeField] protected ScriptableEnemy enemy;
    [SerializeField] protected LowEnemyController lowEnemyController;

    protected int maxHP;
    protected int attackPower;
    protected int armor;
    protected int moveSpeed;
    protected int jumpPower;
    protected float atkRange;

    void Start()
    {
        LoadComponent();
    }

    protected void LoadComponent()
    {
        LoadEnemyController();
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
        maxHP = enemy._stats.Health;
        attackPower = enemy._stats.Attack;
        armor = enemy._stats.Armor;
        moveSpeed = enemy._stats.Speed;
        jumpPower = enemy._stats.JumpPower;
        atkRange = enemy._stats.ATKRange;
        Debug.Log($"Đã load stats cho {enemyType} - HP: {maxHP}, ATK: {attackPower}, Armor: {armor}, Speed: {moveSpeed}");
    }

    protected virtual void LoadEnemyController()
    {
        if (this.lowEnemyController != null)
            return;
        this.lowEnemyController = this.GetComponentInParent<LowEnemyController>();
    }
    public int MaxHP
    {
        get
        {
            return maxHP;
        }
    }
    public int AttackPower
    {
        get
        {
            return attackPower;
        }
    }
    public float ATKRange
    {
        get
        {
            return atkRange;
        }
    }
    public int Armor
    {
        get
        {
            return armor;
        }
    }
    public int MoveSpeed
    {
        get
        {
            return moveSpeed;
        }
    }
    public int JumpPower
    {
        get
        {
            return jumpPower;
        }
    }
}
