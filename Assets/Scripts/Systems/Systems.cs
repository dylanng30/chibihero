using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Systems : PersistentSingleton<Systems>
{
    [SerializeField] protected ResourceSystem resourceSystem;
    protected override void Awake()
    {
        base.Awake();
        //Debug.Log("Systems Awake");
        LoadSystem();
    }
    protected virtual void LoadSystem()
    {
        if (resourceSystem != null) return;
        resourceSystem = GetComponentInChildren<ResourceSystem>();
    }

    public ResourceSystem ResourceSystem
    {
        get { return resourceSystem; }
    }
}