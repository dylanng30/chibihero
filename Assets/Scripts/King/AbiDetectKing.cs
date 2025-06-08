using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbiDetectKing : MonoBehaviour
{
    [SerializeField] protected KingController kingController;
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
        if (this.kingController != null)
            return;
        this.kingController = GetComponentInParent<KingController>();
    }

    //Ground detection
    public bool DetectObstacle()
    {
        Vector2 direction = transform.right * Mathf.Sign(kingController.transform.localScale.x);
        hit = Physics2D.Raycast(kingController.transform.position, direction, 2f, LayerMask.GetMask("Ground"));

        if (hit.collider == null)
            return false;

        if (hit.collider.CompareTag("Ground"))
            return true;

        return false;
    }

    public bool NextToWall()
    {
        Vector2 direction = Vector2.right * kingController.transform.localScale.x;
        hit = Physics2D.Raycast(kingController.transform.position, direction, 0.5f, LayerMask.GetMask("Ground"));

        if (hit.collider == null)
            return false;

        if (hit.collider.CompareTag("Ground"))
        {
            Debug.Log("Hit: " + hit.collider.name);
            Debug.DrawRay(kingController.transform.position, direction, Color.yellow, 0.5f);
            return true;
        }


        return false;
    }
}
