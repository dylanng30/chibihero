using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class EXPManager : Singleton<EXPManager>
{
    [Header("Experience")]
    [SerializeField] AnimationCurve expCurve;
    private int currentLevel, totalEXP;
    private int previousLevelEXP, nextLevelEXP;

    [Header("Interface")]
    [SerializeField] TMPro.TextMeshProUGUI levelText;
    [SerializeField] TMPro.TextMeshProUGUI expText;
    [SerializeField] Image expBar;

    [Header("Upgrade")]
    [SerializeField] GameObject upgradePanel;

    public void Apply()
    {
        StartCoroutine(LoadPlayer());
        UpdateLevel();
        upgradePanel.SetActive(false);
    }

    private IEnumerator LoadPlayer()
    {
        yield return new WaitUntil(() => PlayerController.Instance != null);
        if(PlayerController.Instance.EXPManager != null)
            yield break;
        PlayerController.Instance.LoadEXPManager(this);
    }
    public void AddEXP(int exp)
    {
        totalEXP += exp;
        CheckforLevelUp();
        UpdateInterface();
    }
    private void CheckforLevelUp()
    {
        if (totalEXP >= nextLevelEXP)
        {
            currentLevel++;
            UpdateLevel();
        }
    }

    private void UpdateLevel()
    {
        previousLevelEXP = (int)expCurve.Evaluate(currentLevel);
        nextLevelEXP = (int)expCurve.Evaluate(currentLevel + 1);
        UpdateInterface();
        upgradePanel.SetActive(true);
    }
    private void UpdateInterface()
    {
        levelText.text = "Lv: " + currentLevel.ToString();
        expText.text = $"{totalEXP}/{nextLevelEXP} exp";
        expBar.fillAmount = (float)(totalEXP - previousLevelEXP) / (nextLevelEXP - previousLevelEXP);
    }

    public int GetCurrentLevel()
    {
        return currentLevel;
    }
}
