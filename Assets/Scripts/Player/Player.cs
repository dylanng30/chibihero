using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : PersistentSingleton<Player>
{
    protected override void Awake()
    {
        base.Awake();
    }

    public void Spawn()
    {
        this.transform.position = GameObject.Find("PlayerSpawn").transform.position;
    }
}
