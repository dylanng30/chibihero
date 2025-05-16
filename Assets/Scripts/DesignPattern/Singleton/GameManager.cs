using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum GameState { Exploring, Fighting }
public class GameManager : Singleton<GameManager>
{
    public GameObject currentPos;
    public GameObject mainTDPool, redArcherPool, redPawnPool;
    public GameState currentState { get; private set; }
    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(gameObject);
        
    }
    private void Start()
    {
        StartCoroutine(LoadMapCoroutine("MainTopDown"));
    }
    public void LoadMap(string mapName)
    {
        if (mapName == SceneManager.GetActiveScene().name)
            return;
        StartCoroutine(LoadMapCoroutine(mapName));
    }
    public IEnumerator LoadMapCoroutine(string mapName)
    {
        Debug.Log("Đang load map: " + mapName);
        SceneManager.LoadScene(mapName);

        yield return null;
        yield return StartCoroutine(WaitAndUpdatePosition());
        EnemySpawn enemySpawn = GameObject.FindObjectOfType<EnemySpawn>();
        if (enemySpawn != null)
            enemySpawn.Spawn(); // Chỉ spawn nếu chưa có enemy
        EnemyManager.Instance.ActivatePool(); // Luôn active/deactive đúng enemy
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
}
