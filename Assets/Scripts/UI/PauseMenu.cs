using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    [Header("Pause Menu Buttons")]
    [SerializeField] private Button resumeButton;
    [SerializeField] private Button settingsButton;
    [SerializeField] private Button mainMenuButton;
    [SerializeField] private Button quitButton;

    void Start()
    {
        // Setup button listeners
        if (resumeButton != null)
            resumeButton.onClick.AddListener(ResumeGame);
        
        if (settingsButton != null)
            settingsButton.onClick.AddListener(OpenSettings);
        
        if (mainMenuButton != null)
            mainMenuButton.onClick.AddListener(GoToMainMenu);
        
        if (quitButton != null)
            quitButton.onClick.AddListener(QuitGame);
    }    void OnEnable()
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

    public void OpenSettings()
    {
        // TODO: Implement settings menu
        Debug.Log("Settings menu not implemented yet");
    }    public void GoToMainMenu()
    {
        PauseManager.Instance.ForceResume(); // Force resume before going to menu
        GameManagerTest.Instance.ChangeState(GameState.Menu);
    }

    public void QuitGame()
    {
        Debug.Log("Quitting game...");
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }    void Update()
    {
        // Allow ESC key to close pause menu
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ResumeGame();
        }
    }
}
