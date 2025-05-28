using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

public class ProjTNT : ProjectileBase
{
    private Vector3 rotationSpeed;
    public float Angle = 50f;

    void Awake()
    {
        rotationSpeed = new Vector3(0, 0, -360);
        this.Init();
        this.ChangeState("PreFly", this.gameObject);
    }

    private void FixedUpdate()
    {
        if (!this.collision)
            this.Action();
        else
            this.Rigidbody2D.velocity = Vector2.zero;
    }

    public override Vector2 InitVelo(int dmg, Transform origin, Transform dir)
    {
        this.collision = false;
        this.dmg = dmg;
        this.transform.position = origin.position;

        Transform target = GameObject.FindObjectOfType<PlayerController>().transform;
        Vector3 direction = target.position - origin.position;
        float AngleR = 0;
        if (direction.x < 0)
            AngleR = -Mathf.Abs(Angle) * Mathf.Deg2Rad;
        else
            AngleR = Mathf.Abs(Angle) * Mathf.Deg2Rad;

        float v2 = (10 / ((Mathf.Tan(AngleR) * direction.x - direction.y) / (direction.x * direction.x)) / (2 * Mathf.Cos(AngleR) * Mathf.Cos(AngleR)));
        v2 = Mathf.Abs(v2);
        float V = Mathf.Sqrt(v2);
        Vector2 Force = Vector2.zero;
        Force.x = V * Mathf.Cos(AngleR);
        Force.y = V * Mathf.Sin(AngleR);
        return Force * 50 * direction.normalized.x;
    }

    public override void Action()
    {
        Quaternion deltaRotation = Quaternion.Euler(rotationSpeed * Time.deltaTime);
        transform.localRotation *= deltaRotation;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (this.collision)
            return;

        if (collision.gameObject.CompareTag("Player"))
        {
            var p = collision.GetComponentInParent<IDamagable>();
            p.TakeDamage(dmg, this.gameObject);
            this.ChangeState("Explosion", this.gameObject);
        }
        else if (collision.gameObject.CompareTag("Ground"))
        {
            this.ChangeState("Explosion", this.gameObject);
            this.collision = true;

        }
    }
}
