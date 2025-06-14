using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class UIManager : Singleton<UIManager>
{
    [Header("UpgradedStats")]
    [SerializeField] protected GameObject UpgradeStats;

    [Header("HUD Player")]
    [SerializeField] protected GameObject HUDPlayer;

    [Header("Pause")]
    [SerializeField] protected GameObject PauseMenu;

    [Header("GameOver")]
    [SerializeField] protected GameObject GameOverPanel;

    [Header("Win")]
    [SerializeField] protected GameObject WinPanel;


    protected override void Awake()
    {
        base.Awake();
    }

    public void DeactivateAllUIs()
    {
        UpgradeStats.SetActive(false);
        HUDPlayer.SetActive(false);
        PauseMenu.SetActive(false);
        GameOverPanel.SetActive(false);
        WinPanel.SetActive(false);
    }
    public void ShowGameOverPanel()
    {
        DeactivateAllUIs();
        GameOverPanel.SetActive(true);
    }
    public void ShowWinPanel()
    {
        DeactivateAllUIs();
        WinPanel.SetActive(true);
    }


    public void ShowEXPBar()
    {
        DeactivateAllUIs();
        HUDPlayer.SetActive(true);
    }
    public void ShowPauseMenu()
    {
        // Play UI sound
        AudioManager.PlayUIClick();
        
        if (PauseMenu != null)
            PauseMenu.SetActive(true);            
        else
            Debug.LogError("PauseMenu is null! Make sure PauseCanvas exists in the scene.");
    }

    public void HidePauseMenu()
    {
        // Play UI sound
        AudioManager.PlayUIClick();
        
        if (PauseMenu != null)
            PauseMenu.SetActive(false);
    }


}
