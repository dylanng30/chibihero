using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(BoxCollider2D))]
public class CollisionEnemy : CollisionBase
{
    [SerializeField] protected LowEnemyController lowEnemyController;

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
        if (lowEnemyController != null) return;
        lowEnemyController = GetComponentInParent<LowEnemyController>();
    }
}
