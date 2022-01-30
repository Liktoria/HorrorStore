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

    private GameManager gameManager;

    private void Start() {
        gameManager = GameManager.gameManager;

        pausePanel.GetComponentInChildren<TextMeshProUGUI>().text = "Game paused! \n\n Press 'Esc' to continue \n\n Press 'Q' to quit";
    }

    void Update()
    {
        // Pause
        if (Input.GetKeyUp(KeyCode.Escape) && !gameManager.gameWon)
        {
            TogglePause();
        }
        // Restart
        else if (Input.GetKeyUp(KeyCode.Return) && gameManager.gameWon)
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
        // Exit
        else if (Input.GetKeyDown(KeyCode.Q) && !isGameActive)
        {
            Application.Quit();
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

    IEnumerator ShowWinScreen()
    {
        yield return new WaitForSeconds(5);
        pausePanel.GetComponentInChildren<TextMeshProUGUI>().text = "You did it! \n The store has finally power and even Oswald is happy! \n \n Thank you for playing!" +
            "\n\n Press 'Enter' to restart \n\n Press 'Q' to quit";
        TogglePause();
    }
}
