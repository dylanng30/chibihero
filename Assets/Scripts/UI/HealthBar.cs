using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Image hpBar;

    private float target = 1;
    private float deltaSpeed = 1f;

    private bool isFacingRight;

    private void Update()
    {
        FLip();
        hpBar.fillAmount = Mathf.MoveTowards(hpBar.fillAmount, target, deltaSpeed * Time.deltaTime);
    }

    public void UpdateHeathBar(int currentHp, int maxHp)
    { 
        target = (float) currentHp / (float) maxHp;
    }
    public void FlipHealthBar(bool isFacingRight)
    {
        this.isFacingRight = isFacingRight;
    }

    public void FLip()
    {
        hpBar.fillOrigin = isFacingRight ? 0 : 1;
    }
}
