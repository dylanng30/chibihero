using UnityEngine;

[RequireComponent(typeof(StateManager))]
public class RedKnight : LowEnemy
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
            PlayerController p = player.GetComponent<PlayerController>();
            p?.TakeDamage(_Damage, this.transform);
        }
    }

}
