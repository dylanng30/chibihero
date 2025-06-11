using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class UIManager : Singleton<UIManager>
{
    [Header("Menu")]
    [SerializeField] protected GameObject Menu;

    [Header("UpgradedStats")]
    [SerializeField] protected GameObject UpgradeStats;

    [Header("EXP")]
    [SerializeField] protected GameObject EXPBar;

    [Header("GameOver")]
    [SerializeField] protected GameObject PlayerDied;

    protected override void Awake()
    {
        base.Awake();
        GetCanvases();
    }
    private void GetCanvases()
    {
        UpgradeStats = FindObjectOfType<UpgradeStats>().gameObject;
        EXPBar = FindObjectOfType<EXPManager>().gameObject;
        //PlayerDied = GameObject.Find("PlayerDiedCanvas");
    }

    public void DeactivateAllUIs()
    {
        UpgradeStats.SetActive(false);
        EXPBar.SetActive(false);
        //PlayerDied.SetActive(false);
    }
    public void ShowUpgradeStats()
    {
        DeactivateAllUIs();
        UpgradeStats.SetActive(true);
        Time.timeScale = 0f;
    }
    public void ShowPlayerDied()
    {
        DeactivateAllUIs();
        PlayerDied.SetActive(true);
    }

    public void ShowEXPBar()
    {
        DeactivateAllUIs();
        EXPBar.SetActive(true);
    }


}
