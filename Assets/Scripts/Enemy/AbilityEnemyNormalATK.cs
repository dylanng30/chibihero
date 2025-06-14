using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityEnemyNormalATK : MonoBehaviour
{
    [SerializeField] protected LowEnemyController lowEnemyController;
    [SerializeField] protected Transform ATKPoint;
    [SerializeField] protected ProjectileType projectileType;

    private ObjectPool pool;

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
        if (this.lowEnemyController != null)
            return;
        this.lowEnemyController = transform.GetComponentInParent<LowEnemyController>();
    }
    protected virtual void LoadPool()
    {
        if (this.pool != null)
            return;
        this.pool = GameObject.FindObjectOfType<ObjectPool>();
    }

    public bool PlayerInATKRange()
    {
        float atkRange = lowEnemyController.EnemyStats.ATKRange;
        Transform player = GameObject.FindGameObjectWithTag("Player").transform;
        Vector2 target = player.position;
        Vector2 origin = lowEnemyController.transform.position;
        return Vector2.Distance(origin, target) < atkRange;
    }

    public void NormalATK()
    {
        // Play enemy attack sound based on enemy type
        string enemyTypeName = lowEnemyController.EnemyStats.EnemyType.ToString().ToLower();
        AudioManager.PlayEnemyAttack(enemyTypeName, transform.position);
        
        switch (lowEnemyController.EnemyStats.EnemyType)
        {
            case EnemyType.RedPawn:
                StartCoroutine(CloseATK());
                break;
            case EnemyType.RedTNT:
            case EnemyType.RedArcher:
                StartCoroutine(RangeATK(projectileType));
                break;
            default:
                break;
        }
    }

    public IEnumerator CloseATK()
    {
        yield return new WaitUntil(() => lowEnemyController != null && lowEnemyController.EnemyStats != null);
        int Dmg = lowEnemyController.EnemyStats.AttackPower;
        float atkRange = lowEnemyController.EnemyStats.ATKRange;
        LayerMask targetLayer = LayerMask.GetMask("Player");

        Collider2D[] hitPlayers = Physics2D.OverlapCircleAll(ATKPoint.position, atkRange, targetLayer);
        foreach (Collider2D player in hitPlayers)
        {
            var p = player.GetComponentInParent<IDamagable>();
            p.TakeDamage(Dmg, lowEnemyController.gameObject);
        }
    }
    public IEnumerator RangeATK(ProjectileType projectileType)
    {
        yield return new WaitUntil(() => lowEnemyController != null && lowEnemyController.EnemyStats != null);
        pool.GetProjectile(projectileType, lowEnemyController.EnemyStats.AttackPower, ATKPoint, lowEnemyController.gameObject.transform);
    }

}
