using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionBarrel : CollisionBase
{
    private BarrelController controller;
    protected override void Start()
    {
        base.Start();
    }
    public override void LoadComponent()
    {
        base.LoadComponent();
    }

    public override void LoadController()
    {
        if (controller != null) return;
        controller = GetComponentInParent<BarrelController>();
    }
    public override void LoadHitBox()
    {
        base.LoadHitBox();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            controller.StateManager.ChangeState(controller.PreExplosionState);
        }
    }
}
