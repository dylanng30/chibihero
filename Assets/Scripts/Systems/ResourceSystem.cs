using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ResourceSystem : MonoBehaviour
{
    public List<ScriptablePlayer> Players { get; private set; }
    public List<ScriptableEnemy> Enemies { get; private set; }
    public List<MineSO> Mines { get; private set; }
    public List<SpawnPointsSO> SpawnPoints { get; private set; }

    public Dictionary<PlayerType, ScriptablePlayer> PlayerDictionary = new Dictionary<PlayerType, ScriptablePlayer>();
    public Dictionary<EnemyType, ScriptableEnemy> EnemyDictionary = new Dictionary<EnemyType, ScriptableEnemy>();

    private ScriptablePlayer player;
    private ScriptableEnemy enemy;
    private MineSO mine;
    private SpawnPointsSO spawnPoint;
    protected void Awake()
    {
        AssembleResource();
    }

    private void AssembleResource()
    {
        LoadScriptableObject();
        TransformToDictionary();
    }
    private void LoadScriptableObject()
    {
        Players = Resources.LoadAll<ScriptablePlayer>("Players").ToList();
        Enemies = Resources.LoadAll<ScriptableEnemy>("Enemies").ToList();
        Mines = Resources.LoadAll<MineSO>("Mines").ToList();
        SpawnPoints = Resources.LoadAll<SpawnPointsSO>("SpawnPoints").ToList();
    }
    private void TransformToDictionary()
    {
        PlayerDictionary = Players.ToDictionary(player => player.PlayerType, player => player);
        EnemyDictionary = Enemies.ToDictionary(enemy => enemy.EnemyType, enemy => enemy);
    }

    public ScriptablePlayer GetPlayer(PlayerType playerType)
    {
        if (PlayerDictionary.TryGetValue(playerType, out player))
        {
            return player;
        }
        else
        {
            Debug.LogError($"Player type {playerType} not found");
            return null;
        }
    }
    public ScriptableEnemy GetEnemy(EnemyType enemyType)
    {
        if (EnemyDictionary.TryGetValue(enemyType, out enemy))
        {
            return enemy;
        }
        else
        {
            Debug.LogError($"Enemy type {enemyType} not found");
            return null;
        }
    }
}