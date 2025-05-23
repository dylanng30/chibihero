using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ObjectStats : MonoBehaviour
{
    [SerializeField] protected EnemyType enemyType;
    [SerializeField] protected ScriptableEnemy enemy;

    protected int maxHP;
    protected int currentHP;
    protected int attackPower;
    protected int armor;
    protected int moveSpeed;
    protected int jumpPower;
    protected float atkRange;

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
    public int CurrentHP
    {
        get
        {
            return currentHP;
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
