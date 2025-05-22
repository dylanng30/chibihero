using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilitySkill : MonoBehaviour
{
    [SerializeField] protected PlayerController playerController;
    [SerializeField] protected GameObject ATKPoint;

    public ProjectileFactory projectileFactory;
    public ProjectileType projectileType = ProjectileType.SwordSlash;
    private Action GetSkillTrigger;
    private bool skillTrigger;

    protected void GetSkill()
    {
        skillTrigger = InputManager.Instance.SkillPressed;
        //Debug.Log("ATK Trigger: " + skillTrigger);
    }

    void Awake()
    {
        LoadComponent();
        GetSkillTrigger += GetSkill;
        projectileFactory = GameObject.FindObjectOfType<ProjectileFactory>().GetComponent<ProjectileFactory>();
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

    public void Skill()
    {
        GetSkillTrigger?.Invoke();
        if (ATKPoint != null)
        {
            int dmg = playerController.PlayerStats.AttackPower;
            projectileFactory.CreateProjectile(projectileType,dmg ,ATKPoint, playerController.transform);
        }
        else
        {
            Debug.LogError("ATKPoint kco");
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
