using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

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
        StartCoroutine(LowEnemySpawn());
        StartCoroutine(RedKnightSpawn());
        StartCoroutine(PirateSpawn());
    }
    private IEnumerator LowEnemySpawn()
    {
        yield return new WaitUntil(() => Systems.Instance.ResourceSystem.Mines != null);
        List<MineSO> mineSOs = Systems.Instance.ResourceSystem.Mines;
        SpawnAtPositions<MineSO>(mineSOs, LowEnemyPrefabs);
    }
    private IEnumerator RedKnightSpawn()
    {
        yield return new WaitUntil(() => Systems.Instance.ResourceSystem.SpawnPoints != null);
        List<SpawnPointsSO> spawnPointsSO = Systems.Instance.ResourceSystem.SpawnPoints;
        List<SpawnPointsSO> redKnightSpawnPoints = new ();
        foreach (SpawnPointsSO point in spawnPointsSO)
        {
            if (point.EnemyType != EnemyType.RedKnight)
                yield break;
            redKnightSpawnPoints.Add(point);
            Debug.Log(redKnightSpawnPoints.Count);
        }
        SpawnAtPositions<SpawnPointsSO>(redKnightSpawnPoints, KnightPrefabs);
    }
    private IEnumerator PirateSpawn()
    {
        yield return new WaitUntil(() => Systems.Instance.ResourceSystem.SpawnPoints != null);
        //Debug.Log(Systems.Instance.ResourceSystem.Mines.Count);
        List<SpawnPointsSO> spawnPointsSO = Systems.Instance.ResourceSystem.SpawnPoints;
        List<SpawnPointsSO> pirateSpawnPoints = new();
        foreach (SpawnPointsSO point in spawnPointsSO)
        {
            if (point.EnemyType != EnemyType.Pirate)
                yield break;
            pirateSpawnPoints.Add(point);
            Debug.Log(pirateSpawnPoints.Count);
        }
        SpawnAtPositions<SpawnPointsSO>(pirateSpawnPoints, PiratePrefabs);
    }
    public void SpawnAtPositions<T>(List<T> positions, GameObject[] prefabs)
    {
        if (positions == null || prefabs == null || prefabs.Length == 0) return;

        foreach (var item in positions)
        {
            Vector3 position = Vector3.zero;

            // Xác định vị trí từ kiểu của item
            if (item is Transform transform)
            {
                position = transform.position;
            }
            else if (item is SpawnPointsSO spawnPoint)
            {
                position = spawnPoint.Position;
            }
            else if (item is MineSO mineSO)
            {
                position = mineSO.Position;
            }
            else
            {
                Debug.LogWarning($"Không thể lấy vị trí từ kiểu {typeof(T)}");
                continue;
            }

            int randomIndex = Random.Range(0, prefabs.Length);
            GameObject enemy = Instantiate(prefabs[randomIndex], position, Quaternion.identity);
            enemy.transform.SetParent(root);
            EnemyManager.Instance.RegisterEnemy(enemy);
        }
    }
}
