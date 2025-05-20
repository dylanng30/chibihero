using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyManager : Singleton<EnemyManager>
{
    public List<GameObject> EnemiesTopDown = new List<GameObject>();

    protected override void Awake()
    {
        base.Awake();
    }

    public void ActivatePool()
    {
        foreach(var enemy in EnemiesTopDown)
        {
            if (enemy != null)
                enemy.SetActive(true);
        }
    }

    public void DeactivatePool()
    {
        foreach (var enemy in EnemiesTopDown)
        {
            if (enemy != null)
                enemy.SetActive(false);
        }
    }

    public void RegisterEnemy(GameObject enemy)
    {
        if (!EnemiesTopDown.Contains(enemy))
            EnemiesTopDown.Add(enemy);
    }

    public void UnregisterEnemy(GameObject enemy)
    {
        if (EnemiesTopDown.Contains(enemy))
        {
            EnemiesTopDown.Remove(enemy);
            Destroy(enemy);
        }
    }
}