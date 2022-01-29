using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class InitialSceneManager : MonoBehaviour
{
    public TextMeshProUGUI highscoreText;
    public GameObject initialTexts;
    public Slider loadingBar;

    private AsyncOperation loadingSceneOperation;
    private bool isLoadingScene = false;

    // Start is called before the first frame update
    void Start()
    {
        highscoreText.text = "Highscore: " + PlayerPrefs.GetInt("highscore", 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (isLoadingScene)
            loadingBar.value = Mathf.Clamp01(loadingSceneOperation.progress / .9f);
        if (Input.GetKeyUp(KeyCode.Return) && !isLoadingScene)
            StartGame();
    }

    private void StartGame()
    {
        initialTexts.SetActive(false);
        loadingBar.gameObject.SetActive(true);
        isLoadingScene = true;
        loadingSceneOperation = SceneManager.LoadSceneAsync("MainScene");
    }
}
