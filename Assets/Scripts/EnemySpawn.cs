using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemySpawn : MonoBehaviour
{
    [SerializeField] GameObject[] Prefabs;
    private GameObject[] SpawnPoints;

    public void Spawn()
    {
        string currentSceneName = SceneManager.GetActiveScene().name;
        GameObject sceneRoot = GameObject.Find(currentSceneName);
        if (sceneRoot == null) return;

        // Chỉ spawn nếu chưa có enemy nào là con của sceneRoot
        if (sceneRoot.transform.childCount > 0) return;

        SpawnPoints = GameObject.FindGameObjectsWithTag("Spawn");
        for (int i = 0; i < SpawnPoints.Length; i++)
        {
            int randomIndex = Random.Range(0, Prefabs.Length);
            GameObject e = Instantiate(Prefabs[randomIndex], SpawnPoints[i].transform.position, Quaternion.identity);
            e.transform.SetParent(sceneRoot.transform);
            EnemyManager.Instance.RegisterEnemy(e);
        }
    }
}
