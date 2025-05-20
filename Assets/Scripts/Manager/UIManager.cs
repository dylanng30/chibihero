using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : Singleton<UIManager>
{
    [Header("Menu")]
    [SerializeField] protected GameObject Menu;

    [Header("UpgradedStats")]
    [SerializeField] protected GameObject UpgradeStats;

    [Header("GameOver")]
    [SerializeField] protected GameObject PlayerDied;

    protected override void Awake()
    {
        base.Awake();
        //GetCanvases();
    }
    private void GetCanvases()
    {
        Menu = GameObject.Find("MenuCanvas");
        UpgradeStats = GameObject.Find("UpgradeStatsCanvas");
        PlayerDied = GameObject.Find("PlayerDiedCanvas");
    }

    public void DeactivateAllUIs()
    {
        Menu.SetActive(false);
        UpgradeStats.SetActive(false);
        PlayerDied.SetActive(false);
    }
    public void ShowMenu()
    {
        DeactivateAllUIs();
        Menu.SetActive(true);
    }
    public void ShowUpgradeStats()
    {
        DeactivateAllUIs();
        UpgradeStats.SetActive(true);
    }
    public void ShowPlayerDied()
    {
        DeactivateAllUIs();
        PlayerDied.SetActive(true);
    }


}
