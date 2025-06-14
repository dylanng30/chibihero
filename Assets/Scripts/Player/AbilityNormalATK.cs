using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityNormalATK : MonoBehaviour
{
    [SerializeField] protected PlayerController playerController;
    [SerializeField] protected Transform ATKPoint;
    [SerializeField] private int currentAttackIndex = 1; // Track current attack for sound variation

    private Action GetATKTrigger;
    private bool atkTrigger;

    protected void GetATKing()
    {
        atkTrigger = InputManager.Instance.AttackPressed;
    }

    void Start()
    {
        LoadComponent();
        GetATKTrigger += GetATKing;
    }
    void Update()
    {
        GetATKTrigger?.Invoke();
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

        if (playerController.PhysicsPlayer.Mode == PlayerMode.TopDown)
            return;

        // Play attack sound with variation
        AudioManager.PlayPlayerAttack(currentAttackIndex, transform.position);
        
        // Cycle through attack sounds (1, 2, 3, then back to 1)
        currentAttackIndex = (currentAttackIndex % 3) + 1;

        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(ATKPoint.position, 0.8f, LayerMask.GetMask("Enemy"));
        foreach (Collider2D enemy in hitEnemies)
        {
            var e = enemy.GetComponentInParent<IDamagable>();
            Debug.Log(e);
            e.TakeDamage(playerController.PlayerStats.AttackPower, playerController.gameObject);
            playerController.EXPManager.AddEXP(1000);
        }
       
    }

    public bool ATKTrigger
    {
        get
        {
            return atkTrigger;
        }
    }

}
