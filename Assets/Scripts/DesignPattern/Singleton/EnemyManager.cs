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

        string sceneName = enemy.scene.name;
        if (!sceneEnemies.ContainsKey(sceneName))
            sceneEnemies[sceneName] = new List<GameObject>();

        if (!sceneEnemies[sceneName].Contains(enemy))
            sceneEnemies[sceneName].Add(enemy);
    }

    public void UnregisterEnemy(GameObject enemy)
    {
        //enemies.Remove(enemy);
        GameManager.Instance.CheckEnemiesLeft();
    }

    public void ActiveEnemy()
    {
        string currentScene = SceneManager.GetActiveScene().name;

        if (sceneEnemies.ContainsKey(currentScene))
        {
            foreach (GameObject e in sceneEnemies[currentScene])
            {
                if (e != null)
                {
                    e.GetComponent<Enemy>().ResetStat();
                    e.SetActive(true);
                }
            }
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
