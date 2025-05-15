using UnityEngine;

public abstract class ProjectileBase : MonoBehaviour
{
    private Vector2 dir;
    private float AngleR, V;
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

    public Vector2 InitVelo(int dmg,float Angle, Transform target, Transform origin)
    {
        this.dmg = dmg;
        dir = target.position - origin.position;

        if (dir.x < 0)
            this.AngleR = -Mathf.Abs(Angle) * Mathf.Deg2Rad;
        else
            this.AngleR = Mathf.Abs(Angle) * Mathf.Deg2Rad;

        float v2 = (10 / ((Mathf.Tan(AngleR) * dir.x - dir.y) / (dir.x * dir.x)) / (2 * Mathf.Cos(AngleR) * Mathf.Cos(AngleR)));
        v2 = Mathf.Abs(v2);
        V = Mathf.Sqrt(v2);
        Vector2 Force = Vector2.zero;
        Force.x = V * Mathf.Cos(AngleR);
        Force.y = V * Mathf.Sin(AngleR);
        return Force * 50 * dir.normalized.x;
    }

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
