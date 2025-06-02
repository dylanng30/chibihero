using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeStats : MonoBehaviour
{
    [SerializeField] private GameObject upgradePanel;
    public void UpgradeHealth()
    {
        int amount = 10;
        PlayerController.Instance.PlayerStats.UpgradeMaxHP(amount);
        upgradePanel.SetActive(false);
    }
    public void UpgradeAttackPower()
    {
        int amount = 10;
        PlayerController.Instance.PlayerStats.UpgradeAttackPower(amount);
        upgradePanel.SetActive(false);
    }
    public void UpgradeDefense()
    {
        int amount = 10;
        PlayerController.Instance.PlayerStats.UpgradeArmor(amount);
        upgradePanel.SetActive(false);
    }
}
