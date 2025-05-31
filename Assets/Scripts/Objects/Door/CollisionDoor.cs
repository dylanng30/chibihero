using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDoor : CollisionBase
{
    [SerializeField] protected DoorController doorController;
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("King"))
        {
            KingController kingController = collision.GetComponent<KingController>();
            float xDistance = Mathf.Abs(kingController.transform.position.x - transform.position.x);
            float allowedDistance = 0.5f; // khoảng sai số cho phép
            if (xDistance <= allowedDistance && kingController.CollisionKing.IsGrounded())
            {
                doorController.PlayerInFrontOfDoor(true);
                kingController.StateManager.ChangeState(kingController.DoorInState);
                return;
            }
        }
        doorController.PlayerInFrontOfDoor(false);
    }
}
