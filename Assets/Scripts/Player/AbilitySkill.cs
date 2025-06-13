using System;
using System.Collections;
using UnityEngine;

public class AbilitySkill : MonoBehaviour
{
    [SerializeField] protected PlayerController playerController;
    [SerializeField] protected Transform ATKPoint;

    [SerializeField] protected ProjectileType projectileType;
    private Action GetSkillTrigger;

    private bool skillTrigger;

    [SerializeField] private ObjectPool pool;

    protected void GetSkill()
    {
        skillTrigger = InputManager.Instance.SkillPressed;
        //Debug.Log("ATK Trigger: " + skillTrigger);
    }

    private void Start()
    {
        LoadComponent();
        GetSkillTrigger += GetSkill;
    }
    void Update()
    {
        GetSkillTrigger?.Invoke();
    }

    protected void LoadComponent()
    {
        LoadPlayerController();
    }
    protected void LoadPlayerController()
    {
        if (this.playerController != null)
            return;
        this.playerController = transform.GetComponentInParent<PlayerController>();
    }
    protected void LoadPool()
    {
        if (this.pool != null)
            return;
        this.pool = GameObject.FindObjectOfType<ObjectPool>().GetComponent<ObjectPool>();
    }

    public void Skill()
    {
        GetSkillTrigger?.Invoke();
        LoadPool();
        StartCoroutine(Shooting(projectileType));
    }

    public IEnumerator Shooting(ProjectileType projectileType)
    {
        yield return new WaitUntil(() => playerController != null && playerController.PlayerStats != null);
        yield return new WaitUntil(() => pool != null);

        if(NearestEnemy == null)
        {
            Debug.Log("Khong co enemy");
            yield break;
        }
        
        // Play attack sound - bỏ random
        AudioSystem.Instance.PlayAttackSound(1); // Cố định dùng attack sound 1
            
        pool.GetProjectile(projectileType, playerController.PlayerStats.AttackPower, ATKPoint, this.NearestEnemy);
    }
    public Transform NearestEnemy
    {
        get
        {
            float closestDistance = Mathf.Infinity;
            GameObject enemy = null;
            GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
            
            if (enemies.Length <= 0)
                return null;

            foreach (GameObject e in enemies)
            {
                float distance = Vector2.Distance(playerController.transform.position, e.transform.position);
                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    enemy = e;
                }
            }

            return enemy.transform;
        }        
    }

    public bool SkillTrigger
    {
        get
        {
            return skillTrigger;
        }
    }
}
