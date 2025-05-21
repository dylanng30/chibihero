using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CollisionBase : MonoBehaviour
{
    [SerializeField] protected BoxCollider2D hitbox;

    protected virtual void Start()
    {
        LoadComponent();
    }
    public virtual void LoadComponent()
    {
        LoadHitBox();
        LoadController();
    }

    public virtual void LoadController()
    {

    }
    public virtual void LoadHitBox()
    {
        if (hitbox != null) return;
        hitbox = GetComponent<BoxCollider2D>();
    }
    public bool IsGrounded()
    {
        return hitbox.IsTouchingLayers(LayerMask.GetMask("Ground"));
    }
    public BoxCollider2D BoxCollider2D
    {
        get { return hitbox; }
    }
}
