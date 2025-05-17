using Unity.VisualScripting;
using UnityEngine;

public abstract class Entity : MonoBehaviour
{
    [SerializeField] protected int _maxHealth;
    public int _currentHealth;
    [SerializeField] public int _Damage;
    [SerializeField] protected int _Armor;
    protected float _speed;
    [SerializeField] protected float ATKRange;
    [SerializeField] protected float jumpPower;

    protected LayerMask groundLayer;
    [SerializeField] Transform groundCheck;
    //Components
    private Rigidbody2D rb;
    private Animator anim;
    private BoxCollider2D hitbox;
    private StateManager stateManager;

    protected GameObject _currentTarget;
    protected LayerMask targetLayer;

    protected void SetComponents()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        hitbox = GetComponent<BoxCollider2D>();
        stateManager = GetComponent<StateManager>();
        groundLayer = LayerMask.GetMask("Ground");
    }
    public bool IsGrounded()
    {
        Vector2 sizeCheck = new Vector2(hitbox.size.x, 0.1f);
        LayerMask groundLayer = LayerMask.GetMask("Ground");
        return Physics2D.OverlapCapsule(groundCheck.position, sizeCheck, CapsuleDirection2D.Horizontal, 0f, groundLayer);
    }
    protected void Flip()
    {
        if (this.GetRb().velocity.x > 0)
            this.transform.localScale = new Vector3(1, 1, 1);
        else if (this.GetRb().velocity.x < 0)
            this.transform.localScale = new Vector3(-1, 1, 1);
    }
    public void Jump()
    {
        this.GetRb().velocity = new Vector2(this.GetRb().velocity.x, jumpPower);
    }    
    
    public void TakeDamage(int dmg, Transform dir)
    {
        int value = dmg - _Armor;
        _currentHealth -= value;
        _currentHealth = Mathf.Clamp(_currentHealth, 0, _maxHealth);
        KnockBack(this.transform.position - dir.position);
    }
    public void SetSpeed(float speed)
    {
        this._speed = speed;
    }
    public float GetSpeed()
    {
        return _speed;
    }

    private void KnockBack(Vector2 dir)
    {
        dir.y = 0.3f;
        this.rb.AddForce(dir.normalized * 100);
    }
    public int GetDamage()
    {
        return _Damage;
    }
    public Rigidbody2D GetRb()
    {
        return rb;
    }
    public Animator GetAnim()
    {
        return anim;
    }
    public BoxCollider2D GetHitbox()
    {
        return hitbox;
    }
    public StateManager GetStateManager()
    {
        return stateManager;
    }
}
