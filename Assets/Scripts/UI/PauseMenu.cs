using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    [Header("Pause Menu Buttons")]
    [SerializeField] private Button resumeButton;
    [SerializeField] private Button mainMenuButton;
    [SerializeField] private Button quitButton;
    [SerializeField] private GameObject muteButton;
    [SerializeField] private GameObject unmuteButton;

    void Start()
    {
        // Setup button listeners
        if (resumeButton != null)
            resumeButton.onClick.AddListener(ResumeGame);

        //if (settingsButton != null)
        //    settingsButton.onClick.AddListener(OpenSettings);

        if (mainMenuButton != null)
            mainMenuButton.onClick.AddListener(GoToMainMenu);

        if (quitButton != null)
            quitButton.onClick.AddListener(QuitGame);
    }
    void OnEnable()
    {
        // Time scale is managed by PauseManager

    }

    void OnDisable()
    {
        // Time scale is managed by PauseManager
    }

    public void ResumeGame()
    {
        PauseManager.Instance.ResumeGame();
    }

    public void Mute()
    {
        AudioListener.volume = 0f;
        muteButton.SetActive(false);
        unmuteButton.SetActive(true);
    }
    public void Unmute()
    {
        AudioListener.volume = 1f;
        muteButton.SetActive(true);
        unmuteButton.SetActive(false);
    }
    public void GoToMainMenu()
    {
        PauseManager.Instance.ForceResume(); // Force resume before going to menu
        GameManager.Instance.ChangeState(GameState.Menu);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
