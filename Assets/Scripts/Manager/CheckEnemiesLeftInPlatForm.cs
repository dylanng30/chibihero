using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckEnemiesLeftInPlatForm : StaticInstance<CheckEnemiesLeftInPlatForm>
{
    public List<GameObject> enemiesInPlatform = new List<GameObject>();
    private bool hasCompletedMap = false; // Flag to prevent multiple calls
    
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
    
    // Reset flag when entering new platform scene
    private void OnEnable()
    {
        hasCompletedMap = false;
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
        if(enemiesInPlatform.Count == 0 && !hasCompletedMap)
        {
            hasCompletedMap = true; // Set flag to prevent multiple calls
            //Debug.Log("All enemies defeated - calling CompleteMap(true)");
            GameManager.Instance.CompleteMap(true);
        }
    }
}
