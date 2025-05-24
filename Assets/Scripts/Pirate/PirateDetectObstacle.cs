using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class PirateDetectObstacle : MonoBehaviour
{
    [SerializeField] protected PirateController pirateController;
    private RaycastHit2D hit;
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
        if (this.pirateController != null)
            return;
        this.pirateController = GetComponentInParent<PirateController>();
    }

    //Ground detection
    public bool DetectObstacle()
    {
        Vector2 direction = transform.right * Mathf.Sign(pirateController.transform.localScale.x);
        hit = Physics2D.Raycast(pirateController.transform.position, direction, 2f, LayerMask.GetMask("Ground"));

        if (hit.collider == null)
            return false;

        if (hit.collider.CompareTag("Ground"))
        {
            Debug.Log("Hit: " + hit.collider.name);
            Debug.DrawRay(pirateController.transform.position, direction, Color.red, 2f);
            return true;
        }
        

        return false;
    }    

    public bool NextToWall()
    {
        Vector2 direction = transform.right * Mathf.Sign(transform.localScale.x);
        hit = Physics2D.Raycast(pirateController.transform.position, direction, 0.5f, LayerMask.GetMask("Ground"));

        if (hit.collider == null)
            return false;

        if (hit.collider.CompareTag("Ground"))
        {
            Debug.Log("Hit: " + hit.collider.name);
            Debug.DrawRay(pirateController.transform.position, direction, Color.yellow, 2f);
            return true;
        }
        

        return false;
    }
}
