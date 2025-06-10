using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbiRangeATKRedKnight : MonoBehaviour
{
    [SerializeField] protected RedKnightController redKnightController;
    [SerializeField] protected Transform ATKPoint;
    [SerializeField] protected ProjectileType projectileType;
    [SerializeField] protected float scale;

    private ObjectPool pool;
    private Transform target;    

    private void Start()
    {
        LoadTarget();
        LoadComponent();
    }

    protected void LoadComponent()
    {
        LoadController();
        LoadPool();
    }
    protected virtual void LoadController()
    {
        if (this.redKnightController != null)
            return;
        this.redKnightController = transform.GetComponentInParent<RedKnightController>();
    }
    protected virtual void LoadPool()
    {
        if (this.pool != null)
            return;
        ObjectPool objPool = GameObject.FindObjectOfType<ObjectPool>();
        this.pool = objPool.GetComponent<ObjectPool>();
    }
    protected void LoadTarget()
    {
        if (this.target != null)
            return;
        this.target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    public bool CanShootPlayer()
    {
        if (redKnightController == null || redKnightController.RedKnightStats == null)
            return false;

        float atkRange = redKnightController.RedKnightStats.ATKRange;
        Vector2 origin = redKnightController.transform.position;
        return Vector2.Distance(origin, this.target.position) < atkRange * scale;
    }

    public void Shoot()
    {
        StartCoroutine(Shooting(projectileType));
    }
    public IEnumerator Shooting(ProjectileType projectileType)
    {
        yield return new WaitUntil(() => redKnightController != null && redKnightController.RedKnightStats != null);
        pool.GetProjectile(projectileType, redKnightController.RedKnightStats.AttackPower, ATKPoint, target);
    }
}
