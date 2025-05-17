
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
        Debug.Log(projectileFactory);
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
        Debug.Log(projectileType);
        projectileFactory.CreateProjectile(projectileType, this.gameObject);
        /*GameObject bullet = Instantiate(prefab, this.transform.position, Quaternion.identity);
        Vector2 Force = bullet.GetComponent<ProjectileBase>().InitVelo(_Damage, this._currentTarget.transform, this.transform);
        bullet.GetComponent<Rigidbody2D>().AddForce(Force);*/
    }
}
