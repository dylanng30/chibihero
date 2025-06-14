using UnityEngine;

public enum DecorType
{
    Mine,
}

public enum MineType
{
    Real,
    Fake,
}


[CreateAssetMenu(fileName = "Mine", menuName = "Scriptable/Mine")]
public class MineSO : ScriptableObject
{
    public DecorType Type;
    public MineType MineType;
    public Vector3 Position;
    public GameObject Prefab;
}
