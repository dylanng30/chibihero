using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TestTools;

public class MovementBarrel : MonoBehaviour
{
    private GameObject player;

    [SerializeField] protected BarrelController controller;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        LoadComponent();
    }
    private void Update()
    {
        Flip();
    }
    protected void LoadComponent()
    {
        LoadController();
    }
    protected virtual void LoadController()
    {
        if (this.controller != null)
            return;
        this.controller = GetComponentInParent<BarrelController>();
    }
    public void Move()
    {
        if (player == null) return;
        Vector2 direction = player.transform.position - this.transform.position;
        int speed = controller.BarrelStats.MoveSpeed;
        controller.Rigidbody2D.velocity = new Vector2(direction.normalized.x * speed , controller.Rigidbody2D.velocity.y);
    }
    public void Flip()
    {
        if (player == null) return;
        Vector2 direction = player.transform.position - this.transform.position;
        controller.transform.localScale = direction.x > 0 ? new Vector3(1, 1, 1) : new Vector3(-1, 1, 1);
    }

    public bool InRange
    {
        get
        {
            float range = controller.BarrelStats.ATKRange;
            return Vector3.Distance(this.transform.position, player.transform.position) < range;
        }
    }
}
