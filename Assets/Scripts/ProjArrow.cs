using UnityEngine;

public class ProjArrow : ProjectileBase
{
    void Awake()
    {
        this.Init();
    }

    private void FixedUpdate()
    {
        if (!this.collision)
            this.Action();
        else
            this.GetRb().velocity = Vector2.zero;

        AnimatorStateInfo stateInfo = this.GetAnim().GetCurrentAnimatorStateInfo(0);
        if (stateInfo.IsName("PreFly") && stateInfo.normalizedTime >= 1f)
            this.GetAnim().Play("Fly");
        if (stateInfo.IsName("Explosion") && stateInfo.normalizedTime >= 1f)
            Destroy(this.gameObject);

    }
    public override void Action()
    {
        if (this.GetRb().velocity != Vector2.zero)
        {
            float angle = Mathf.Atan2(this.GetRb().velocity.y, this.GetRb().velocity.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0f, 0f, angle);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.GetComponent<PlayerController>().TakeDamage(dmg, this.transform);
            Destroy(this.gameObject);
        }
        else if (collision.gameObject.CompareTag("Ground"))
        {
            Explosion();
        }
    }
}
