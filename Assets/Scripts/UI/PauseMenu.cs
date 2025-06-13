using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    [Header("Pause Menu Buttons")]
    [SerializeField] private Button resumeButton;
    [SerializeField] private Button mainMenuButton;
    [SerializeField] private Button quitButton;
    
    [Header("Audio Control Buttons")]
    [SerializeField] private Button musicToggleButton;
    [SerializeField] private Button soundToggleButton;
    [SerializeField] private Slider masterVolumeSlider;
    
    [Header("Audio Control Texts")]
    [SerializeField] private Text musicToggleText;
    [SerializeField] private Text soundToggleText;

    void Start()
    {
        // Setup button listeners
        if (resumeButton != null)
            resumeButton.onClick.AddListener(ResumeGame);

        if (mainMenuButton != null)
            mainMenuButton.onClick.AddListener(GoToMainMenu);

        if (quitButton != null)
            quitButton.onClick.AddListener(QuitGame);
            
        // Setup audio control buttons
        if (musicToggleButton != null)
            musicToggleButton.onClick.AddListener(ToggleMusic);
            
        if (soundToggleButton != null)
            soundToggleButton.onClick.AddListener(ToggleSoundEffects);
            
        if (masterVolumeSlider != null)
            masterVolumeSlider.onValueChanged.AddListener(SetMasterVolume);
            
        // Initialize audio control UI
        UpdateAudioUI();
    }
    void OnEnable()
    {
        // Time scale is managed by PauseManager
        UpdateAudioUI(); // Update UI when menu opens
    }

    void OnDisable()
    {
        // Time scale is managed by PauseManager
    }

    public void ResumeGame()
    {
        PauseManager.Instance.ResumeGame();
    }

    //public void OpenSettings()
    //{
    //    // TODO: Implement settings menu
    //    Debug.Log("Settings menu not implemented yet");
    //}
    public void GoToMainMenu()
    {
        PauseManager.Instance.ForceResume(); // Force resume before going to menu
        GameManager.Instance.ChangeState(GameState.Menu);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
    
    // Audio Control Methods
    public void ToggleMusic()
    {
        if (AudioSystem.Instance != null)
        {
            AudioSystem.Instance.ToggleMusic();
            AudioSystem.Instance.SaveAudioSettings();
            UpdateAudioUI();
        }
    }
    
    public void ToggleSoundEffects()
    {
        if (AudioSystem.Instance != null)
        {
            AudioSystem.Instance.ToggleSoundEffects();
            AudioSystem.Instance.SaveAudioSettings();
            UpdateAudioUI();
        }
    }
    
    public void SetMasterVolume(float volume)
    {
        if (AudioSystem.Instance != null)
        {
            AudioSystem.Instance.SetMasterVolume(volume);
            AudioSystem.Instance.SaveAudioSettings();
        }
    }
    
    private void UpdateAudioUI()
    {
        if (AudioSystem.Instance == null) return;
        
        // Update music toggle text
        if (musicToggleText != null)
            musicToggleText.text = AudioSystem.Instance.MusicEnabled ? "Music: ON" : "Music: OFF";
            
        // Update sound toggle text
        if (soundToggleText != null)
            soundToggleText.text = AudioSystem.Instance.SoundEffectsEnabled ? "Sound: ON" : "Sound: OFF";
            
        // Update master volume slider
        if (masterVolumeSlider != null)
            masterVolumeSlider.value = AudioListener.volume;
    }
}
