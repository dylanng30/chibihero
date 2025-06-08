using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckEnemiesLeftInPlatForm : StaticInstance<CheckEnemiesLeftInPlatForm>
{
    public List<GameObject> enemiesInPlatform = new List<GameObject>();
    protected override void Awake()
    {
        base.Awake();
    }
    private void Update()
    {
        EnemiesToList();
        CheckAnyEnemiesLeft();
        enemiesInPlatform.Clear();
    }
    private void EnemiesToList()
    {
        foreach (Transform child in this.transform)
        {
            if(!enemiesInPlatform.Contains(child.gameObject))
                enemiesInPlatform.Add(child.gameObject);
        }
    }
    private void CheckAnyEnemiesLeft()
    {
        if(enemiesInPlatform.Count == 0)
            GameManager.Instance.CompleteMap(true);
    }
}
