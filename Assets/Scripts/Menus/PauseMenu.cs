using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenu;
    public GameObject crossHair;
    public bool isPaused;

    private void LateUpdate() 
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Resume();
        }

        if(pauseMenu.activeInHierarchy)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            Time.timeScale = 0.0f;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            Time.timeScale = 1.0f;
        }
    }

    public void Resume()
    {
        if(pauseMenu.activeInHierarchy)
        {
            pauseMenu.SetActive(false);
            crossHair.SetActive(true);
        }
        else
        {
            pauseMenu.SetActive(true);
            crossHair.SetActive(false);
        }
    }

    public void Quit()
    {
        SceneManager.LoadScene("Menu");
    }
}
