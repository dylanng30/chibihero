using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityNormalATK : MonoBehaviour
{
    [SerializeField] protected PlayerController playerController;
    [SerializeField] protected Transform ATKPoint;

    private Action GetATKTrigger;
    private bool atkTrigger;


    void Start()
    {
        LoadComponent();
        GetATKTrigger += GetATKing;
    }
    void Update()
    {
        GetATKTrigger?.Invoke();
    }
    protected void GetATKing()
    {
        atkTrigger = InputManager.Instance.AttackPressed;
        Debug.Log("ATK Trigger: " + atkTrigger);
    }
    public bool ATKTrigger
    {
        get
        {
            return atkTrigger;
        }
    }

    protected void LoadComponent()
    {
        LoadPlayerController();
    }
    protected void LoadPlayerController()
    {
        if (this.playerController != null)
            return;
        this.playerController = transform.GetComponentInParent<PlayerController>();
    }

    public void NormalATK()
    {
        GetATKTrigger?.Invoke();
        if (ATKPoint != null)
        {
            Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(ATKPoint.position, 0.8f, LayerMask.GetMask("Enemy"));
            foreach (Collider2D enemy in hitEnemies)
            {
                Enemy e = enemy.GetComponent<Enemy>();
                e.TakeDamage(playerController.PlayerStats.AttackPower, this.transform);
            }
        }
        else
        {
            Debug.LogError("ATKPoint kco");
        }
    }

}
