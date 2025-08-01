﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public ProjectileFactory projectileFactory;
    public int poolSize = 10;

    private Dictionary<ProjectileType, Queue<ProjectileBase>> projectilePools = new();

    private void Awake()
    {
        LoadProjectile();
    }

    private void LoadProjectile()
    {
        projectileFactory = GameObject.FindObjectOfType<ProjectileFactory>();

        foreach (ProjectileType type in System.Enum.GetValues(typeof(ProjectileType)))
        {
            projectilePools[type] = new Queue<ProjectileBase>();
            for (int i = 0; i < poolSize; i++)
            {
                var projectile = projectileFactory.CreateProjectile(type);
                projectile.transform.SetParent(this.transform);
                projectile.SetPool(this);
                projectile.gameObject.SetActive(false);
                projectilePools[type].Enqueue(projectile);
            }
        }
    }

    public void GetProjectile(ProjectileType type, int dmg, Transform origin, Transform dir)
    {
        ProjectileBase projectile = null;

        if (projectilePools[type].Count > 0)
        {
            projectile = projectilePools[type].Dequeue();
            projectile.gameObject.SetActive(true);
        }
        else
        {
            projectile = projectileFactory.CreateProjectile(type);
            projectile.SetPool(this);
        }
        Vector2 direction = projectile.InitVelo(dmg, origin, dir);
        projectile.Rigidbody2D.AddForce(direction);
    }

    public void ReturnProjectile(ProjectileBase projectile)
    {
        projectile.gameObject.SetActive(false);

        if(projectile is ProjArrow)
            projectilePools[ProjectileType.Arrow].Enqueue(projectile);
        else if (projectile is ProjTNT)
            projectilePools[ProjectileType.TNT].Enqueue(projectile);
        else if (projectile is ProjSwordSlash)
            projectilePools[ProjectileType.SwordSlash].Enqueue(projectile);
        else
            Debug.LogError("Unknown projectile type: " + projectile.GetType());
    }
}
