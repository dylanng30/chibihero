using System.Collections;
using UnityEngine;

public abstract class ProjectileBase : MonoBehaviour
{
    protected int dmg;
    private Animator anim;
    private Rigidbody2D rb;

    protected bool collision = false;

    protected ObjectPool pool;
    public void SetPool(ObjectPool pool) => this.pool = pool;

    protected void Init()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }
    protected IEnumerator ChangeStateCoroutine(string state, GameObject projectile)
    {
        anim.Play(state);

        switch (state)
        {
            case "PreFly":
                yield return new WaitForSeconds(anim.GetCurrentAnimatorStateInfo(0).length);
                StartCoroutine(ChangeStateCoroutine("Fly", projectile));
                break;
            case "Fly":
                break;
            case "Explosion":
                yield return new WaitForSeconds(anim.GetCurrentAnimatorStateInfo(0).length);
                pool.ReturnProjectile(projectile.GetComponent<ProjectileBase>());
                break;
        }
    }

    public void ChangeState(string state, GameObject projectile)
    {
        StartCoroutine(ChangeStateCoroutine(state, projectile));
    }

    public abstract void Action();
    public abstract Vector2 InitVelo(int dmg, Transform entity, Transform dir);

    public Rigidbody2D Rigidbody2D
    {
        get
        {
            return rb;
        }
    }
    public Animator Anim
    {
        get
        {
            return anim;
        }
    }

    public ObjectPool Pool
    {
        get
        {
            return pool;
        }
    }
}
