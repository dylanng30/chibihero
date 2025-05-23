using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class DetectObstacle : MonoBehaviour
{
    /*public bool DetectObstacle()
    {
        Vector2 direction = transform.right * Mathf.Sign(lowEnemyController.transform.localScale.x);
        hit = Physics2D.Raycast(lowEnemyController.transform.position, direction, 2f, LayerMask.GetMask("Ground"));

        if (hit.collider == null)
            return false;

        if (hit.collider.CompareTag("Ground"))
            return true;

        return false;
    }

    public bool NextToWall()
    {
        Vector2 direction = transform.right * Mathf.Sign(transform.localScale.x);
        hit = Physics2D.Raycast(lowEnemyController.transform.position, direction, 0.5f, LayerMask.GetMask("Ground"));

        if (hit.collider == null)
            return false;

        if (hit.collider.CompareTag("Ground"))
            return true;

        return false;
    }*/
}
