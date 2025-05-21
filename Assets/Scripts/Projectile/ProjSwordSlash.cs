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
            this.GetRb().velocity = Vector2.zero;
    }
    public override void Action()
    {

    }
    public override Vector2 InitVelo(int dmg, GameObject entity, Transform dir)
    {
        this.dmg = dmg;
        Vector2 Force = new Vector2(dir.localScale.x, 0);
        return Force * 10;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerController p = collision.GetComponentInParent<PlayerController>();
            p.DamageManager.TakeDamage(dmg, this.gameObject);
            this.ChangeState("Explosion", this.gameObject);
        }
        else if (collision.gameObject.CompareTag("Ground"))
        {
            this.ChangeState("Explosion", this.gameObject);
        }
    }
}
