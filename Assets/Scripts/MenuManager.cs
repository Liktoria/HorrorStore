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

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
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
    }

    private void TogglePause()
    {
        isGameActive = !isGameActive;

        if (isGameActive)
        {
            pausePanel.SetActive(false);
            Time.timeScale = 1;
            player.GetComponent<PlayerMovement>().canMove = true;
        }
        else
        {
            pausePanel.SetActive(true);
            Time.timeScale = 0;
            player.GetComponent<PlayerMovement>().canMove = false;
        }
    }

    private void GameOver()
    {
        isGameOver = true;
        pausePanel.GetComponentInChildren<TextMeshProUGUI>().text = "Game over\n\n\nPress Enter to restart";
    }
}
