using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class KingAI : MonoBehaviour
{
    [SerializeField] private KingController kingController;


    protected virtual void Awake()
    {
        LoadComponent();

    }
    protected virtual void LoadComponent()
    {
        LoadController();
    }
    protected virtual void LoadController()
    {
        if (kingController != null) return;
        kingController = GetComponentInParent<KingController>();
    }

    public bool CanOpenTheDoor(Transform door)
    {
        float xDistance = Mathf.Abs(kingController.transform.position.x - transform.position.x);
        float allowedDistance = 0.1f;

        return xDistance <= allowedDistance && kingController.CollisionKing.IsGrounded();
    }

}
