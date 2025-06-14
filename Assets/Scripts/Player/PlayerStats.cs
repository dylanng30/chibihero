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
    [SerializeField] protected int currentHP;
    [SerializeField] protected int maxMP;
    [SerializeField] protected int currentMP;
    [SerializeField] protected int attackPower;
    [SerializeField] protected int armor;
    [SerializeField] protected int moveSpeed;
    [SerializeField] protected int jumpPower;
    [SerializeField] protected float dashForce;
    [SerializeField] protected float dashTime;
    [SerializeField] protected float dashCooldown;

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
        currentHP = maxHP;
        attackPower = player._stats.Attack;
        armor = player._stats.Armor;
        moveSpeed = player._stats.Speed;
        jumpPower = player._stats.JumpPower;
        dashForce = player._stats.DashForce;
        dashTime = player._stats.DashTime;
        dashCooldown = player._stats.DashCooldown;
        maxMP = player.MP;
        currentMP = maxMP;
        

        //Debug.Log($"Đã load stats cho {playerType} - HP: {maxHP}, ATK: {attackPower}, Armor: {armor}, Speed: {moveSpeed}");
    }

    protected virtual void LoadPlayerController()
    {
        if (this.playerController != null)
            return;
        this.playerController = transform.GetComponentInParent<PlayerController>();
    }

    //HP
    public int MaxHP
    {
        get
        {
            return maxHP;
        }
    }
    public int CurrentHP
    {
        get
        {
            return currentHP;
        }
    }
    public void SetCurrentHP(int currentHp)
    {
        this.currentHP = currentHp;
    }
    public void UpgradeMaxHP(int value)
    {
        maxHP += value;
        currentHP += value;
        Debug.Log($"Max HP upgraded to: {maxHP}");
    }
    //MP
    public int MaxMP
    {
        get
        {
            return maxMP;
        }
    }
    public int CurrentMP
    {
        get
        {
            return currentMP;
        }
    }
    public void CostMP(int value)
    {
        this.currentMP -= value;
    }
    //
    public int AttackPower
    {
        get
        {
            return attackPower;
        }
    }
    public void UpgradeAttackPower(int value)
    {
        attackPower += value;
        Debug.Log($"Attack Power upgraded to: {attackPower}");
    }
    public int Armor
    {
        get
        {
            return armor;
        }
    }
    public void UpgradeArmor(int value)
    {
        armor += value;
        Debug.Log($"Armor upgraded to: {armor}");
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
    public float DashForce
    {
        get
        {
            return dashForce;
        }
    }
    public float DashTime
    {
        get
        {
            return dashTime;
        }
    }
    public float DashCooldown
    {
        get
        {
            return dashCooldown;
        }
    }

}
