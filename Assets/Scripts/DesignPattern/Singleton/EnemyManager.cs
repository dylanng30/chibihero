using System.Collections.Generic;
using System.Linq;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;
using static Unity.VisualScripting.Metadata;

public class EnemyManager : Singleton<EnemyManager>
{
    //public List<GameObject> enemies = new List<GameObject>();
    public Dictionary<string, List<GameObject>> sceneEnemies = new Dictionary<string, List<GameObject>>();
    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(gameObject);
    }    

    public void RegisterEnemy(GameObject enemy)
    {
        /*if (!enemies.Contains(enemy))
            enemies.Add(enemy);*/

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

    public bool CheckEnemiesAndReturnToTopDown()
    {
        List<GameObject> enemiesInScene = new List<GameObject>();

        string currentSceneName = SceneManager.GetActiveScene().name;
        for (int i = 0; i < GameObject.Find(currentSceneName).transform.childCount; i++)
        {
            GameObject child = GameObject.Find(currentSceneName).transform.GetChild(i).gameObject;
            enemiesInScene.Add(child);
            Debug.Log("Found child: " + child.name);
        }
        bool anyActive = enemiesInScene.Any<GameObject>(e => e.gameObject.activeInHierarchy);
        Debug.Log(anyActive);
        return anyActive;
    }

    public int GetAliveEnemyCount()
    {
        return sceneEnemies.Count;
    }

}
