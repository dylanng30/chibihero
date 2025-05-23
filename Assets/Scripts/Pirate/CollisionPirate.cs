using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class CollisionPirate : CollisionBase
{
    [SerializeField] protected PirateController pirateController;

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
        if (pirateController != null) return;
        pirateController = GetComponentInParent<PirateController>();
    }
}
