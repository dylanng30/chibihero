using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SceneEnemyConfig", menuName = "Enemy System/Scene Enemy Config")]
public class SceneEnemyConfig : ScriptableObject
{
    public string sceneName;
    public List<EnemyData> enemies;
}