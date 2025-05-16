using UnityEngine;

[RequireComponent(typeof(StateManager))]
public class PlayerController : Entity
{
    [SerializeField] Transform ATKPoint;
    [SerializeField] float PSpeed;
    [SerializeField] GameObject prefab;

    private IdleState idleState;
    private RunState runState;
    private NormalATKState normalATKState;
    private SkillState skillState;

    private bool Skill1Locked = true;
    
    void Start()
    {
        targetLayer = LayerMask.GetMask("Enemy");
        this.SetComponents();
        _speed = PSpeed;
        idleState = new IdleState(this);
        runState = new RunState(this);
        normalATKState = new NormalATKState(this);
        skillState = new SkillState(this);
        this.GetStateManager().ChangeState(idleState);    
    }

    public void Update()
    {
        this.Flip();
/*        if(Skill1Locked == false)
        {
            
            Debug.Log("da mo khoa skill1");
        }*/
    }
    private void FixedUpdate()
    {

    }
    
    public void Move(float leftRight)
    {
        GetRb().velocity = new Vector2(leftRight * _speed, GetRb().velocity.y);
    }    

    public void NormalATK()
    {
        if (ATKPoint != null)
        {
            Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(ATKPoint.position, ATKRange, targetLayer);
            foreach (Collider2D enemy in hitEnemies)
            {
                Enemy e = enemy.GetComponent<Enemy>();
                e.TakeDamage(_Damage, this.transform);
            }
        }
        else
        {
            Debug.LogError("ATKPoint kco");
        }
    }
    public void Skill1()
    {
        
    }

    public IdleState GetIdleState()
    {
        return idleState;
    }
    public RunState GetRunState()
    {
        return runState;
    }
    public NormalATKState GetNormalATKState()
    {
        return normalATKState;
    }
    public SkillState GetSkill1State()
    {
        return skillState;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(ATKPoint.position, ATKRange);
    }
}
