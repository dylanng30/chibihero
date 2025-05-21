using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class DamageBase : MonoBehaviour
{
    [SerializeField] protected int currentHP = 0;
    [SerializeField] protected int maxHP = 0;
    [SerializeField] protected bool isDead = false;

    protected virtual void Start()
    {
        Reset();
        LoadComponent();
    }
    public virtual void LoadComponent()
    {
        LoadController();
    }
    protected virtual void LoadController()
    {

    }
    public virtual void Reset()
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
}
