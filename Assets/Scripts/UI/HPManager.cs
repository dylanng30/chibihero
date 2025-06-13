using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPManager : MonoBehaviour
{
    [SerializeField] private Image hpBar;    

    private void Update()
    {
        StartCoroutine(UpdateHpBar());
    }
    private IEnumerator UpdateHpBar()
    {
        yield return new WaitUntil(() => PlayerController.Instance != null);
        float maxHp = (float) PlayerController.Instance.PlayerStats.MaxHP;
        float currentHp = (float) PlayerController.Instance.PlayerStats.CurrentHP;
        hpBar.fillAmount = currentHp / maxHp;        
    }


}
