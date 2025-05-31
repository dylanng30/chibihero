using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class CollisionKing : CollisionBase
{
    [SerializeField] protected KingController kingController;

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
        if (kingController != null) return;
        kingController = GetComponentInParent<KingController>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        /*if (collision.gameObject.CompareTag("CanonBullet"))
        {
            kingController.StateManager.ChangeState(kingController.NormalATKState);
        }*/
    }
}
