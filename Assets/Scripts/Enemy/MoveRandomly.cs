using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.XR;

public class MoveRandomly : MonoBehaviour
{
    [SerializeField] private float moveSpeed;

    private float moveRange = 2f;
     private float waitTime = 2f;

    private Vector2 centerPosition, targetPosition;
    private float timer;
    private string nextScene;
    private bool isRunning = false;

    //Component
    private Animator anim;

    private void Start()
    {
        centerPosition = this.transform.position;
        targetPosition = this.transform.position;
        timer = waitTime;
        nextScene = this.gameObject.name;
        nextScene = nextScene.Replace("(Clone)", "");
        anim = GetComponent<Animator>();

    }
    private void FixedUpdate()
    {
        this.Move();
        this.SetAnim();

        float dis = Vector2.Distance(this.transform.position, targetPosition);
        if (dis < 0.1f)
        {
            isRunning = false;
            if (timer > 0)
                timer -= Time.deltaTime;
            else
            {
                isRunning = true;
                this.ChangeTargetPosition();
                timer = waitTime;
            }                
        }
        this.Flip();

        
    }
    private void Move()
    {
        this.transform.position = Vector2.MoveTowards(this.transform.position, targetPosition, moveSpeed);
    }
    private void SetAnim()
    {
        if (isRunning)
            anim.Play("Run");
        else
            anim.Play("Idle");
    }
    private void Flip()
    {
        Vector2 dir = targetPosition - (Vector2)this.transform.position;
        this.transform.localScale = dir.x > 0 ? new Vector3(1, 1, 1) : new Vector3(-1, 1, 1);
    }
    private void ChangeTargetPosition()
    {
        Vector2 randomRange = Random.insideUnitCircle * moveRange;
        targetPosition = new Vector2(centerPosition.x + randomRange.x, centerPosition.y + randomRange.y);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            GameManager.Instance.ChangeState(GameState.Fighting);
            GameManager.Instance.NextScene(nextScene, gameObject);            
        }            
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(centerPosition, moveRange);
    }

}
