using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorManager : Singleton<DoorManager>
{
    [SerializeField] private List<DoorController> doors = new List<DoorController>();
    [SerializeField] private KingController kingController;

    [SerializeField] private bool isLocked, kingReady;

    protected override void Awake()
    {
        base.Awake();
    }
    private void Start()
    {
        LoadKing();
        LoadDoors();
    }
    private void LoadKing()
    {
        if (kingController != null) return;
        kingController = FindObjectOfType<KingController>();
    }
    private void LoadDoors()
    {
        doors.Clear();
        DoorController[] doorObjects = FindObjectsOfType<DoorController>();
        foreach (DoorController doorObject in doorObjects)
        {
            if (doorObject.transform != null)
            {
                doors.Add(doorObject);
            }
        }
    }

    public DoorController FindNearestDoor(Transform king)
    {
        float closestDistance = Mathf.Infinity;
        DoorController nearestDoor = null;

        foreach (DoorController door in doors)
        {
            float distance = Vector2.Distance(king.position, door.transform.position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                nearestDoor = door;
            }
        }
        

        return nearestDoor;
    }
    public DoorController GetRandomDoor(DoorController previousDoor)
    {
        if (doors.Count == 0)
            return null;

        int randomIndex;
        do
        {
            randomIndex = Random.Range(0, doors.Count);
        }
        while (doors[randomIndex] == previousDoor);

        return doors[randomIndex];
    }
    public List<DoorController> Doors
    {
        get
        {
            return doors;
        }
    }

    public void KingIsReadyToTransform(bool kingReady)
    {
        this.kingReady = kingReady;
        //Debug.Log("king is ready:" + kingReady);
    }
    public void LockDoor(bool isLocked)
    {
        this.isLocked = isLocked;
        //Debug.Log("Door is locked:" + isLocked);
    }
    public bool IsLocked
    {
        get
        {
            return isLocked;
        }
    }
    public bool KingReady
    {
        get
        {
            return kingReady;
        }
    }

}
