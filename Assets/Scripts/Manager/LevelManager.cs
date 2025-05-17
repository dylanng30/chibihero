using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelManager : PersistentSingleton<LevelManager>
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
        loadingBar.fillAmount = Mathf.MoveTowards(loadingBar.fillAmount, target, Time.deltaTime * 2f);
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

        await Task.Delay(1000);

        scene.allowSceneActivation = true;
        loadingScreen.SetActive(false);

    }
}
