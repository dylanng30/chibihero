using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewEnemyData", menuName = "Data/Enemy")]
public class EnemyScriptableObject : ScriptableObject
{
    public int maxHealth;
    public int curentHealth ;
    public float moveSpeed ;
    public float attackRange;
    public float jumpPower ;
    public int armor;
    public int damage ;
    public Sprite sprite;
    public AudioClip atackSound;
}
