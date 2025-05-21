using UnityEngine;

public abstract class ProjectileBase : MonoBehaviour
{
    protected int dmg;
    private Animator anim;
    private Rigidbody2D rb;

    protected bool collision;

    protected void Init()
    {
        collision = false;
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }
    protected void ChangeState(string state, GameObject projectile)
    {
        this.anim.Play(state);
        AnimatorStateInfo stateInfo = this.GetAnim().GetCurrentAnimatorStateInfo(0);
        switch (state)
        {
            case "PreFly":
                if (stateInfo.normalizedTime >= 1f)
                    this.ChangeState("Fly", projectile);
                break;
            case "Fly":
                Debug.Log(projectile + "đang bay");
                break;
            case "Explosion":
                if (stateInfo.normalizedTime >= 1f)
                    Destroy(projectile);
                break;
        }
    }
    
    public abstract void Action();
    public abstract Vector2 InitVelo(int dmg, GameObject entity, Transform dir);

    public Rigidbody2D GetRb()
    {
        return rb;
    }
    public Animator GetAnim()
    {
        return anim;
    }

    
}
