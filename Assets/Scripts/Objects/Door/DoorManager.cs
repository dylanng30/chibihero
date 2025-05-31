using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorManager : Singleton<DoorManager>
{
    [SerializeField] private List<Transform> doors = new List<Transform>();

    protected override void Awake()
    {
        base.Awake();
    }
    private void Start()
    {
        LoadDoors();
    }
    private void LoadDoors()
    {
        doors.Clear();
        var doorObjects = GameObject.FindGameObjectsWithTag("Door");
        foreach (var doorObject in doorObjects)
        {
            if (doorObject.transform != null)
            {
                doors.Add(doorObject.transform);
            }
        }
    }
    public Transform FindNearestDoor(Transform king)
    {
        float dis = Mathf.Infinity;
        foreach(Transform door in doors)
        {
            if (Vector2.Distance(king.position, door.position) < dis)
            {
                dis = Vector2.Distance(king.position, door.position);
                return door;
            }
            else
                return null;                
        }
        return null;
    }
    public Transform GetRandomDoor()
    {
        if (doors.Count == 0)
            return null;
        int randomIndex = Random.Range(0, doors.Count - 1);
        return doors[randomIndex];
    }
    public List<Transform> Doors
    {
        get
        {
            return doors;
        }
    }

}
