using UnityEngine;

public class ProjTNT : ProjectileBase
{
    private Vector3 rotationSpeed;

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

    public override void Action()
    {
        this.transform.localRotation *= Quaternion.Euler(rotationSpeed * Time.fixedDeltaTime);
    }

    public override Vector2 InitVelo(int dmg, Transform origin, Transform dir)
    {
        this.collision = false;
        this.transform.position = origin.position;
        this.dmg = dmg;
        Transform target = GameObject.FindObjectOfType<PlayerController>().transform;
        Vector3 direction = target.position - transform.position;
        float AngleR = 0;
        if (direction.x < 0)
            AngleR = -Mathf.Abs(AngleRandom) * Mathf.Deg2Rad;
        else
            AngleR = Mathf.Abs(AngleRandom) * Mathf.Deg2Rad;

        float v2 = (10 / ((Mathf.Tan(AngleR) * direction.x - direction.y) / (direction.x * direction.x)) / (2 * Mathf.Cos(AngleR) * Mathf.Cos(AngleR)));
        v2 = Mathf.Abs(v2);
        float V = Mathf.Sqrt(v2);
        Vector2 Force = Vector2.zero;
        Force.x = V * Mathf.Cos(AngleR);
        Force.y = V * Mathf.Sin(AngleR);
        return Force * DistaceRandom * direction.normalized.x;
    }

    private float AngleRandom
    {
        get
        {
            float randomAngle = Random.Range(50f, 70f);
            return randomAngle;
        }
    }

    private float DistaceRandom
    {
        get
        {
            float randomDistance = Random.Range(45f, 55f);
            return randomDistance;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (this.collision)
            return;

        if (collision.gameObject.CompareTag("Player"))
        {
            var p = collision.GetComponentInParent<IDamagable>();
            p.TakeDamage(dmg, this.gameObject);
            Pool.ReturnProjectile(this);
        }
        else if (collision.gameObject.CompareTag("Ground"))
        {
            this.ChangeState("Explosion", this.gameObject);
            this.collision = true;
        }
    }
}
