using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ProjectileFactory : MonoBehaviour
{
    public GameObject arrowPrefab;
    public GameObject tntPrefab;
    public GameObject swordSlashPrefab;

    public void CreateProjectile(ProjectileType type, GameObject entity, Transform dir)
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
            GameObject projectileObject = Instantiate(projectilePrefab, entity.transform.position, Quaternion.identity);
            ProjectileBase p = projectileObject.GetComponent<ProjectileBase>();
            int dmg = entity.GetComponent<Entity>().GetDamage();
            Vector2 direction = p.InitVelo(dmg, entity, dir);
            p.GetRb().AddForce(direction);
        }
    }
}
