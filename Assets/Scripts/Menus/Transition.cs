using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Transition : MonoBehaviour
{
    public Transform fadeInOutGO;
    private Image image;
    public float transitionDuration;
    float currentDuration;
    
    [Header("End State")]
    public bool winState;
    public Doors labDoor;

    [HideInInspector] public SoundEvent fadeInEvent;
    [HideInInspector] public SoundEvent fadeOutEvent;

    void Start() 
    {
        if(!fadeInOutGO.gameObject.activeSelf)
        {
            fadeInOutGO.gameObject.SetActive(true);
        }
        image = fadeInOutGO.GetComponent<Image>();

        currentDuration = transitionDuration;
    }

    void Update() 
    {
        if(labDoor != null)
        {
            if(labDoor.isOpen && !labDoor.isLocked)
            {
                winState = true;
            }
        }

        if(!winState)
        {
            fadeInEvent.Invoke();
            currentDuration -= Time.deltaTime;
            if(currentDuration < 0.0f)
            {
                currentDuration = 0.0f;
                fadeInOutGO.gameObject.SetActive(false);
            }
            image.color = new Vector4(image.color.r, image.color.g, image.color.b, currentDuration / transitionDuration);
            fadeInEvent.Devoke();
        }
        else
        {
            fadeOutEvent.Invoke();
            if(!fadeInOutGO.gameObject.activeSelf)
            {
                fadeInOutGO.gameObject.SetActive(true);
            }

            currentDuration += Time.deltaTime;
            if(currentDuration > transitionDuration)
            {
                currentDuration = transitionDuration;
                SceneManager.LoadScene("Menu");
            }
            image.color = new Vector4(image.color.r, image.color.g, image.color.b, currentDuration / transitionDuration);
            fadeOutEvent.Devoke();
        }
    }
}
