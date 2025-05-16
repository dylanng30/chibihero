using UnityEngine;
public abstract class Enemy : Entity
{ 
    protected RaycastHit2D hit;
    [SerializeField] private float ESpeed = 5;

    private void Awake()
    {
        _speed = ESpeed;
        _currentHealth = _maxHealth;
    }
    protected void InitTarget()
    {
        _currentTarget = GameObject.FindGameObjectWithTag("Player");
        targetLayer = LayerMask.GetMask("Player");
    }
    public void Move()
    {
        this.GetRb().velocity = new Vector2(_speed, this.GetRb().velocity.y);
    }

    protected void FlipToPlayer()
    {
        if (_currentTarget == null)
            return;
        else
        {
            Vector2 dir = _currentTarget.transform.position - this.transform.position;
            if (dir.x > 0)
                this.transform.localScale = new Vector3(1, 1, 1);
            else
                this.transform.localScale = new Vector3(-1, 1, 1);
        }
    }

    public bool InATKRange()
    {
        if (_currentTarget == null)
            return false;
        return Vector3.Distance(this.transform.position, _currentTarget.transform.position) < ATKRange;
    }
    public void ChasePlayer()
    {
        if (_currentTarget == null)
            return;

        if (_currentTarget.transform.position.x - this.transform.position.x > 0)
            _speed = ESpeed;
        else
            _speed = -ESpeed;
    }
    public bool DetectObstacle()
    {
        if (this._currentTarget == null)
            return false;

        Vector2 direction = transform.right * Mathf.Sign(transform.localScale.x);
        hit = Physics2D.Raycast(this.transform.position, direction, 2f, LayerMask.GetMask("Ground"));

        if (hit.collider == null)
            return false;

        if (hit.collider.CompareTag("Ground"))
            return true;    

        return false;
    }
    public bool NextToWall()
    {
        if (this._currentTarget == null)
            return false;

        Vector2 direction = transform.right * Mathf.Sign(transform.localScale.x);
        hit = Physics2D.Raycast(this.transform.position, direction, 0.5f, LayerMask.GetMask("Ground"));

        if (hit.collider == null)
            return false;

        if (hit.collider.CompareTag("Ground"))
            return true;

        return false;
    }
    public void ResetStat()
    {
        _speed = ESpeed;
        _currentHealth = _maxHealth;
    }

    public void Die()
    {
        this.gameObject.SetActive(false);
        ResetStat();
    }

}
