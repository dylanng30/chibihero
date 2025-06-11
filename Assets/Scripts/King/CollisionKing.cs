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

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Door")
            && !DoorManager.Instance.IsLocked
            && DoorManager.Instance.KingReady)
        {
            if(kingController.KingAI.CanOpenTheDoor(collision.transform))
                kingController.StateManager.ChangeState(kingController.DoorInState);
        }

    }
}
