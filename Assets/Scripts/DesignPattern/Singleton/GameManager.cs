using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum GameState { Exploring, Fighting }
public class GameManager : Singleton<GameManager>
{
    public GameObject currentPos;
    private bool IsCompleted = false;
    public GameObject mainTDPool, redArcherPool, redPawnPool;
    public GameState currentState { get; private set; }
    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(gameObject);
        
    }
    private void Start()
    {
        LoadPool();
        StartCoroutine(LoadMapCoroutine("MainTopDown"));
    }

    public void ChangeState(GameState state)
    {

    }
    public void LoadMap(string mapName)
    {
        StartCoroutine(LoadMapCoroutine(mapName));
    }
    public IEnumerator LoadMapCoroutine(string mapName)
    {
        Debug.Log("đã tới");
        SceneManager.LoadScene(mapName);
        yield return null;

        DeactiveAllScene();
        ActiveCurrentScene(mapName);
        yield return StartCoroutine(WaitAndUpdatePosition());

        EnemySpawn spawner = GameObject.FindObjectOfType<EnemySpawn>();
        if (spawner != null)
            spawner.ForceSpawn();

        EnemyManager.Instance.ActiveEnemy();
    }
    private void LoadPool()
    {
        mainTDPool = GameObject.Find("MainTopDown");
        redArcherPool = GameObject.Find("RedArcher");
        redPawnPool = GameObject.Find("RedPawn");
    }

    private void DeactiveAllScene()
    {
        mainTDPool.SetActive(false);
        redArcherPool.SetActive(false);
        redPawnPool.SetActive(false);
    }
    private void ActiveCurrentScene(string sceneName)
    {
        switch (sceneName)
        {
            case "MainTopDown":
                mainTDPool.SetActive(true);
                break;
            case "RedArcher":
                redArcherPool.SetActive(true);
                break;
            case "RedPawn":
                redPawnPool.SetActive(true);
                break;
        }

    }

    public void CheckEnemiesLeft()
    {
        bool activeEnemy = EnemyManager.Instance.CheckEnemiesAndReturnToTopDown();
        Debug.Log(activeEnemy);        

        if (activeEnemy)
        {
            //EnemyManager.Instance.ActiveEnemy();
            IsCompleted = true;
            LoadMap("MainTopDown");
            return;
        }
        IsCompleted = false;
    }
    public bool isCompleted()
    {
        return IsCompleted;
    }

    public GameState GetGameState()
    {
        return currentState;
    }
    private IEnumerator WaitAndUpdatePosition()
    {
        yield return null;
        GameObject spawnPoint = GameObject.Find("PlayerSpawn");
        if (spawnPoint != null)
        {
            PlayerModeManager.Instance.transform.position = spawnPoint.transform.position;
        }
    }
    private void UpdatePosition()
    {
        PlayerModeManager.Instance.transform.position = GameObject.Find("PlayerSpawn").transform.position;
    }
}
