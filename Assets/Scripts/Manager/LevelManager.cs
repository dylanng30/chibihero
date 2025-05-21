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

        do
        {
            await Task.Delay(100);
            target = scene.progress;
        } while (scene.progress < 0.9f);

        scene.allowSceneActivation = true;

        await OnScenePreActivate(sceneName);

        await Task.Delay(100);
        loadingScreen.SetActive(false);
    }

    private async Task OnScenePreActivate(string sceneName)
    {
        Scene loadedScene = SceneManager.GetSceneByName(sceneName);
        while (!loadedScene.isLoaded)
            await Task.Delay(100);

        GameObject spawnPoint = GameObject.Find("PlayerSpawn");
        PlayerController.Instance.transform.position = spawnPoint.transform.position;
    }
}
