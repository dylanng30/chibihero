using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [SerializeField] private PlayerType playerType;
    [SerializeField] protected ScriptablePlayer player;
    [SerializeField] protected PlayerController playerController;

    protected int maxHP;
    protected int attackPower;
    protected int armor;
    protected int moveSpeed;
    protected int jumpPower;

    void Start()
    {
        LoadComponent();
    }

    protected void LoadComponent()
    {
        LoadPlayerController();
        LoadPlayerStats();
    }

    protected virtual void LoadPlayerStats()
    {
        player = Systems.Instance.ResourceSystem.GetPlayer(playerType);

        maxHP = player._stats.Health;
        attackPower = player._stats.Attack;
        armor = player._stats.Armor;
        moveSpeed = player._stats.Speed;
        jumpPower = player._stats.JumpPower;

        Debug.Log($"Đã load stats cho {playerType} - HP: {maxHP}, ATK: {attackPower}, Armor: {armor}, Speed: {moveSpeed}");
    }

    protected virtual void LoadPlayerController()
    {
        if (this.playerController != null)
            return;
        this.playerController = transform.GetComponentInParent<PlayerController>();
    }
    public int MaxHP
    {
        get
        {
            return maxHP;
        }
    }
    public int AttackPower
    {
        get
        {
            return attackPower;
        }
    }
    public int Armor
    {
        get
        {
            return armor;
        }
    }
    public int MoveSpeed
    {
        get
        {
            return moveSpeed;
        }
    }
    public int JumpPower
    {
        get
        {
            return jumpPower;
        }
    }

}
