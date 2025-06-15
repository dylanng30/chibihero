using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckEnemiesLeftInPlatForm : StaticInstance<CheckEnemiesLeftInPlatForm>
{
    public List<GameObject> enemiesInPlatform = new List<GameObject>();
    private bool missionFailed;
    protected override void Awake()
    {
        base.Awake();
        missionFailed = true;
    }
    private void Update()
    {
        EnemiesToList();
        Debug.Log(missionFailed);
        if(missionFailed)
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
        //Debug.Log(enemiesInPlatform.Count);
    }
    private void CheckAnyEnemiesLeft()
    {
        if(enemiesInPlatform.Count == 0)
        {
            GameManager.Instance.CompleteMap(true);
            missionFailed = false;
        }                
    }
}
