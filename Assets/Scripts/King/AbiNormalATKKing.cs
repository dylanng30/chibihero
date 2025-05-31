using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbiNormalATKKing : MonoBehaviour
{
    [SerializeField] protected KingController kingController;
    [SerializeField] protected Transform ATKPoint;

    private void Start()
    {
        LoadComponent();
    }
    protected void LoadComponent()
    {
        LoadController();
    }
    protected virtual void LoadController()
    {
        if (this.kingController != null)
            return;
        this.kingController = GetComponentInParent<KingController>();
    }

    public bool CanAttack()
    {
        float atkRange = kingController.KingStats.ATKRange;
        Transform player = GameObject.FindGameObjectWithTag("Player").transform;
        Vector2 target = player.position;
        Vector2 origin = kingController.transform.position;
        return Vector2.Distance(origin, target) < atkRange;
    }

    public void NormalATK()
    {
        StartCoroutine(CloseATK());
    }

    private IEnumerator CloseATK()
    {
        yield return new WaitUntil(() => kingController != null && kingController.KingStats != null);
        int dmg = kingController.KingStats.AttackPower;
        float atkRange = kingController.KingStats.ATKRange;
        AttackPlayer(dmg, atkRange);


    }
    private void AttackPlayer(int dmg, float atkRange)
    {        
        LayerMask targetLayer = LayerMask.GetMask("Player");

        Collider2D[] hitPlayers = Physics2D.OverlapCircleAll(ATKPoint.position, atkRange, targetLayer);
        foreach (Collider2D player in hitPlayers)
        {
            var p = player.GetComponentInParent<IDamagable>();
            p.TakeDamage(dmg, kingController.gameObject);
        }
    }
    /*private void AttackCannonBullet(int dmg, float atkRange)
    {
        LayerMask cannonBulletLayer = LayerMask.GetMask("CannonBullet");

        Collider2D[] hitCannonBullets = Physics2D.OverlapCircleAll(ATKPoint.position, atkRange, cannonBulletLayer);
        foreach (Collider2D cannonBullet in hitCannonBullets)
        {
            *//*var p = cannonBullet.GetComponentInParent<IDamagable>();
            p.TakeDamage(dmg, kingController.gameObject);*//*
        }
    }*/
}