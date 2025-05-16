using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyManager : Singleton<EnemyManager>
{
    public GameObject mainTDPool, redArcherPool, redPawnPool;
    public List<GameObject> ActiveEnemies = new List<GameObject>();
    public List<GameObject> InactiveEnemies = new List<GameObject>();
    private bool isCompleted = false;
    private bool justEnteredScene = false;
    private float entryTimer = 0f;
    
    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(gameObject);
    }
    
    private void Update()
    {
        // Give player time to enter scene before checking completion
        if (justEnteredScene)
        {
            entryTimer += Time.deltaTime;
            if (entryTimer >= 1f)
            {
                justEnteredScene = false;
                entryTimer = 0f;
            }
        }
    }

    public void ActivatePool()
    {
        Debug.Log("Activate enemy pool");
        isCompleted = false; // Reset completion state
        justEnteredScene = true; // Mark that we just entered a scene
        entryTimer = 0f;

        string sceneName = SceneManager.GetActiveScene().name;
        Transform pool = GameObject.Find(sceneName)?.transform;
        if (pool == null) return;
        
        // Deactivate enemies from other scenes
        List<GameObject> enemiesActive = new List<GameObject>(ActiveEnemies);
        foreach (GameObject e in enemiesActive)
        {
            if (e != null && (e.transform.parent == null || e.transform.parent.name != sceneName))
                UnregisterEnemy(e, false);
        }

        // Reactivate enemies for this scene - including respawning defeated enemies
        ReactivateEnemiesForCurrentScene(sceneName);
    }
    
    private void ReactivateEnemiesForCurrentScene(string sceneName)
    {
        Transform pool = GameObject.Find(sceneName)?.transform;
        if (pool == null) return;
        
        // Reactivate all enemies belonging to current scene whether they're in active or inactive list
        List<GameObject> enemiesInactive = new List<GameObject>(InactiveEnemies);
        foreach (GameObject e in enemiesInactive)
        {
            if (e != null && e.transform.parent != null && e.transform.parent.name == sceneName)
            {
                // Reset enemy health if needed
                LowEnemy enemy = e.GetComponent<LowEnemy>();
                if (enemy != null)
                {
                    enemy.ResetHealth(); // Add this method to LowEnemy class
                }
                
                RegisterEnemy(e);
            }
        }
    }

    public void DeactivatePool()
    {
        // Same as before
        Debug.Log("Deactivate enemy pool");
        isCompleted = false;
        string sceneName = SceneManager.GetActiveScene().name;
        Transform pool = GameObject.Find(sceneName)?.transform;
        if (pool == null) return;
        
        List<GameObject> enemiesActive = new List<GameObject>(ActiveEnemies);
        foreach (GameObject e in enemiesActive)
        {
            if (e != null && e.transform.parent != null && e.transform.parent.name == sceneName)
                UnregisterEnemy(e, false);
        }
    }

    private void CheckAnyActiveEnemyLeft()
    {
        // Don't check for completion if we just entered the scene
        if (justEnteredScene) return;
        
        string sceneName = SceneManager.GetActiveScene().name;
        if (sceneName == "MainTopDown") return; // Skip check for MainTopDown
        
        bool hasEnemiesForThisScene = false;
        
        // Count enemies in current scene
        foreach (GameObject e in ActiveEnemies)
        {
            if (e != null && e.transform.parent != null && e.transform.parent.name == sceneName)
            {
                hasEnemiesForThisScene = true;
                break;
            }
        }
        
        if (!hasEnemiesForThisScene)
        {
            isCompleted = true;
            GameManager.Instance.LoadMap("MainTopDown");
        }
    }

    public void RegisterEnemy(GameObject enemy)
    {
        if (!ActiveEnemies.Contains(enemy))
        {
            enemy.SetActive(true);
            ActiveEnemies.Add(enemy);
            if (InactiveEnemies.Contains(enemy))
                InactiveEnemies.Remove(enemy);
        }
    }

    public void UnregisterEnemy(GameObject enemy, bool checkComplete = true)
    {
        if (ActiveEnemies.Contains(enemy))
        {
            ActiveEnemies.Remove(enemy);
            enemy.SetActive(false);
            InactiveEnemies.Add(enemy);
            if (checkComplete)
                CheckAnyActiveEnemyLeft();
        }
    }

    

    public void ResetEnemies()
    {
        // Xóa toàn bộ enemy khỏi pool khi load lại map
        foreach (var e in ActiveEnemies)
            if (e != null) Destroy(e);
        foreach (var e in InactiveEnemies)
            if (e != null) Destroy(e);
        ActiveEnemies.Clear();
        InactiveEnemies.Clear();
    }

    public bool AllEnemiesActive()
    {
        return InactiveEnemies.Count == 0;
    }

    public bool GetState()
    {
        return isCompleted;
    }

    public int GetActiveEnemyCount()
    {
        return ActiveEnemies.Count;
    }
    public int GetDeactiveEnemyCount()
    {
        return InactiveEnemies.Count;
    }

}
