using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public abstract class ScriptableUnitBase : ScriptableObject
{
    public Faction _faction;

    [SerializeField] public Stats _stats;
    public Stats BaseStats => _stats;

    //public PlayerUnitBase Prefab;

    public string Description;
    public Sprite MenuSprite;

    [SerializeField]
    public struct Stats
    {
        public int Health;
        public int Attack;
        public int Armor;
        public int Speed;
        public int ATKRange;
    }


}
