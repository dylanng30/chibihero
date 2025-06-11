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

    [Header("Pause")]
    [SerializeField] protected GameObject PauseMenu;

    protected override void Awake()
    {
        base.Awake();
        GetCanvases();
    }
    private void GetCanvases()
    {
        Menu = GameObject.Find("MenuCanvas");
        //UpgradeStats = FindObjectOfType<UpgradeStats>().gameObject;
        EXPBar = FindObjectOfType<EXPManager>().gameObject;
        //PlayerDied = GameObject.Find("PlayerDiedCanvas");
        PauseMenu = GameObject.Find("PauseCanvas");
        if (PauseMenu == null)
        {
            Debug.LogWarning("PauseCanvas not found. Please create a PauseCanvas GameObject in the scene.");
        }
    }

    public void DeactivateAllUIs()
    {
        Menu.SetActive(false);
        //UpgradeStats.SetActive(false);
        EXPBar.SetActive(false);
        //PlayerDied.SetActive(false);
        if (PauseMenu != null)
            PauseMenu.SetActive(false);
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

    public void ShowEXPBar()
    {
        DeactivateAllUIs();
        EXPBar.SetActive(true);
    }

    public void ShowPauseMenu()
    {
        if (PauseMenu != null)
            PauseMenu.SetActive(true);
        else
            Debug.LogError("PauseMenu is null! Make sure PauseCanvas exists in the scene.");
    }

    public void HidePauseMenu()
    {
        if (PauseMenu != null)
            PauseMenu.SetActive(false);
    }
}
