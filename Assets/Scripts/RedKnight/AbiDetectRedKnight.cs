using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbiDetectRedKnight : MonoBehaviour
{
    [SerializeField] protected RedKnightController redKnightController;

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
        if (this.redKnightController != null)
            return;
        this.redKnightController = GetComponentInParent<RedKnightController>();
    }

    public bool DetectObstacle()
    {
        Vector2 direction = transform.right * Mathf.Sign(redKnightController.transform.localScale.x);
        hit = Physics2D.Raycast(redKnightController.transform.position, direction, 2f, LayerMask.GetMask("Ground"));

        if (hit.collider == null)
            return false;

        if (hit.collider.CompareTag("Ground"))
            return true;

        return false;
    }

    public bool NextToWall()
    {
        Vector2 direction = transform.right * Mathf.Sign(transform.localScale.x);
        hit = Physics2D.Raycast(redKnightController.transform.position, direction, 0.5f, LayerMask.GetMask("Ground"));

        if (hit.collider == null)
            return false;

        if (hit.collider.CompareTag("Ground"))
            return true;

        return false;
    }
}
