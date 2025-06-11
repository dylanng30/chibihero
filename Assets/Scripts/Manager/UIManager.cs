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

    [Header("PauseMenu")]
    [SerializeField] protected GameObject PauseMenu;

    protected override void Awake()
    {
        base.Awake();
        GetCanvases();
    }
    private void GetCanvases()
    {
        UpgradeStats = FindObjectOfType<UpgradeStats>().gameObject;
        EXPBar = FindObjectOfType<EXPManager>().gameObject;
        PauseMenu = FindObjectOfType<PauseMenu>().gameObject;
        //PlayerDied = GameObject.Find("PlayerDiedCanvas");
    }

    public void DeactivateAllUIs()
    {
        UpgradeStats.SetActive(false);
        EXPBar.SetActive(false);
        if (PauseMenu != null)
            PauseMenu.SetActive(false);
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

    public void ShowPauseMenu()
    {
        if (PauseMenu != null)
        {
            PauseMenu.SetActive(true);
            PauseMenu.GetComponent<PauseMenu>().ShowPauseMenu();
        }
    }

    public void HidePauseMenu()
    {
        if (PauseMenu != null)
        {
            PauseMenu.GetComponent<PauseMenu>().HidePauseMenu();
        }
    }


}
