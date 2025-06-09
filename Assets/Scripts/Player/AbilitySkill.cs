using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilitySkill : MonoBehaviour
{
    [SerializeField] protected PlayerController playerController;
    [SerializeField] protected GameObject ATKPoint;

    public ProjectileType projectileType = ProjectileType.SwordSlash;
    private Action GetSkillTrigger;
    private bool skillTrigger;

    private ObjectPool pool;



    protected void GetSkill()
    {
        skillTrigger = InputManager.Instance.SkillPressed;
        //Debug.Log("ATK Trigger: " + skillTrigger);
    }

    void Awake()
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
        LoadPool();
    }
    protected void LoadPlayerController()
    {
        if (this.playerController != null)
            return;
        this.playerController = transform.GetComponentInParent<PlayerController>();
    }
    protected virtual void LoadPool()
    {
        if (this.pool != null)
            return;
        ObjectPool objPool = GameObject.FindObjectOfType<ObjectPool>();
        this.pool = objPool.GetComponent<ObjectPool>();
    }


    public void Skill()
    {
        GetSkillTrigger?.Invoke();
        StartCoroutine(Shooting(projectileType));
    }

    public IEnumerator Shooting(ProjectileType projectileType)
    {
        yield return new WaitUntil(() => playerController != null && playerController.PlayerStats != null);
        pool.GetProjectile(projectileType, playerController.PlayerStats.AttackPower, playerController.transform, ATKPoint.transform);
    }

    public bool SkillTrigger
    {
        get
        {
            return skillTrigger;
        }
    }
}
