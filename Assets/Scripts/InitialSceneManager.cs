using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class InitialSceneManager : MonoBehaviour
{
    public Button startGameButton;
    public Slider loadingBar;

    private AsyncOperation loadingSceneOperation;
    private bool isLoadingScene = false;

    // Start is called before the first frame update
    void Start()
    {
        startGameButton.onClick.AddListener(StartGameClicked);
    }

    // Update is called once per frame
    void Update()
    {
        if (isLoadingScene)
            loadingBar.value = Mathf.Clamp01(loadingSceneOperation.progress / .9f);
    }

    private void StartGameClicked()
    {
        startGameButton.gameObject.SetActive(false);
        loadingBar.gameObject.SetActive(true);
        isLoadingScene = true;
        loadingSceneOperation = SceneManager.LoadSceneAsync("MainScene");
    }
}
