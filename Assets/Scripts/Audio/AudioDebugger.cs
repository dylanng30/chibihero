using UnityEngine;

public class AudioDebugger : MonoBehaviour
{
    [Header("Debug Controls")]
    [SerializeField] private KeyCode playMusicKey = KeyCode.M;
    [SerializeField] private KeyCode playWalkKey = KeyCode.W;
    [SerializeField] private KeyCode playJumpKey = KeyCode.J;
    [SerializeField] private KeyCode playAttackKey = KeyCode.A;
    [SerializeField] private KeyCode stopMusicKey = KeyCode.S;

    [Header("Volume Test")]
    [SerializeField] private KeyCode increaseMusicVolumeKey = KeyCode.Plus;
    [SerializeField] private KeyCode decreaseMusicVolumeKey = KeyCode.Minus;

    private float currentMusicVolume = 0.5f;

    private void Update()
    {
        if (Input.GetKeyDown(playMusicKey))
        {
            Debug.Log("🎵 Playing background music...");
            AudioManager.PlayBackgroundMusic("Phoenix-Wright-Ace-Attorney-OST-Pressing-Pursuit-_-Cornered");
        }

        if (Input.GetKeyDown(playWalkKey))
        {
            Debug.Log("👟 Playing walk sound...");
            AudioManager.PlayPlayerWalk(transform.position);
        }

        if (Input.GetKeyDown(playJumpKey))
        {
            Debug.Log("🦘 Playing jump sound...");
            AudioManager.PlayPlayerJump(transform.position);
        }

        if (Input.GetKeyDown(playAttackKey))
        {
            Debug.Log("⚔️ Playing attack sound...");
            AudioManager.PlayPlayerAttack(Random.Range(1, 4), transform.position);
        }

        if (Input.GetKeyDown(stopMusicKey))
        {
            Debug.Log("🔇 Stopping background music...");
            AudioManager.StopBackgroundMusic();
        }

        if (Input.GetKeyDown(increaseMusicVolumeKey))
        {
            currentMusicVolume = Mathf.Clamp01(currentMusicVolume + 0.1f);
            AudioManager.SetMusicVolume(currentMusicVolume);
            Debug.Log($"🔊 Music Volume: {currentMusicVolume:F1}");
        }

        if (Input.GetKeyDown(decreaseMusicVolumeKey))
        {
            currentMusicVolume = Mathf.Clamp01(currentMusicVolume - 0.1f);
            AudioManager.SetMusicVolume(currentMusicVolume);
            Debug.Log($"🔉 Music Volume: {currentMusicVolume:F1}");
        }
    }

    private void OnGUI()
    {
        if (!Application.isPlaying) return;

        GUILayout.BeginArea(new Rect(10, 10, 300, 200));
        GUILayout.Label("🎧 Audio Debug Controls:");
        GUILayout.Label($"M - Play Music");
        GUILayout.Label($"W - Play Walk Sound");
        GUILayout.Label($"J - Play Jump Sound");
        GUILayout.Label($"A - Play Attack Sound");
        GUILayout.Label($"S - Stop Music");
        GUILayout.Label($"+/- - Music Volume");
        GUILayout.Label($"Current Music Volume: {currentMusicVolume:F1}");
        
        if (AudioSystem.Instance != null)
        {
            var audioSystem = AudioSystem.Instance;
            if (audioSystem.transform.Find("Music Source") != null)
            {
                var musicSource = audioSystem.transform.Find("Music Source").GetComponent<AudioSource>();
                GUILayout.Label($"Music Playing: {musicSource.isPlaying}");
                GUILayout.Label($"Music Clip: {(musicSource.clip ? musicSource.clip.name : "None")}");
            }
        }
        
        GUILayout.EndArea();
    }

    [ContextMenu("Test All Sounds")]
    public void TestAllSounds()
    {
        StartCoroutine(TestSoundsSequence());
    }

    private System.Collections.IEnumerator TestSoundsSequence()
    {
        Debug.Log("🎵 Starting audio test sequence...");
        
        // Start background music
        AudioManager.PlayBackgroundMusic("Phoenix-Wright-Ace-Attorney-OST-Pressing-Pursuit-_-Cornered");
        yield return new WaitForSeconds(2f);
        
        // Test player sounds while music is playing
        Debug.Log("👟 Testing walk sound with background music...");
        AudioManager.PlayPlayerWalk(transform.position);
        yield return new WaitForSeconds(1f);
        
        Debug.Log("🦘 Testing jump sound with background music...");
        AudioManager.PlayPlayerJump(transform.position);
        yield return new WaitForSeconds(1f);
        
        Debug.Log("⚔️ Testing attack sounds with background music...");
        for (int i = 1; i <= 3; i++)
        {
            AudioManager.PlayPlayerAttack(i, transform.position);
            yield return new WaitForSeconds(0.8f);
        }
        
        Debug.Log("✅ Audio test complete! Music should still be playing.");
    }
}
