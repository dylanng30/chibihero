using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PirateRangeATKAbility : MonoBehaviour
{
    [SerializeField] private PirateController pirateController;
    [SerializeField] private ProjectileType projectileType;
    [SerializeField] private Transform ATKPoint;

    [SerializeField] private ObjectPool pool;
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
    public void RangeAttack()
    {
        StartCoroutine(RangeATK());
    }

    /*public IEnumerator RangeATK()
    {
        yield return new WaitUntil(() => pirateController != null && pirateController.PirateStats != null);
        ProjectileFactory projectileFactory = GameObject.FindObjectOfType<ProjectileFactory>().GetComponent<ProjectileFactory>();
        int dmg = pirateController.PirateStats.AttackPower;
        //projectileFactory.CreateProjectile(projectileType, dmg, pirateController.gameObject, pirateController.gameObject.transform);
    }*/

    public IEnumerator RangeATK()
    {
        yield return new WaitUntil(() => pirateController != null && pirateController.PirateStats != null);
        pool.GetProjectile(projectileType, pirateController.PirateStats.AttackPower, ATKPoint, pirateController.gameObject.transform);
    }
}
