using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class DamageBase : MonoBehaviour
{
    [SerializeField] protected int currentHP;
    [SerializeField] protected int maxHP;
    [SerializeField] protected int currentMP;
    [SerializeField] protected int maxMP;
    [SerializeField] protected int armor;
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
        currentMP += amount;
    }
    public virtual void TakeDamage(int damage, GameObject enemy)
    {
        currentHP -= (damage - armor);
    }
    public int CurrentHP
    {
        get { return currentHP; }
    }
    public int MaxHP
    {
        get { return maxHP; }
    }
    public int CurrentMP
    {
        get { return currentHP; }
    }
    public int MaxMP
    {
        get { return maxHP; }
    }
    public int Armor
    {
        get { return armor; }
    }
    public bool IsDead
    {
        get { return isDead; }
    }
}
