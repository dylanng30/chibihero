using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MPManager : MonoBehaviour
{
    [SerializeField] private Image mpBar;

    private float deltaSpeed = 2f;

    private void Update()
    {
        StartCoroutine(UpdateMpBar());
    }
    private IEnumerator UpdateMpBar()
    {
        yield return new WaitUntil(() => PlayerController.Instance != null);
        float maxMp = (float)PlayerController.Instance.PlayerStats.MaxMP;
        float currentMp = (float)PlayerController.Instance.PlayerStats.CurrentMP;
        float target = currentMp / maxMp;
        mpBar.fillAmount = Mathf.MoveTowards(mpBar.fillAmount, target, deltaSpeed * Time.deltaTime);
    }
}
