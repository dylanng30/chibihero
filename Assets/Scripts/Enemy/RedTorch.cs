using UnityEngine;

[RequireComponent(typeof(StateManager))]
public class RedTorch : LowEnemy
{
    private void Awake()
    {
        this.InitTarget();
        this.SetComponents();
        this.InitState();
    }

    void Update()
    {
        this.FlipToPlayer();
    }

    public override void NormalATK()
    {
        Collider2D[] hitPlayers = Physics2D.OverlapCircleAll(this.transform.position, ATKRange, targetLayer);
        foreach (Collider2D player in hitPlayers)
        {
            PlayerController p = player.GetComponentInParent<PlayerController>();
            p.DamageManager.TakeDamage(_Damage, this.gameObject);
        }
    }

}
