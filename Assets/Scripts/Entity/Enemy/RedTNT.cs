using UnityEngine;

[RequireComponent(typeof(StateManager))]
public class RedTNT : LowEnemy
{    
    [SerializeField]
    GameObject prefab;
    private float Angle;

    private void Awake()
    {
        Angle = 50;
        this.InitTarget();
        this.SetComponents();
        this.InitState();
    }

    void Update()
    {
        this.FlipToPlayer();
        if (_currentHealth <= 0)
        {
            this.gameObject.SetActive(false);
        }
    }

    public override void NormalATK()
    {
        GameObject bullet = Instantiate(prefab, this.transform.position, Quaternion.identity);
        Vector2 Force = bullet.GetComponent<ProjectileBase>().InitVelo(_Damage ,Angle, this._currentTarget.transform, this.transform);
        bullet.GetComponent<Rigidbody2D>().AddForce(Force);
    }
}
