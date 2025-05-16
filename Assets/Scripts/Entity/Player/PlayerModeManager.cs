using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerModeManager : Singleton<PlayerModeManager>
{
    public enum Mode { TopDown, Platformer }

    private ControllerTopDown topDownController;
    private PlayerController platformerController;
    private StateManager stateManager;
    private Rigidbody2D rb;

    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(gameObject);
        rb = GetComponent<Rigidbody2D>();
        topDownController = GetComponent<ControllerTopDown>();
        platformerController = GetComponent<PlayerController>();
        stateManager = GetComponent<StateManager>();
    }

    private void Update()
    {
        string scene = SceneManager.GetActiveScene().name;
        if (scene.Contains("TopDown"))
            SetMode(Mode.TopDown);
        else
            SetMode(Mode.Platformer);
    }

    private void SetMode(Mode mode)
    {
        if (mode == Mode.TopDown)
        {
            topDownController.enabled = true;
            platformerController.enabled = false;
            stateManager.enabled = false;

            rb.gravityScale = 0;
        }
        else if (mode == Mode.Platformer)
        {
            topDownController.enabled = false;
            platformerController.enabled = true;
            stateManager.enabled = true;

            rb.gravityScale = 1;
        }
    }
}
