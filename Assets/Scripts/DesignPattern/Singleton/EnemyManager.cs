using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyManager : Singleton<EnemyManager>
{
    public GameObject mainTDPool, redArcherPool, redPawnPool;
    public List<GameObject> ActiveEnemies = new List<GameObject>();
    public List<GameObject> InactiveEnemies = new List<GameObject>();
    private bool isCompleted = false;
    //public Dictionary<string, List<GameObject>> sceneEnemies = new Dictionary<string, List<GameObject>>();
    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(gameObject);
    }

    public void ActivatePool()
    {
        Debug.Log("Activate enemy pool");
        isCompleted = false; // Reset trạng thái hoàn thành map

        string sceneName = SceneManager.GetActiveScene().name;
        Transform pool = GameObject.Find(sceneName)?.transform;
        if (pool == null) return;
        List<GameObject> enemiesActive = new List<GameObject>(ActiveEnemies);
        foreach (GameObject e in enemiesActive)
        {
            if (e != null && (e.transform.parent == null || e.transform.parent.name != sceneName))
                UnregisterEnemy(e, false); // Không kiểm tra hoàn thành khi chuyển scene
        }

        // Active lại các enemy thuộc scene hiện tại
        List<GameObject> enemiesInactive = new List<GameObject>(InactiveEnemies);
        foreach (GameObject e in enemiesInactive)
        {
            if (e != null && e.transform.parent != null && e.transform.parent.name == sceneName)
                RegisterEnemy(e);
        }        
    }
    public void DeactivatePool()
    {
        Debug.Log("Deactivate enemy pool");
        isCompleted = false; // Reset trạng thái hoàn thành map
        string sceneName = SceneManager.GetActiveScene().name;
        Transform pool = GameObject.Find(sceneName)?.transform;
        if (pool == null) return;
        // Deactive các enemy thuộc scene hiện tại
        List<GameObject> enemiesActive = new List<GameObject>(ActiveEnemies);
        foreach (GameObject e in enemiesActive)
        {
            if (e != null && e.transform.parent != null && e.transform.parent.name == sceneName)
                UnregisterEnemy(e, false); // Không kiểm tra hoàn thành khi chuyển scene
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

    private void CheckAnyActiveEnemyLeft()
    {
        string sceneName = SceneManager.GetActiveScene().name;
        if (sceneName == "MainTopDown") return; // Không kiểm tra ở MainTopDown

        if (ActiveEnemies.Count == 0)
        {
            isCompleted = true;
            GameManager.Instance.LoadMap("MainTopDown");
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
