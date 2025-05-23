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

    //Player detection
    public bool DetectPlayerInAir()
    {
        Vector2 direction = new Vector2(pirateController.transform.localScale.x, 1);
        hit = Physics2D.Raycast(pirateController.transform.position, direction, 1f, LayerMask.GetMask("Player"));

        if (hit.collider == null)
            return false;

        if (hit.collider.CompareTag("Player"))
            return true;

        return false;
    }

    public bool DetectPlayerUnder()
    {
        Vector2 direction = new Vector2(pirateController.transform.localScale.x, -1);
        hit = Physics2D.Raycast(pirateController.transform.position, direction, 1f, LayerMask.GetMask("Player"));

        if (hit.collider == null)
            return false;

        if (hit.collider.CompareTag("Player"))
            return true;

        return false;
    }

    //Ground detection
    public bool DetectObstacle()
    {
        Vector2 direction = transform.right * Mathf.Sign(pirateController.transform.localScale.x);
        hit = Physics2D.Raycast(pirateController.transform.position, direction, 2f, LayerMask.GetMask("Ground"));

        if (hit.collider == null)
            return false;

        if (hit.collider.CompareTag("Ground"))
            return true;

        return false;
    }    

    public bool NextToWall()
    {
        Vector2 direction = transform.right * Mathf.Sign(transform.localScale.x);
        hit = Physics2D.Raycast(pirateController.transform.position, direction, 0.5f, LayerMask.GetMask("Ground"));

        if (hit.collider == null)
            return false;

        if (hit.collider.CompareTag("Ground"))
            return true;

        return false;
    }
}
