using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoomBarrel : MonoBehaviour
{
    [SerializeField] protected BarrelController barrelController;

    private ObjectPool pool;

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
        if (this.barrelController != null)
            return;
        this.barrelController = GetComponentInParent<BarrelController>();
    }

    public void Boom()
    {

        LayerMask targetLayer = LayerMask.GetMask("Player");
        float Range = barrelController.BarrelStats.Range;
        int ATKPower = barrelController.BarrelStats.ATKPower;

        Collider2D[] hitPlayers = Physics2D.OverlapCircleAll(this.transform.position, Range, targetLayer);
        foreach (Collider2D player in hitPlayers)
        {
            var p = player.GetComponentInParent<IDamagable>();
            p.TakeDamage(ATKPower, this.gameObject);
        }

        Destroy(this.transform.parent.gameObject);
    }
}
