using UnityEngine;
using UnityEngine.SceneManagement;

public class GameAudioManager : MonoBehaviour
{
    [Header("Background Music")]
    [SerializeField] private string menuMusicName = "Phoenix-Wright-Ace-Attorney-OST-Pressing-Pursuit-_-Cornered";
    [SerializeField] private string gameplayMusicName = "Phoenix-Wright-Ace-Attorney-OST-Pressing-Pursuit-_-Cornered";
    
    [Header("Audio Settings")]
    [SerializeField] private bool playMusicOnStart = true;
    [SerializeField] private bool fadeTransitions = true;
    [SerializeField] private float fadeTime = 2f;

    private string currentSceneName;    private void Start()
    {
        currentSceneName = SceneManager.GetActiveScene().name;
        
        // Đặt volume nhạc nền cao hơn
        AudioManager.SetMusicVolume(0.5f); // Tăng từ 0.3f lên 0.5f
        
        if (playMusicOnStart)
        {
            PlaySceneMusic();
        }
        
        // Subscribe to scene change events
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        currentSceneName = scene.name;
        PlaySceneMusic();
    }

    private void PlaySceneMusic()
    {
        string musicToPlay = GetMusicForScene(currentSceneName);
        
        if (!string.IsNullOrEmpty(musicToPlay))
        {
            AudioManager.PlayBackgroundMusic(musicToPlay, fadeTransitions);
        }
    }

    private string GetMusicForScene(string sceneName)
    {
        // Determine music based on scene name
        return sceneName.ToLower() switch
        {
            string name when name.Contains("menu") || name.Contains("main") => menuMusicName,
            string name when name.Contains("game") || name.Contains("level") || name.Contains("platform") || name.Contains("topdown") => gameplayMusicName,
            _ => gameplayMusicName // Default to gameplay music
        };
    }

    public void PlayMenuMusic()
    {
        AudioManager.PlayBackgroundMusic(menuMusicName, fadeTransitions);
    }

    public void PlayGameplayMusic()
    {
        AudioManager.PlayBackgroundMusic(gameplayMusicName, fadeTransitions);
    }

    public void StopMusic()
    {
        AudioManager.StopBackgroundMusic(fadeTransitions);
    }

    public void SetMusicVolume(float volume)
    {
        AudioManager.SetMusicVolume(volume);
    }

    public void SetSFXVolume(float volume)
    {
        AudioManager.SetSFXVolume(volume);
    }

    public void SetMasterVolume(float volume)
    {
        AudioManager.SetMasterVolume(volume);
    }
}
