using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemySpawn : MonoBehaviour
{
    [SerializeField] GameObject[] LowEnemyPrefabs;
    [SerializeField] GameObject[] KnightPrefabs;
    [SerializeField] GameObject[] PiratePrefabs;

    private Transform root;

    private void Start()
    {
        root = GameObject.Find("EnemyManager").transform;
        if (root == null) return;
        if (root.childCount > 0) return;
        LoadLowEnemySpawnPoint();
        LoadKnightEnemySpawnPoint();
        LoadPirateSpawnPoint();
    }
    private void LoadLowEnemySpawnPoint()
    {
        GameObject[] LowEnemySpawnPoints = GameObject.FindGameObjectsWithTag("Spawn");
        if (LowEnemySpawnPoints.Length == 0)
        {
            Debug.Log("LowEnemy spawn points are empty !!!");
            return;
        }
        Spawn(LowEnemySpawnPoints, LowEnemyPrefabs);
    }
    private void LoadKnightEnemySpawnPoint()
    {
        GameObject[] KnightSpawnPoints = GameObject.FindGameObjectsWithTag("RedKnight");
        if(KnightSpawnPoints.Length == 0)
        {
            Debug.Log("Knight spawn points are empty !!!");
            return;
        }
        Spawn(KnightSpawnPoints, KnightPrefabs);
    }
    private void LoadPirateSpawnPoint()
    {
        GameObject[] PirateSpawnPoints = GameObject.FindGameObjectsWithTag("Pirate");
        if (PirateSpawnPoints.Length == 0)
        {
            Debug.Log("Pirate spawn points are empty !!!");
            return;
        }
            
        Spawn(PirateSpawnPoints, PiratePrefabs);
    }

    public void Spawn(GameObject[] spawnPoints, GameObject[] enemies)
    {     

        for (int i = 0; i < spawnPoints.Length; i++)
        {
            int randomIndex = Random.Range(0, enemies.Length);
            GameObject e = Instantiate(enemies[randomIndex], spawnPoints[i].transform.position, Quaternion.identity);
            e.transform.SetParent(root);
            EnemyManager.Instance.RegisterEnemy(e);
        }
    }
}
