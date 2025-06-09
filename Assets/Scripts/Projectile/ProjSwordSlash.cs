using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjSwordSlash : ProjectileBase
{
    private float timer = 3f;
    void Awake()
    {
        this.Init();
        this.ChangeState("PreFly", this.gameObject);
    }

    private void FixedUpdate()
    {
        if (!this.collision)
            this.Action();
        else
            this.Rigidbody2D.velocity = Vector2.zero;
        timer -= Time.fixedDeltaTime;
        if (timer <= 0)
            this.ChangeState("Explosion", this.gameObject);

    }
    private void Update()
    {
      
    }
    public override void Action()
    {
        if (this.Rigidbody2D.velocity.x > 0)
            this.transform.localScale = new Vector3(1, 1, 1);
        else
            this.transform.localScale = new Vector3(1, -1, 1);

        float angle = Mathf.Atan2(this.Rigidbody2D.velocity.y, this.Rigidbody2D.velocity.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, angle);
    }

    public override Vector2 InitVelo(int dmg, Transform origin, Transform dir)
    {
        this.dmg = dmg;     
        this.transform.position = origin.position;

        Vector2 Force = dir.position - origin.position;
        return Force.normalized * 500;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(this.collision)
            return;
        if (collision.transform == this.transform)
            return;

        if (collision.gameObject.CompareTag("Enemy"))
        {
            var p = collision.GetComponentInParent<IDamagable>();
            p.TakeDamage(dmg, this.gameObject);
            ChangeState("Explosion", this.gameObject);
        }
    }
}
