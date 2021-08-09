using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseScript : MonoBehaviour
{
    public static bool GameIsPaused = false;

    public GameObject pauseMenuUI;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Resume() //BTN Resume
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    void Pause() //BTN Pause
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        Debug.Log("TiempoPausado");
        GameIsPaused = true;
    }

    public void LoadMenu() //BTN LoadMenu
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Menu");
    }

    public void QuitGame() //BTN Quit
    {
        Application.Quit();
    }
}
