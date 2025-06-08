using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransformKingAbi : MonoBehaviour
{
    [SerializeField] private DoorController doorController;

    private void Start()
    {
        LoadComponents();
    }
    private void LoadComponents()
    {
        LoadController();
    }
    private void LoadController()
    {
        if (doorController != null) return;
        doorController = GetComponentInParent<DoorController>();
    }
    private void TransformKing()
    {
        
    }
    // public void GetNewDoor(Transform king)
    // {
    //     this.currentDoor = DoorManager.Instance.GetRandomDoor();
    // }

}
