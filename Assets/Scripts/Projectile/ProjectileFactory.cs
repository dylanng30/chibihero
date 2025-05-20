using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ProjectileFactory : MonoBehaviour
{
    public GameObject arrowPrefab;
    public GameObject tntPrefab;
    public GameObject swordSlashPrefab;

    public void CreateProjectile(ProjectileType type, GameObject entity)
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
            Debug.Log("da sinh proj");
            ProjectileBase p = projectileObject.GetComponent<ProjectileBase>();
            Debug.Log(p);
            int dmg = entity.GetComponent<Entity>().GetDamage();
            Debug.Log(dmg);
            Vector2 dir = p.InitVelo(dmg, entity);
            p.GetRb().AddForce(dir);
        }
    }
}
