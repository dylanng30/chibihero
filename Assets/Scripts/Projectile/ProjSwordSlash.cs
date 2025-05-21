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
    private void Update()
    {
        
    }
    public override void Action()
    {
        if(this.GetRb().velocity.x > 0)
            this.transform.localScale = new Vector3(1 , 1, 1);
        else
            this.transform.localScale = new Vector3(-1, 1, 1);
    }
    public override Vector2 InitVelo(int dmg, GameObject entity, Transform dir)
    {
        this.dmg = dmg;
        Vector2 Force = new Vector2(dir.localScale.x, 0);
        return Force * 200;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(this.collision)
            return;
        if (collision.gameObject.CompareTag("Enemy"))
        {
            this.collision = true;
            collision.gameObject.GetComponent<Entity>().TakeDamage(dmg, this.gameObject.transform);
            this.ChangeState("Explosion", this.gameObject);
        }
    }
}
