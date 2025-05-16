using UnityEngine;

[System.Serializable]
public class EnemyData
{
    public string enemyID;
    public GameObject prefab;
    public Vector2 spawnPosition;
    public float maxHealth = 100f;
}