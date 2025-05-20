using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Rigidbody2D))]

public class PhysicsPlayer : MonoBehaviour
{
    [SerializeField] protected Rigidbody2D rb;
    [SerializeField] protected PlayerController playerController;
    void Start()
    {
        LoadComponent();
    }
    protected void LoadComponent()
    {
        LoadRigidBody2D();
        LoadPlayerController();
    }
    public void LoadRigidBody2D()
    {
        if(rb != null) return;
        rb = GetComponent<Rigidbody2D>();
        this.SetRigidBody2D();
        //Debug.Log("Add Rigidbody2D to " + gameObject);
    }
    protected void LoadPlayerController()
    {
        if (playerController != null) return;
        playerController = GetComponent<PlayerController>();
        //Debug.Log("Add PlayerController to: " + gameObject);
    }

    public void SetRigidBody2D()
    {
        rb.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
        rb.interpolation = RigidbodyInterpolation2D.Interpolate;
        rb.sleepMode = RigidbodySleepMode2D.NeverSleep;
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        SetGravity();
    }

    public void SetGravity()
    {
        string scene = SceneManager.GetActiveScene().name;
        if (scene.Contains("TopDown"))
            SetMode(PlayerMode.TopDown);
        else
            SetMode(PlayerMode.Platform);
    }
    protected void SetMode(PlayerMode mode)
    {
        if (mode == PlayerMode.TopDown)
            rb.gravityScale = 0;
        if (mode == PlayerMode.Platform)
            rb.gravityScale = 1;
    }

    public Rigidbody2D Rigidbody2D
    {
        get { return rb; }
    }


}
