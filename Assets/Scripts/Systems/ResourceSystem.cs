using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ResourceSystem : Singleton<ResourceSystem>
{
    public List<ScriptablePlayer> Players { get; private set; }
    private Dictionary<PlayerType, ScriptablePlayer> playerDictionary;

    protected override void Awake()
    {
        base.Awake();
        AssembleResource();
    }

    private void AssembleResource()
    {
        Players = Resources.LoadAll<ScriptablePlayer>("Players").ToList();
        playerDictionary = Players.ToDictionary(player => player._playerType, player => player);
    }

    public ScriptablePlayer GetPlayer(PlayerType playerType)
        => playerDictionary[playerType];
    public ScriptablePlayer GetRandomPlayer()
        => Players[Random.Range(0, Players.Count)];
}
