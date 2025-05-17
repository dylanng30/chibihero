using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(StateManager))]
public class RedTNT : LowEnemy
{
    private ProjectileFactory projectileFactory;
    public ProjectileType projectileType = ProjectileType.TNT;

    private void Awake()
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
            _currentHealth = _maxHealth;
        }
    }

    public override void NormalATK()
    {
        projectileFactory.CreateProjectile(projectileType, this.gameObject);
        /*GameObject bullet = Instantiate(prefab, this.transform.position, Quaternion.identity);
        Vector2 Force = bullet.GetComponent<ProjectileBase>().InitVelo(_Damage,thi);
        bullet.GetComponent<Rigidbody2D>().AddForce(Force);*/
    }
}
