using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageManagerPlayer : MonoBehaviour
{
    [SerializeField] protected int currentHP = 0;
    [SerializeField] protected int maxHP = 0;
    [SerializeField] protected bool isDead = false;

    [SerializeField] protected PlayerController playerController;

    private bool loaded = false;

    private void Awake()
    {
        
    }
    private void Start()
    {
        LoadComponent();
        
    }
    protected void LoadComponent()
    {
        LoadPlayerController();
    }
    protected virtual void LoadPlayerController()
    {
        if (this.playerController != null)
            return;
        this.playerController = transform.GetComponentInParent<PlayerController>();
    }

    public void TakeDamage(int damage, GameObject enemy)
    {
        currentHP -= damage;
        /*if (currentHP <= 0)
            currentHP = 0;*/
        playerController.PhysicsPlayer.KnockBack(enemy);
        CheckPlayerDied();
        /*Debug.Log($"Player took {damage} damage. Current HP: {currentHP}");*/
    }
    private void CheckPlayerDied()
    {
        if(currentHP > 0)
            isDead = false;
        else
        {
            isDead = true;
            //Debug.Log("Player is dead");
            //playerController.GetStateManager().ChangeState(playerController.GetDeathState());
        }
    }
    public void Heal(int amount)
    {
        currentHP += amount;
        if (currentHP > maxHP)
            currentHP = maxHP;
    }
    public void Reset()
    {
        maxHP = playerController.PlayerStats.MaxHP;
        currentHP = maxHP;
        isDead = false;
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
