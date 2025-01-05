using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject mainMenu;
    public GameObject labSelectMenu;

    public void Play()
    {
        labSelectMenu.SetActive(true);
        mainMenu.SetActive(false);
    }

    public void PlayLabSpring()
    {
        SceneManager.LoadScene("SpringLab");
    }
    public void PlayLabWater()
    {
        SceneManager.LoadScene("CannonLab");
    }

    public void Back()
    {
        mainMenu.SetActive(true);
        labSelectMenu.SetActive(false);
    }

    public void Quit()
    {
        Application.Quit();
    }
}