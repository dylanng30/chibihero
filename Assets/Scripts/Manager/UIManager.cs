using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class UIManager : Singleton<UIManager>
{
    //[Header("Menu")]
    //[SerializeField] protected GameObject Menu;

    [Header("UpgradedStats")]
    [SerializeField] protected GameObject UpgradeStats;

    [Header("HUD Player")]
    [SerializeField] protected GameObject HUDPlayer;

    //[Header("GameOver")]
    //[SerializeField] protected GameObject PlayerDied;

    [Header("Pause")]
    [SerializeField] protected GameObject PauseMenu;


    protected override void Awake()
    {
        base.Awake();
        LoadAllCanvases();
    }

    private void LoadAllCanvases()
    {


    }

    public void DeactivateAllUIs()
    {
        UpgradeStats.SetActive(false);
        HUDPlayer.SetActive(false);
        //PlayerDied.SetActive(false);
        PauseMenu.SetActive(false);

    }
    /*public void ShowUpgradeStats()
    {
        DeactivateAllUIs();
        UpgradeStats.SetActive(true);
        Time.timeScale = 0f;
    }*/
    //public void ShowPlayerDied()
    //{
    //    DeactivateAllUIs();
    //    PlayerDied.SetActive(true);
    //}

    public void ShowEXPBar()
    {
        DeactivateAllUIs();
        HUDPlayer.SetActive(true);
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
