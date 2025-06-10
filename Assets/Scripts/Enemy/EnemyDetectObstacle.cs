using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDetectObstacle : MonoBehaviour
{
    [SerializeField] protected LowEnemyController lowEnemyController;

    protected RaycastHit2D hit;

    private void Start()
    {
        LoadComponent();
    }
    protected void LoadComponent()
    {
        LoadController();
    }
    protected virtual void LoadController()
    {
        if (this.lowEnemyController != null)
            return;
        this.lowEnemyController = GetComponentInParent<LowEnemyController>();
    }

    public bool DetectObstacle()
    {
        Vector2 direction = transform.right * Mathf.Sign(lowEnemyController.transform.localScale.x);
        hit = Physics2D.Raycast(lowEnemyController.transform.position, direction, 2f, LayerMask.GetMask("Ground"));

        if (hit.collider == null)
            return false;

        if (hit.collider.CompareTag("Ground"))
        {
            //Debug.Log("Obstacle detected: " + hit.collider.name);
            return true;
        }            
        return false;
    }

    public bool NextToWall()
    {
        Vector2 direction = transform.right * Mathf.Sign(transform.localScale.x);
        hit = Physics2D.Raycast(lowEnemyController.transform.position, direction, 0.5f, LayerMask.GetMask("Ground"));

        if (hit.collider == null)
            return false;

        if (hit.collider.CompareTag("Ground"))
        {
            //Debug.Log("Obstacle detected: " + hit.collider.name);
            return true;
        }     
        return false;
    }
}
