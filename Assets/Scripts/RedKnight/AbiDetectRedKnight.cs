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

    public bool DetectGround()
    {
        Vector2 direction = transform.right * redKnightController.transform.localScale.x;
        hit = Physics2D.Raycast(redKnightController.transform.position, direction, 2f, LayerMask.GetMask("Ground"));

        if (hit.collider == null)
            return false;

        if (hit.collider.CompareTag("Ground"))
        {
            Debug.Log("Detect ground: ");
            return true;
        }

        return false;
    }

    public bool NextToGround()
    {
        Vector2 direction = transform.right * transform.localScale.x;
        hit = Physics2D.Raycast(redKnightController.transform.position, direction, 1f, LayerMask.GetMask("Ground"));

        if (hit.collider == null)
            return false;

        if (hit.collider.CompareTag("Ground"))
        {
            Debug.Log("Next to ground");
            return true;
        }
            

        return false;
    }
}
