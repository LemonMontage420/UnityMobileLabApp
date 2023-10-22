using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenu;
    public GameObject crossHair;
    public bool isPaused;

    void Start() 
    {
        isPaused = false;    
    }

    void Update() 
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            isPaused = !isPaused;
        }

        if(isPaused)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            Time.timeScale = 0.0f;

            crossHair.SetActive(false);
            pauseMenu.SetActive(true);
        }
        if(!isPaused)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            Time.timeScale = 1.0f;
            
            crossHair.SetActive(true);
            pauseMenu.SetActive(false);
        }
    }

    public void Resume()
    {
        isPaused = false;
    }

    public void Quit()
    {
        SceneManager.LoadScene("Menu");
    }
}
