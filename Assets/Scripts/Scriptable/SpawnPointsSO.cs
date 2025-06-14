using UnityEngine;

[CreateAssetMenu(fileName = "SpawnPoint", menuName = "ScriptableObjects/SpawnPoint")]
public class SpawnPointsSO : ScriptableObject
{
    public EnemyType EnemyType;
    public Vector3 Position;
}
