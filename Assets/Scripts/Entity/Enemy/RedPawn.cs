using UnityEngine;
[RequireComponent(typeof(StateManager))]
public class RedPawn : LowEnemy
{    private void Start()
    {        
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
            EnemyManager.Instance.UnregisterEnemy(this.gameObject);
        }
            
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
