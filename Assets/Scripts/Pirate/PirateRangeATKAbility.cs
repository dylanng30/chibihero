using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PirateRangeATKAbility : MonoBehaviour
{
    [SerializeField] private PirateController pirateController;
    [SerializeField] private ProjectileType projectileType;
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
        if (this.pirateController != null)
            return;
        this.pirateController = GetComponentInParent<PirateController>();
    }
    public void RangeAttack()
    {
        StartCoroutine(RangeATK());
    }

    public IEnumerator RangeATK()
    {
        yield return new WaitUntil(() => pirateController != null && pirateController.PirateStats != null);
        ProjectileFactory projectileFactory = GameObject.FindObjectOfType<ProjectileFactory>().GetComponent<ProjectileFactory>();
        int dmg = pirateController.PirateStats.AttackPower;
        projectileFactory.CreateProjectile(projectileType, dmg, pirateController.gameObject, pirateController.gameObject.transform);
    }
}
