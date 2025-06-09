using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeStats : MonoBehaviour
{
    [SerializeField] private GameObject upgradePanel;

    [SerializeField] TMPro.TextMeshProUGUI HpText;
    [SerializeField] TMPro.TextMeshProUGUI DmgText;
    [SerializeField] TMPro.TextMeshProUGUI ArmorText;

    [SerializeField] private int statsPoints;

    public void UpStatsPoints()
    {
        statsPoints = 0;
        statsPoints++;
        Debug.Log(statsPoints);
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
    }

    public void UpdateStats()
    {
        HpText.text = PlayerController.Instance.PlayerStats.MaxHP.ToString();
        DmgText.text = PlayerController.Instance.PlayerStats.AttackPower.ToString();
        ArmorText.text = PlayerController.Instance.PlayerStats.Armor.ToString();
    }

    
}
