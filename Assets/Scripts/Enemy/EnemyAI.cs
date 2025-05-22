using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyAI : MonoBehaviour
{
    protected void FlipToPlayer()
    {
       /* if (_currentTarget == null)
            return;
        else
        {
            Vector2 dir = _currentTarget.transform.position - this.transform.position;
            if (dir.x > 0)
                this.transform.localScale = new Vector3(1, 1, 1);
            else
                this.transform.localScale = new Vector3(-1, 1, 1);
        }*/
    }
}
