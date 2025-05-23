using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PirateNormalATKAbility : MonoBehaviour
{
    [SerializeField] protected PirateController pirateController;
    [SerializeField] protected Transform ATKPoint;

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
        Collider2D[] hitPlayers = Physics2D.OverlapCircleAll(ATKPoint.position, attackRange, enemyLayer);
        foreach (Collider2D player in hitPlayers)
        {
            var p = player.GetComponentInParent<PlayerController>();
            p.DamageManager.TakeDamage(dmg, pirateController.gameObject);
        }
    }
    public bool PlayerInNormalATKRange()
    {
        Vector2 target = GameObject.FindGameObjectWithTag("Player").transform.position;
        Vector2 origin = pirateController.transform.position;
        return Vector2.Distance(origin, target) < attackRange;
    }
}
