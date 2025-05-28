using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PirateATKAbility : MonoBehaviour
{
    [SerializeField] protected PirateController pirateController;
    [SerializeField] protected Transform ATKPoint;
    [SerializeField] protected ProjectileType projectileType;

    [SerializeField] protected ObjectPool pool;

    private float attackRange;
    private int dmg;


    private void Start()
    {
        LoadComponent();
    }
    protected void LoadComponent()
    {
        LoadController();
        LoadPool();
    }
    protected virtual void LoadController()
    {
        if (this.pirateController != null)
            return;
        this.pirateController = GetComponentInParent<PirateController>();
    }
    protected virtual void LoadPool()
    {
        if (this.pool != null)
            return;
        this.pool = GameObject.FindObjectOfType<ObjectPool>().GetComponent<ObjectPool>();
    }
    public void NormalAttack()
    {
        StartCoroutine(CloseATK());
    }

    public IEnumerator CloseATK()
    {
        yield return new WaitUntil(() => pirateController != null && pirateController.PirateStats != null);
        int dmg = pirateController.PirateStats.AttackPower;
        LayerMask enemyLayer = LayerMask.GetMask("Player");
        Collider2D[] hitPlayers = Physics2D.OverlapCircleAll(ATKPoint.position, 0.5f, enemyLayer);
        foreach (Collider2D player in hitPlayers)
        {
            var p = player.GetComponentInParent<IDamagable>();
            Debug.Log($"Pirate attacking player: {player.name} with damage: {dmg}");
            p.TakeDamage(dmg, pirateController.gameObject);
        }
    }

    public void RangeAttack()
    {
        StartCoroutine(RangeATK(projectileType));
    }
    public IEnumerator RangeATK(ProjectileType projectileType)
    {
        yield return new WaitUntil(() => pirateController != null && pirateController.PirateStats != null);
        ProjectileFactory projectileFactory = GameObject.FindObjectOfType<ProjectileFactory>().GetComponent<ProjectileFactory>();
        int dmg = pirateController.PirateStats.AttackPower;
        pool.GetProjectile(projectileType, dmg, ATKPoint, pirateController.gameObject.transform);
    }
    public bool PlayerInATKRange()
    {
        float atkRange = pirateController.PirateStats.ATKRange;
        Transform player = GameObject.FindGameObjectWithTag("Player").transform;
        Vector2 target = player.position;
        Vector2 origin = pirateController.transform.position;
        return Vector2.Distance(origin, target) < atkRange;
    }
}
