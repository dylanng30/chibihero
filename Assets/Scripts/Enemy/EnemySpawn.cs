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
        LoadKnightEnemySpawnPoint();
        LoadPirateSpawnPoint();
    }
    private IEnumerator LowEnemySpawn()
    {
        yield return new WaitUntil(() => Systems.Instance.ResourceSystem.Mines != null);
        //Debug.Log(Systems.Instance.ResourceSystem.Mines.Count);
        List<MineSO> mineSOs = Systems.Instance.ResourceSystem.Mines;
        SpawnAtPositions<MineSO>(mineSOs, LowEnemyPrefabs);
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
            else if (item is GameObject go)
            {
                position = go.transform.position;
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
