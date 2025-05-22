using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class DamageBase : MonoBehaviour
{
    [SerializeField] protected int currentHP;
    [SerializeField] protected int maxHP;
    [SerializeField] protected bool isDead = false;

    protected virtual void Awake()
    {
        LoadComponent();
    }
    protected virtual void Start()
    {
        LoadStats();
    }
    public virtual void LoadComponent()
    {
        LoadController();        
    }
    protected virtual void LoadController()
    {

    }
    protected virtual void LoadStats()
    {

    }

    public virtual void Heal(int amount)
    {
        currentHP += amount;
        if (currentHP > maxHP)
            currentHP = maxHP;
    }
    public virtual void TakeDamage(int damage, GameObject enemy)
    {
        currentHP -= damage;
        if (currentHP <= 0)
            currentHP = 0;
        ChecktityEnDied();
    }

    private void ChecktityEnDied()
    {
        if (currentHP > 0)
            isDead = false;
        else
        {
            isDead = true;
        }
    }
    public int CurrentHP
    {
        get { return currentHP; }
    }
    public int MaxHP
    {
        get { return maxHP; }
    }
    public bool IsDead
    {
        get { return isDead; }
    }
}
