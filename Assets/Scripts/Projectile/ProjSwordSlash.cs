using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjSwordSlash : ProjectileBase
{
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
    }
    private void Update()
    {
      
    }
    public override void Action()
    {
        if(this.Rigidbody2D.velocity.x > 0)
            this.transform.localScale = new Vector3(1 , 1, 1);
        else
            this.transform.localScale = new Vector3(-1, 1, 1);
    }
    public override Vector2 InitVelo(int dmg, Transform origin, Transform dir)
    {
        this.dmg = dmg;
        Vector2 Force = new Vector2(dir.localScale.x, 0);
        return Force * 500;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(this.collision)
            return;
        if (collision.gameObject.CompareTag("Enemy"))
        {
            var p = collision.GetComponentInParent<IDamagable>();
            p.TakeDamage(dmg, this.gameObject);
            ChangeState("Explosion", this.gameObject);
        }
    }
}
