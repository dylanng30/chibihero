using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToPlayer : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;

    private float moveRange = 5f;
    [SerializeField] private string nextScene;
    private GameObject player;

    //Component
    private Animator anim;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        nextScene = this.gameObject.name;
        anim = GetComponent<Animator>();

    }
    private void FixedUpdate()
    {
        this.Flip();
        if (InRange())
        {
            this.Move();
            SetAnim(true);
            return;
        }
        
        SetAnim(false);
    }
    private void Move()
    {
        this.transform.position = Vector2.MoveTowards(this.transform.position, player.transform.position, moveSpeed * Time.deltaTime);
    }
    private bool InRange()
    {
        return Vector3.Distance(this.transform.position, player.transform.position) < moveRange;
    }
    private void SetAnim(bool isRunning)
    {
        if (anim == null)
            return;

        if (isRunning)
            anim.Play("Run");
        else
            anim.Play("Idle");
    }
    private void Flip()
    {
        if(player == null) return;
        Vector2 dir = player.transform.position - this.transform.position;
        this.transform.localScale = dir.x > 0 ? new Vector3(1, 1, 1) : new Vector3(-1, 1, 1);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // Check if player is invincible before allowing scene transition
            PlayerController playerController = collision.GetComponentInParent<PlayerController>();
            if (playerController != null && playerController.DamageManager.IsInvincible)
            {
                Debug.Log("Player is invincible, enemy collision ignored!");
                return;
            }

            GameManager.Instance.ChangeState(GameState.Fighting);
            GameManager.Instance.NextScene(nextScene, this.gameObject);
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(this.transform.position, moveRange);
    }
}
