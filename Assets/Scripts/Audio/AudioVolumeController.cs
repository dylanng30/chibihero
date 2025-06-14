using UnityEngine;
using UnityEngine.UI;

public class AudioVolumeController : MonoBehaviour
{
    [Header("Volume Sliders")]
    [SerializeField] private Slider masterVolumeSlider;
    [SerializeField] private Slider musicVolumeSlider;
    [SerializeField] private Slider sfxVolumeSlider;
    [SerializeField] private Slider uiVolumeSlider;

    [Header("Volume Display")]
    [SerializeField] private Text masterVolumeText;
    [SerializeField] private Text musicVolumeText;
    [SerializeField] private Text sfxVolumeText;
    [SerializeField] private Text uiVolumeText;    [Header("Initial Settings")]
    [SerializeField] private float initialMasterVolume = 1f;
    [SerializeField] private float initialMusicVolume = 0.5f; // Tăng từ 0.3f
    [SerializeField] private float initialSFXVolume = 1.2f; // Tăng cho walking/jumping
    [SerializeField] private float initialUIVolume = 0.9f;

    private void Start()
    {
        InitializeSliders();
        SetupSliderListeners();
        ApplyInitialVolumes();
    }

    private void InitializeSliders()
    {
        if (masterVolumeSlider != null)
            masterVolumeSlider.value = initialMasterVolume;
        
        if (musicVolumeSlider != null)
            musicVolumeSlider.value = initialMusicVolume;
        
        if (sfxVolumeSlider != null)
            sfxVolumeSlider.value = initialSFXVolume;
        
        if (uiVolumeSlider != null)
            uiVolumeSlider.value = initialUIVolume;
    }

    private void SetupSliderListeners()
    {
        if (masterVolumeSlider != null)
            masterVolumeSlider.onValueChanged.AddListener(OnMasterVolumeChanged);
        
        if (musicVolumeSlider != null)
            musicVolumeSlider.onValueChanged.AddListener(OnMusicVolumeChanged);
        
        if (sfxVolumeSlider != null)
            sfxVolumeSlider.onValueChanged.AddListener(OnSFXVolumeChanged);
        
        if (uiVolumeSlider != null)
            uiVolumeSlider.onValueChanged.AddListener(OnUIVolumeChanged);
    }

    private void ApplyInitialVolumes()
    {
        AudioManager.SetMasterVolume(initialMasterVolume);
        AudioManager.SetMusicVolume(initialMusicVolume);
        AudioManager.SetSFXVolume(initialSFXVolume);
        AudioManager.SetUIVolume(initialUIVolume);
        
        UpdateVolumeTexts();
    }

    public void OnMasterVolumeChanged(float value)
    {
        AudioManager.SetMasterVolume(value);
        UpdateVolumeText(masterVolumeText, value);
    }

    public void OnMusicVolumeChanged(float value)
    {
        AudioManager.SetMusicVolume(value);
        UpdateVolumeText(musicVolumeText, value);
    }

    public void OnSFXVolumeChanged(float value)
    {
        AudioManager.SetSFXVolume(value);
        UpdateVolumeText(sfxVolumeText, value);
        
        // Test SFX sound when changing volume
        if (value > 0)
        {
            AudioManager.PlayUIClick();
        }
    }

    public void OnUIVolumeChanged(float value)
    {
        AudioManager.SetUIVolume(value);
        UpdateVolumeText(uiVolumeText, value);
        
        // Test UI sound when changing volume
        if (value > 0)
        {
            AudioManager.PlayUIClick();
        }
    }

    private void UpdateVolumeTexts()
    {
        UpdateVolumeText(masterVolumeText, initialMasterVolume);
        UpdateVolumeText(musicVolumeText, initialMusicVolume);
        UpdateVolumeText(sfxVolumeText, initialSFXVolume);
        UpdateVolumeText(uiVolumeText, initialUIVolume);
    }

    private void UpdateVolumeText(Text text, float value)
    {
        if (text != null)
        {
            text.text = Mathf.RoundToInt(value * 100) + "%";
        }
    }

    // Preset volume configurations
    public void SetLowMusicHighSFX()
    {
        if (musicVolumeSlider != null) musicVolumeSlider.value = 0.2f;
        if (sfxVolumeSlider != null) sfxVolumeSlider.value = 1.2f;
    }

    public void SetBalancedAudio()
    {
        if (masterVolumeSlider != null) masterVolumeSlider.value = 0.8f;
        if (musicVolumeSlider != null) musicVolumeSlider.value = 0.5f;
        if (sfxVolumeSlider != null) sfxVolumeSlider.value = 1f;
        if (uiVolumeSlider != null) uiVolumeSlider.value = 0.9f;
    }

    public void MuteAll()
    {
        if (masterVolumeSlider != null) masterVolumeSlider.value = 0f;
    }

    // Test sounds
    public void TestWalkSound()
    {
        AudioManager.PlayPlayerWalk();
    }

    public void TestJumpSound()
    {
        AudioManager.PlayPlayerJump();
    }

    public void TestAttackSound()
    {
        AudioManager.PlayPlayerAttack(1);
    }
}
