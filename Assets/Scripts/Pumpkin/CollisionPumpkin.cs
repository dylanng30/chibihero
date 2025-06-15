using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class CollisionPumpkin : CollisionBase
{
    [SerializeField] protected PumpkinController controller;

    protected override void Start()
    {
        base.Start();
    }
    public override void LoadComponent()
    {
        base.LoadComponent();
    }
    public override void LoadHitBox()
    {
        base.LoadHitBox();
    }
    public override void LoadController()
    {
        if (controller != null) return;
        controller = GetComponentInParent<PumpkinController>();
    }
}
