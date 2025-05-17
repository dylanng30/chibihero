using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ResourceSystem : MonoBehaviour
{
    public List<ScriptablePlayer> Players { get; private set; }
    private Dictionary<PlayerType, ScriptablePlayer> playerDictionary;

    protected void Awake()
    {
        //base.Awake();
        AssembleResource();
    }

    private void AssembleResource()
    {
        Players = Resources.LoadAll<ScriptablePlayer>("Players").ToList();
        playerDictionary = Players.ToDictionary(player => player.PlayerType, player => player);
    }

    public ScriptablePlayer GetPlayer(PlayerType playerType)
        => playerDictionary[playerType];
    public ScriptablePlayer GetRandomPlayer()
        => Players[Random.Range(0, Players.Count)];
}