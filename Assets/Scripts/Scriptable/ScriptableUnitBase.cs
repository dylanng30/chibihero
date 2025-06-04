using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ScriptableUnitBase : ScriptableObject
{
    public Faction _faction;    

    public Stats _stats;

    //public string Description;
    //public Sprite MenuSprite;

    public Stats BaseStats => _stats;
}
[Serializable]
public struct Stats
{
    public int Health;
    public int Attack;
    public int Armor;
    public int Speed;
    public float ATKRange;
    public int JumpPower;
    public float DashForce;
    public float DashTime;
    public float DashCooldown;
}