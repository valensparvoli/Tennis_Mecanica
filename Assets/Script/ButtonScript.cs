using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonScript : MonoBehaviour
{
    //Script encargado de todos los botones del menu

    public GameObject controlsMenu;
    public void VolverMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    public void VolverJugar()
    {
        SceneManager.LoadScene("PlayerVSIA");
        
    }

    public void PlayerVSIA()
    {
        SceneManager.LoadScene("PlayerVSIA");
    }

    public void PlayerVSPlayer()
    {
        SceneManager.LoadScene("PlayerVSPlayer");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void Controles()
    {
        controlsMenu.SetActive(true);
    }
}
