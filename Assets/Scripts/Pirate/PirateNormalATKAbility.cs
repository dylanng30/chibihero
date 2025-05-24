using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PirateATKAbility : MonoBehaviour
{
    [SerializeField] protected PirateController pirateController;
    [SerializeField] protected Transform ATKPoint;
    [SerializeField] protected ProjectileType projectileType;

    private float attackRange;
    private int dmg;


    private void Start()
    {
        LoadComponent();
        StartCoroutine(LoadStats());
    }
    protected void LoadComponent()
    {
        LoadController();
    }
    protected virtual void LoadController()
    {
        if (this.pirateController != null)
            return;
        this.pirateController = GetComponentInParent<PirateController>();
    }
    private IEnumerator LoadStats()
    {
        yield return new WaitUntil(() => pirateController != null && pirateController.PirateStats != null);
        attackRange = pirateController.PirateStats.ATKRange;
        dmg = pirateController.PirateStats.AttackPower;
    }
    public void NormalAttack()
    {
        StartCoroutine(CloseATK());
    }

    public IEnumerator CloseATK()
    {
        yield return new WaitUntil(() => pirateController != null && pirateController.PirateStats != null);

        LayerMask enemyLayer = LayerMask.GetMask("Player");
        Collider2D[] hitPlayers = Physics2D.OverlapCircleAll(ATKPoint.position, 0.5f, enemyLayer);
        foreach (Collider2D player in hitPlayers)
        {
            var p = player.GetComponentInParent<PlayerController>();
            p.DamageManager.TakeDamage(dmg, pirateController.gameObject);
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
        //projectileFactory.CreateProjectile(projectileType, dmg, pirateController.gameObject, pirateController.gameObject.transform);
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
