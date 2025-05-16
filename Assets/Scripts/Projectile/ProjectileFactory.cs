using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ProjectileFactory : MonoBehaviour
{
    public GameObject arrowPrefab;
    public GameObject tntPrefab;
    public GameObject swordSlashPrefab;

    public ProjectileBase CreateProjectile(ProjectileType type, Vector3 position)
    {
        GameObject projectilePrefab = null;
        switch (type)
        {
            case ProjectileType.Arrow:
                projectilePrefab = arrowPrefab;
                break;
            case ProjectileType.TNT:
                projectilePrefab = tntPrefab;
                break;
            case ProjectileType.SwordSlash:
                projectilePrefab = swordSlashPrefab;
                break;
        }
        if (projectilePrefab != null)
        {
            GameObject projectileObject = Instantiate(projectilePrefab, position, Quaternion.identity);
            ProjectileBase p = projectileObject.GetComponent<ProjectileBase>();
            return p;
        }
        return null;
    }
}
