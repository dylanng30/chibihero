using UnityEngine;

public abstract class ProjectileBase : MonoBehaviour
{
    private Vector2 dir;
    protected int dmg;
    private Animator anim;
    private Rigidbody2D rb;

    protected bool collision;

    protected void Init()
    {
        collision = false;
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        this.anim.Play("PreFly");
    }
    
    public abstract void Action();
    public abstract Vector2 InitVelo(int dmg, Transform target, Transform origin);

    protected void Explosion()
    {
        this.collision = true;
        this.anim.Play("Explosion"); 
    }
    public Rigidbody2D GetRb()
    {
        return rb;
    }
    public Animator GetAnim()
    {
        return anim;
    }

    
}
