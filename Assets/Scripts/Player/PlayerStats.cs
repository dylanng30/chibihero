using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.U2D.Aseprite;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [SerializeField] private PlayerType playerType;
    [SerializeField] protected ScriptablePlayer player;
    // Start is called before the first frame update
    private int currentHealth;
    private int attackPower;
    private int armor;
    private float moveSpeed;

    void Start()
    {
        LoadComponent();
    }

    protected void LoadComponent()
    {
        LoadPlayerStats();
    }
    protected virtual void LoadPlayerStats()
    {
        //player = ResourceSystem.Instance.GetPlayer(playerType);
        player = Systems.Instance.ResourceSystem.GetPlayer(playerType);
        if (player == null)
        {
            Debug.LogError($"Không tìm thấy ScriptablePlayer với loại {playerType}");
            return;
        }

        currentHealth = player._stats.Health;
        attackPower = player._stats.Attack;
        armor = player._stats.Armor;
        moveSpeed = player._stats.Speed;

        Debug.Log($"Đã load stats cho {playerType} - HP: {currentHealth}, ATK: {attackPower}, Armor: {armor}, Speed: {moveSpeed}");
    }

}
