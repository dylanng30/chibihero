using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckEnemiesLeftInPlatForm : StaticInstance<CheckEnemiesLeftInPlatForm>
{
    List<GameObject> enemiesInPlatform = new List<GameObject>();
    protected override void Awake()
    {
        base.Awake();
    }
    void Start()
    {
        Transform[] e = GetComponentsInChildren<Transform>();
        foreach (Transform child in e)
        {
            enemiesInPlatform.Add((child.gameObject));
        }
    }

    public void EliminateEnemy(GameObject enemy)
    {
        if (enemiesInPlatform.Contains(enemy))
        {
            enemiesInPlatform.Remove(enemy);
            Destroy(enemy);
        }
        CheckAnyEnemiesLeft();
    }
    private void CheckAnyEnemiesLeft()
    {
        if(enemiesInPlatform.Count == 0)
            GameManagerTest.Instance.CompleteMap(true);
    }
}
