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
    
}
