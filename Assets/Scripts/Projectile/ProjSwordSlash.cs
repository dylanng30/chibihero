using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ProjSwordSlash : ProjectileBase
{
    private float timer = 2f;
    private Transform origin;
    void Awake()
    {
        this.Init();
        this.ChangeState("PreFly", this.gameObject);
    }

    private void FixedUpdate()
    {
        this.Action();
        if(timer > 0)
            timer -= Time.deltaTime;
        else
            ChangeState("Explosion", this.gameObject);

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
        this.origin = origin;
        this.transform.position = origin.position;

        Vector2 Force = dir.position - origin.position;
        return Force.normalized * 500;
    }

    private void UpdateExpForPlayer()
    {
        PlayerController playerController = origin.parent.GetComponent<PlayerController>();
        Debug.Log(playerController);
        playerController.EXPManager.AddEXP(1000);
    }

    private void DoDamage(Collider2D collision)
    {
        var p = collision.GetComponentInParent<IDamagable>();
        p.TakeDamage(dmg, this.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            DoDamage(collision);
            UpdateExpForPlayer();
        }
        else if(collision.gameObject.CompareTag("Player"))
            DoDamage(collision);
    }
}
