using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemySpawn : MonoBehaviour
{
    [SerializeField] GameObject[] Prefabs;
    private GameObject[] SpawnPoints;
    private string currentSceneName;

    void Start()
    {
        /*currentSceneName = SceneManager.GetActiveScene().name;

        bool hasEnemy = false;
        foreach (Transform child in GameObject.Find(currentSceneName).transform)
        {
            if (child.GetComponent<Enemy>() != null)
            {
                hasEnemy = true;
                break;
            }
        }

        if (!hasEnemy)
        {
            SpawnPoints = GameObject.FindGameObjectsWithTag("Spawn");
            Spawn();
        }*/
    }
    public void ForceSpawn()
    {
        currentSceneName = SceneManager.GetActiveScene().name;

        bool hasEnemy = false;
        foreach (Transform child in GameObject.Find(currentSceneName).transform)
        {
            if (child.GetComponent<Enemy>() != null)
            {
                hasEnemy = true;
                break;
            }
        }

        if (!hasEnemy)
        {
            SpawnPoints = GameObject.FindGameObjectsWithTag("Spawn");
            Spawn();
        }
    }

    private void Spawn()
    {
        for (int i = 0; i < SpawnPoints.Length; i++)
        {
            int randomIndex = Random.Range(0, Prefabs.Length);
            GameObject e = Instantiate(Prefabs[randomIndex], SpawnPoints[i].transform.position, Quaternion.identity);
            e.transform.SetParent(GameObject.Find(currentSceneName).transform);
            EnemyManager.Instance.RegisterEnemy(e);
        }
    }
}
