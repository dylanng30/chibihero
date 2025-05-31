using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorManager : MonoBehaviour
{
    [SerializeField] protected Animator animator;
    [SerializeField] protected BoxCollider2D hitbox;

    void Start()
    {
        LoadComponents();
    }

    private void LoadComponents()
    {
        LoadAnimator();
        LoadCollision();
    }
    protected void LoadAnimator()
    {
        if (GetComponent<Animator>() != null) return;
        animator = GetComponentInChildren<Animator>();
    }
    protected void LoadCollision()
    {
        if (GetComponent<BoxCollider>() != null) return;
        hitbox = GetComponentInChildren<BoxCollider2D>();
    }

}
