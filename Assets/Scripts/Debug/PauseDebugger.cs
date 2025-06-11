using UnityEngine;

public class PauseDebugger : MonoBehaviour
{
    [Header("Debug Info")]
    [SerializeField] private bool showDebugInfo = true;
    [SerializeField] private bool logStateChanges = true;

    private GameState lastGameState;
    private bool lastPauseState;

    void Start()
    {
        lastGameState = GameManagerTest.Instance.CurrentState;
        lastPauseState = PauseManager.Instance != null ? PauseManager.Instance.IsPaused : false;
    }

    void Update()
    {
        if (!showDebugInfo) return;

        // Check for state changes
        if (GameManagerTest.Instance.CurrentState != lastGameState)
        {
            if (logStateChanges)
            {
                Debug.Log($"Game State Changed: {lastGameState} -> {GameManagerTest.Instance.CurrentState}");
            }
            lastGameState = GameManagerTest.Instance.CurrentState;
        }

        if (PauseManager.Instance != null)
        {
            bool currentPauseState = PauseManager.Instance.IsPaused;
            if (currentPauseState != lastPauseState)
            {
                if (logStateChanges)
                {
                    Debug.Log($"Pause State Changed: {lastPauseState} -> {currentPauseState}");
                    Debug.Log($"Time Scale: {Time.timeScale}");
                }
                lastPauseState = currentPauseState;
            }
        }
    }

    void OnGUI()
    {
        if (!showDebugInfo) return;

        GUILayout.BeginArea(new Rect(10, 10, 300, 200));
        GUILayout.Label("=== PAUSE DEBUG INFO ===");
        GUILayout.Label($"Game State: {GameManagerTest.Instance.CurrentState}");
        GUILayout.Label($"Is Paused: {(PauseManager.Instance != null ? PauseManager.Instance.IsPaused : false)}");
        GUILayout.Label($"Time Scale: {Time.timeScale:F2}");
        GUILayout.Label($"Real Time: {Time.realtimeSinceStartup:F2}");
        GUILayout.Label($"Game Time: {Time.time:F2}");
        
        if (InputManager.Instance != null)
        {
            GUILayout.Label($"ESC Pressed: {InputManager.Instance.PausePressed}");
        }

        GUILayout.Space(10);
        GUILayout.Label("Controls:");
        GUILayout.Label("ESC - Pause/Resume");
        
        GUILayout.EndArea();
    }
}
