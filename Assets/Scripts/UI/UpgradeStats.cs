using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class UpgradeStats : MonoBehaviour
{
    [SerializeField] private GameObject upgradePanel;

    [SerializeField] TMPro.TextMeshProUGUI HpText;
    [SerializeField] TMPro.TextMeshProUGUI DmgText;
    [SerializeField] TMPro.TextMeshProUGUI ArmorText;

    [SerializeField] private int statsPoints;

    private void OnEnable()
    {
        Time.timeScale = 0f;
    }
    private void OnDisable()
    {
        Time.timeScale = 1f;
    }

    public void UpStatsPoints()
    {
        statsPoints = 0;
        statsPoints++;
        Time.timeScale = 0f;
    }
    public void UpgradeHealth()
    {
        if (statsPoints <= 0)
            return;

        int amount = 10;
        PlayerController.Instance.PlayerStats.UpgradeMaxHP(amount);
        UpdateStats();
        statsPoints--;
    }
    public void UpgradeAttackPower()
    {
        if (statsPoints <= 0)
            return;

        int amount = 10;
        PlayerController.Instance.PlayerStats.UpgradeAttackPower(amount);
        UpdateStats();
        statsPoints--;
    }
    public void UpgradeDefense()
    {
        if (statsPoints <= 0)
            return;

        int amount = 10;
        PlayerController.Instance.PlayerStats.UpgradeArmor(amount);
        UpdateStats();
        statsPoints--;
    }
    public void NextButton()
    {
        upgradePanel.SetActive(false);
        Time.timeScale = 1f;
    }

    public void UpdateStats()
    {
        HpText.text = PlayerController.Instance.PlayerStats.MaxHP.ToString();
        DmgText.text = PlayerController.Instance.PlayerStats.AttackPower.ToString();
        ArmorText.text = PlayerController.Instance.PlayerStats.Armor.ToString();
    }

    
}
