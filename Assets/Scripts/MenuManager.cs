using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public GameObject pausePanel;
    public GameObject player;

    private bool isGameActive = true;
    private bool isGameOver = false;
    private int maxScore = 500;

    private GameManager gameManager;

    private void Start() {
        gameManager = GameManager.gameManager;
    }

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape) && !isGameOver)
        {
            TogglePause();
        }
        else if (Input.GetKeyUp(KeyCode.O) && isGameActive)
        {
            TogglePause();
            GameOver();
        }
        else if (Input.GetKeyUp(KeyCode.Return) && isGameOver)
        {
            Time.timeScale = 1;
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        //WinScreen
        if(gameManager.gameWon)
        {
            StartCoroutine(ShowWinScreen());
            gameManager.gameWon = false;
        }
    }

    private void TogglePause()
    {
        isGameActive = !isGameActive;

        if (isGameActive)
        {
            pausePanel.SetActive(false);
            Time.timeScale = 1;
            player.GetComponent<PlayerMovement>().canMove = true;
            player.GetComponentInChildren<Flashlight>().enabled = true;
        }
        else
        {
            pausePanel.SetActive(true);
            Time.timeScale = 0;
            player.GetComponent<PlayerMovement>().canMove = false;
            player.GetComponentInChildren<Flashlight>().enabled = false;
        }
    }

    private void GameOver()
    {
        isGameOver = true;
        int highscore = Mathf.RoundToInt(maxScore - Time.timeSinceLevelLoad);
        if (highscore < 0)
            highscore = 0;
        if (PlayerPrefs.GetInt("highscore", 0) < highscore)
            PlayerPrefs.SetInt("highscore", highscore);
        pausePanel.GetComponentInChildren<TextMeshProUGUI>().text = "Score: " + highscore + "\n\n\nGame over\n\n\nPress 'Enter' to restart";
    }

    IEnumerator ShowWinScreen()
    {
        yield return new WaitForSeconds(5);
        pausePanel.GetComponentInChildren<TextMeshProUGUI>().text = "You did it! \n The store has finally power and even Oswald is happy! \n \n Thank you for playing!";
        TogglePause();
    }
}
