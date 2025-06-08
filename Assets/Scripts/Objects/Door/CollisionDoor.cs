using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDoor : CollisionBase
{
    [SerializeField] protected DoorController doorController;

    private bool IsLocked = false;
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
        if (doorController != null) return;
        doorController = GetComponentInParent<DoorController>();
    }
    public override void LoadHitBox()
    {
        base.LoadHitBox();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("King") && DoorManager.Instance.IsLocked)
        {
            KingController kingController = collision.GetComponentInParent<KingController>();
            SetKingState(kingController);
        }
    }

    private void SetKingState(KingController kingController)
    {
        float xDistance = Mathf.Abs(kingController.transform.position.x - transform.position.x);
        float allowedDistance = 0.5f;
        if (xDistance <= allowedDistance && kingController.CollisionKing.IsGrounded())
        {
            doorController.PlayerInFrontOfDoor(true);
            kingController.StateManager.ChangeState(kingController.DoorInState);
            return;
        }
        doorController.PlayerInFrontOfDoor(false);
    }
}
