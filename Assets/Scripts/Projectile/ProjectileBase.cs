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

        // Wait for current animation to finish
        yield return new WaitForSeconds(anim.GetCurrentAnimatorStateInfo(0).length);

        switch (state)
        {
            case "PreFly":
                StartCoroutine(ChangeStateCoroutine("Fly", projectile));
                break;
            case "Fly":
                break;
            case "Explosion":
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

    public Rigidbody2D GetRb()
    {
        return rb;
    }
    public Animator GetAnim()
    {
        return anim;
    }

    
}
