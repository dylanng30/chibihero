using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbiNormalATKRedKnight : MonoBehaviour
{
    [SerializeField] protected RedKnightController redKnightController;
    [SerializeField] protected Transform ATKPoint;

    private ObjectPool pool;

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
        if (this.redKnightController != null)
            return;
        this.redKnightController = GetComponentInParent<RedKnightController>();
    }

    public bool CanAttack()
    {
        float atkRange = redKnightController.RedKnightStats.ATKRange;
        Transform player = GameObject.FindGameObjectWithTag("Player").transform;
        Vector2 target = player.position;
        Vector2 origin = redKnightController.transform.position;
        return Vector2.Distance(origin, target) < atkRange;
    }

    public void NormalATK()
    {
        StartCoroutine(CloseATK());
    }

    public IEnumerator CloseATK()
    {
        yield return new WaitUntil(() => redKnightController != null && redKnightController.RedKnightStats != null);

        int dmg = redKnightController.RedKnightStats.AttackPower;
        float atkRange = redKnightController.RedKnightStats.ATKRange;
        LayerMask targetLayer = LayerMask.GetMask("Player");

        Collider2D[] hitPlayers = Physics2D.OverlapCircleAll(ATKPoint.position, atkRange, targetLayer);
        foreach (Collider2D player in hitPlayers)
        {
            var p = player.GetComponentInParent<IDamagable>();
            p.TakeDamage(dmg, redKnightController.gameObject);
        }
    }
}
