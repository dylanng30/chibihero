using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lava : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") ||
           collision.CompareTag("Enemy"))
        {
            var entity = collision.transform.parent.GetComponent<IDamagable>();
            entity.TakeDamage(9999999, gameObject);
        }
    }
}
