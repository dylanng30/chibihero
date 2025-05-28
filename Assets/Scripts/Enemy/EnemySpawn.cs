using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemySpawn : MonoBehaviour
{
    [SerializeField] GameObject[] Prefabs;

    private void Start()
    {
        LoadLowEnemySpawnPoint();
        LoadKnightEnemySpawnPoint();
    }
    private void LoadLowEnemySpawnPoint()
    {
        GameObject[] LowSpawnPoints = GameObject.FindGameObjectsWithTag("Spawn");
        if (LowSpawnPoints.Length == 0)
            return;
        Spawn(LowSpawnPoints);
    }
    private void LoadKnightEnemySpawnPoint()
    {
        GameObject[] KnightSpawnPoints = GameObject.FindGameObjectsWithTag("RedKnight");
        if(KnightSpawnPoints.Length == 0)
            return;
        Spawn(KnightSpawnPoints);
    }

    public void Spawn(GameObject[] spawnPoints)
    {
        Transform root = GameObject.Find("EnemyManager").transform;
        if (root == null) return;
        if (root.childCount > 0) return;

        for (int i = 0; i < spawnPoints.Length; i++)
        {
            int randomIndex = Random.Range(0, Prefabs.Length);
            GameObject e = Instantiate(Prefabs[randomIndex], spawnPoints[i].transform.position, Quaternion.identity);
            e.transform.SetParent(root);
            EnemyManager.Instance.RegisterEnemy(e);
        }

    }
}
