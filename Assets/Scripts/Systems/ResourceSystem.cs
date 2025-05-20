using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ResourceSystem : MonoBehaviour
{
    public List<ScriptablePlayer> Players { get; private set; }
    public Dictionary<PlayerType, ScriptablePlayer> PlayerDictionary = new Dictionary<PlayerType, ScriptablePlayer>();
    private ScriptablePlayer player;

    protected void Awake()
    {
        AssembleResource();
    }

    private void AssembleResource()
    {
        Players = Resources.LoadAll<ScriptablePlayer>("Players").ToList();
        PlayerDictionary = Players.ToDictionary(player => player.PlayerType, player => player);
    }

    public ScriptablePlayer GetPlayer(PlayerType playerType)
    {
        /*switch(playerType)
        {
            case PlayerType.BlueWarrior:
                player = Players.FirstOrDefault(p => p.PlayerType == PlayerType.BlueWarrior);
                break;

            default:
                Debug.LogError($"Player type {playerType} not found");
                break;
        }

        return player;*/

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
    public ScriptablePlayer GetRandomPlayer()
        => Players[Random.Range(0, Players.Count)];
}