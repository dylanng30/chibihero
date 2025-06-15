using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ATKAbiPumpkin : MonoBehaviour
{
    [SerializeField] protected PumpkinController controller;
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
        if (this.controller != null)
            return;
        this.controller = GetComponentInParent<PumpkinController>();
    }

    public void NormalATK()
    {
        StartCoroutine(CloseATK());
    }

    private IEnumerator CloseATK()
    {
        yield return new WaitUntil(() => controller != null && controller.Stats != null);
        int dmg = controller.Stats.AttackPower;
        float atkRange = controller.Stats.ATKRange;
        AttackPlayer(dmg, atkRange);


    }
    private void AttackPlayer(int dmg, float atkRange)
    {
        LayerMask targetLayer = LayerMask.GetMask("Player");

        Collider2D[] hitPlayers = Physics2D.OverlapCircleAll(ATKPoint.position, atkRange, targetLayer);
        foreach (Collider2D player in hitPlayers)
        {
            var p = player.GetComponentInParent<IDamagable>();
            //Debug.Log(p);
            p.TakeDamage(dmg, controller.gameObject);
        }
    }
}
