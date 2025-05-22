using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsBase : MonoBehaviour
{
    [SerializeField] protected Rigidbody2D rb;

    protected virtual void Awake()
    {
        LoadComponent();
    }
    public virtual void LoadComponent()
    {
        LoadRigidBody2D();
        LoadController();
    }

    public virtual void LoadController()
    {

    }
    public virtual void LoadRigidBody2D()
    {
        if (rb != null) return;
        rb = GetComponent<Rigidbody2D>();
    }
    public Rigidbody2D Rigidbody2D
    {
        get { return rb; }
    }

}
