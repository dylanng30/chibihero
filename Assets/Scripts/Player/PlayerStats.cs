using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [SerializeField] private PlayerType playerType;
    [SerializeField] protected ScriptablePlayer player;
    [SerializeField] protected PlayerController playerController;

    [SerializeField] protected int maxHP;
    [SerializeField] protected int attackPower;
    [SerializeField] protected int armor;
    [SerializeField] protected int moveSpeed;
    [SerializeField] protected int jumpPower;

    void Awake()
    {
        LoadComponent();
    }

    protected void LoadComponent()
    {
        LoadPlayerStats();
        LoadPlayerController();
    }

    protected virtual void LoadPlayerStats()
    {

        StartCoroutine(LoadPlayerStatsCouroutine());
    }

    private IEnumerator LoadPlayerStatsCouroutine()
    {
        yield return new WaitUntil(() => Systems.Instance != null && Systems.Instance.ResourceSystem != null);

        player = Systems.Instance.ResourceSystem.GetPlayer(playerType);
        maxHP = player._stats.Health;
        attackPower = player._stats.Attack;
        armor = player._stats.Armor;
        moveSpeed = player._stats.Speed;
        jumpPower = player._stats.JumpPower;
        //Debug.Log($"Đã load stats cho {playerType} - HP: {maxHP}, ATK: {attackPower}, Armor: {armor}, Speed: {moveSpeed}");
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
