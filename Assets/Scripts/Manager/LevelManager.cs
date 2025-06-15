using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelManager : Singleton<LevelManager>
{
    [SerializeField] private GameObject loadingScreen;
    [SerializeField] private Image loadingBar;

    private float target;

    protected override void Awake()
    {
        base.Awake();
    }
    private void Update()
    {
        loadingBar.fillAmount = Mathf.MoveTowards(loadingBar.fillAmount, target, Time.deltaTime);
    }
    public async void LoadScene(string sceneName)
    {
        target = 0;
        loadingBar.fillAmount = 0;

        var scene = SceneManager.LoadSceneAsync(sceneName);
        scene.allowSceneActivation = false;

        loadingScreen.SetActive(true);

        while (scene.progress < 0.9f)
        {
            await Task.Delay(100);
            target = scene.progress;
        }

        target = 1f;
        await Task.Delay(500);

        scene.allowSceneActivation = true;

        await OnScenePreActivate(sceneName);
        Debug.Log("da load xonng");
        loadingScreen.SetActive(false);
        

    }

    private async Task OnScenePreActivate(string sceneName)
    {
        Scene loadedScene = SceneManager.GetSceneByName(sceneName);
        while (!loadedScene.isLoaded)
            await Task.Delay(100);    

        // Only set PlayerSpawn for Platform scenes
        // TopDown scenes will be handled entirely by GameManager
        if (!sceneName.Contains("TopDown"))
        {
            GameObject spawnPoint = GameObject.Find("PlayerSpawn");
            if (spawnPoint != null)
            {
                PlayerController.Instance.transform.position = spawnPoint.transform.position;
            }
        }
        // Don't set any position for TopDown - let GameManager handle it completely
    }
}
