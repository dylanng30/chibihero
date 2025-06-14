using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyManager : Singleton<EnemyManager>, IObserver
{
    public List<GameObject> EnemiesTopDown = new List<GameObject>();

    protected override void Awake()
    {
        base.Awake();
        ObserverManager.Instance.RegisterObserver(this);
    }

    public void ActivatePool()
    {
        Debug.Log("Activate Pool");
        foreach(var enemy in EnemiesTopDown)
        {
            if (enemy != null)
                enemy.SetActive(true);
        }
    }


    public void DeactivatePool()
    {
        Debug.Log("Deactivate Pool");
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

    public void KingIsDead()
    {
        foreach(GameObject enemy in EnemiesTopDown)
        {
            Destroy(enemy);
        }
    }

    public void ChangeMap(GameState state)
    {
        if (state == GameState.Exploring)
            ActivatePool();
        else
            DeactivatePool();
    }
}