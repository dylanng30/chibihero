using UnityEngine;

/// <summary>
/// Component to make UI elements ignore time scale (useful for pause menus, etc.)
/// Attach this to Canvas or UI elements that should continue working during pause
/// </summary>
public class IgnoreTimeScale : MonoBehaviour
{
    private Canvas canvas;
    private CanvasGroup canvasGroup;

    void Awake()
    {
        canvas = GetComponent<Canvas>();
        canvasGroup = GetComponent<CanvasGroup>();
    }

    void Start()
    {
        // Set canvas to ignore time scale if it exists
        if (canvas != null)
        {
            // UI elements typically ignore time scale by default in Unity
            // But we can ensure proper setup here
        }
    }

    void Update()
    {
        // If this is attached to a pause menu, ensure it's always interactable during pause
        if (PauseManager.Instance != null && PauseManager.Instance.IsPaused)
        {
            if (canvasGroup != null)
            {
                canvasGroup.interactable = true;
                canvasGroup.blocksRaycasts = true;
            }
        }
    }
}
