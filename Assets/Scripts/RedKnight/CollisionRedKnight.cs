using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class CollisionRedKnight : CollisionBase
{
    private RedKnightController redKnightController;
    protected override void Start()
    {
        base.Start();
        LoadController();
    }
    public override void LoadComponent()
    {
        base.LoadComponent();
        LoadHitBox();
    }
    public override void LoadHitBox()
    {
        base.LoadHitBox();
    }
    public override void LoadController()
    {
        base.LoadController();
        if (redKnightController != null) return;
        redKnightController = GetComponentInParent<RedKnightController>();
    }
}
