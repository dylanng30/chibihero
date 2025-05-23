using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityEnemyNormalATK : MonoBehaviour
{
    [SerializeField] protected LowEnemyController lowEnemyController;
    [SerializeField] protected Transform ATKPoint;
    private ProjectileType projectileType;

    private void Start()
    {
        LoadComponent();
    }
    protected void LoadComponent()
    {
        LoadController();
    }
    protected virtual void LoadController()
    {
        if (this.lowEnemyController != null)
            return;
        this.lowEnemyController = transform.GetComponentInParent<LowEnemyController>();
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
        switch (lowEnemyController.EnemyStats.EnemyType)
        {
            case EnemyType.RedPawn:
                StartCoroutine(CloseATK());
                break;
            case EnemyType.RedTNT:
                projectileType = ProjectileType.TNT;
                StartCoroutine(RangeATK(projectileType));
                break;
            case EnemyType.RedArcher:
                projectileType = ProjectileType.Arrow;
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
            var p = player.GetComponentInParent<PlayerController>();
            p.DamageManager.TakeDamage(Dmg, lowEnemyController.gameObject);
        }
    }

    public IEnumerator RangeATK(ProjectileType projectileType)
    {
        yield return new WaitUntil(() => lowEnemyController != null && lowEnemyController.EnemyStats != null);
        ProjectileFactory projectileFactory = GameObject.FindObjectOfType<ProjectileFactory>().GetComponent<ProjectileFactory>();
        int dmg = lowEnemyController.EnemyStats.AttackPower;
        projectileFactory.CreateProjectile(projectileType, dmg, lowEnemyController.gameObject, lowEnemyController.gameObject.transform);
    }
}
