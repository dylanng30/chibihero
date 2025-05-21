using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class CollisionPlayer : MonoBehaviour
{
    [SerializeField] protected BoxCollider2D hitbox;
    [SerializeField] protected PlayerController playerController;
    void Start()
    {
        LoadComponent();
    }
    protected void LoadComponent()
    {
        LoadBoxCollider2D();
        LoadPlayerController();
    }
    protected void LoadPlayerController()
    {
        if (playerController != null) return;
        playerController = GetComponentInParent<PlayerController>();
        //Debug.Log("Add PlayerController to: " + gameObject);
    }

    protected void LoadBoxCollider2D()
    {
        if (hitbox != null) return;
        hitbox = GetComponent<BoxCollider2D>();
       // Debug.Log("Add BoxCollider2D to: " + gameObject);
    }

    public bool IsGrounded()
    {
        return hitbox.IsTouchingLayers(LayerMask.GetMask("Ground"));
    }

    public BoxCollider2D Hitbox
    {
        get { return hitbox; }
    }
}
