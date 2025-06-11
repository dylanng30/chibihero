using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingText : MonoBehaviour
{
    public float DestroyTime = 2f;
    public Vector3 Offset = new Vector3(0, 1, 0);

    private void Start()
    {
        Destroy(gameObject, DestroyTime);

        this.transform.position += Offset;
    }
}
