using UnityEngine;

public class ProjTNT : ProjectileBase
{
    private Vector3 rotationSpeed;

    void Awake()
    {
        rotationSpeed = new Vector3(0, 0, -360);
        this.Init();
    }

    private void FixedUpdate()
    {
        if (!collision)
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
        Quaternion deltaRotation = Quaternion.Euler(rotationSpeed * Time.deltaTime);
        transform.localRotation *= deltaRotation;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.GetComponent<PlayerController>().TakeDamage(dmg, this.transform);
            Explosion();
        }
        else if (collision.gameObject.CompareTag("Ground"))
        {
            Explosion();
        }
    }
}
