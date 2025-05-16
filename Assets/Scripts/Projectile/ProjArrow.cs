using UnityEngine;

public class ProjArrow : ProjectileBase
{
    public float Angle = 50f;
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
    public override Vector2 InitVelo(int dmg, Transform target, Transform origin)
    {
        this.dmg = dmg;
        Vector3 dir = target.position - origin.position;
        float AngleR = 0;
        if (dir.x < 0)
            AngleR = -Mathf.Abs(Angle) * Mathf.Deg2Rad;
        else
            AngleR = Mathf.Abs(Angle) * Mathf.Deg2Rad;

        float v2 = (10 / ((Mathf.Tan(AngleR) * dir.x - dir.y) / (dir.x * dir.x)) / (2 * Mathf.Cos(AngleR) * Mathf.Cos(AngleR)));
        v2 = Mathf.Abs(v2);
        float V = Mathf.Sqrt(v2);
        Vector2 Force = Vector2.zero;
        Force.x = V * Mathf.Cos(AngleR);
        Force.y = V * Mathf.Sin(AngleR);
        return Force * 50 * dir.normalized.x;
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
