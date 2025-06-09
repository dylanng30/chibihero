using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SheepController : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator animator;
    void Start()
    {
        LoadComponnents();
    }

    private void LoadComponnents()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }   

    private Vector2 Direction(Transform player)
    {
        return this.transform.position - player.position;
    }

    private void Move(Vector2 dir)
    {
        rb.velocity = dir.normalized * 7;
    }
    private void Flip()
    {
        this.transform.localScale = rb.velocity.x > 0 ? new Vector3(1,1,1) : new Vector3(-1,1,1);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            animator.Play("Run");
            this.Move(Direction(collision.transform));
            Flip();
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            rb.velocity = Vector2.zero;
            animator.Play("Idle");
        }
    }
}
