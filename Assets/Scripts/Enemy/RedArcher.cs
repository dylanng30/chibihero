
using UnityEngine;

[RequireComponent(typeof(StateManager))]
public class RedArcher : LowEnemy
{
    public ProjectileFactory projectileFactory;
    public ProjectileType projectileType = ProjectileType.Arrow;

    private void Start()
    {
        this.InitTarget();
        this.SetComponents();
        this.InitState();
        projectileFactory = GameObject.FindObjectOfType<ProjectileFactory>().GetComponent<ProjectileFactory>();
    }

    void Update()
    {
        this.FlipToPlayer();
        if (_currentHealth <= 0)
        {
            this.gameObject.SetActive(false);
            EnemyManager.Instance.UnregisterEnemy(this.gameObject);
        }
    }

    public override void NormalATK()
    {
        projectileFactory.CreateProjectile(projectileType, _Damage, this.gameObject, this.gameObject.transform);
    }
}
