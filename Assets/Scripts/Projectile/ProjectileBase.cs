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

        if (state == "Explosion")
        {
            yield return null;

            AnimatorClipInfo[] clipInfo = anim.GetCurrentAnimatorClipInfo(0);
            if (clipInfo.Length > 0)
            {
                float duration = clipInfo[0].clip.length;
                yield return new WaitForSeconds(duration);
            }
            else
            {
                yield return new WaitForSeconds(0.5f);
            }                

            pool.ReturnProjectile(projectile.GetComponent<ProjectileBase>());
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
