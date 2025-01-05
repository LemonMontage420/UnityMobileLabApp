using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [Header("Main")]
    public GameObject joystickGO;
    public GameObject pauseGO;
    public GameObject taskGO;
    public GameObject interactionGO;
    public GameObject blackboardGO;
    public GameObject zoomGO;
    public GameObject crosshairGO;

    [Header("Pause Menu")]
    public GameObject pauseButton;
    public GameObject pauseMenu;
    public bool isPaused;

    [Header("Task Menu")]
    public GameObject taskButton;
    public GameObject taskMenu;
    public bool isInTaskMenu;

    [Header("Interact Buttons")]
    public PlayerObjectInteract objectInteract;
    public PlayerDoorInteract doorInteract;
    public PlayerButtonInteract buttonInteract;
    public PlayerBlackboardInteract blackboardInteract;
    public GameObject pickupButton;
    public GameObject dropButton;
    public GameObject placeButton;
    public GameObject recordButton;
    public GameObject interactButton;
    public bool isRecording;

    [Header("Zoom Buttons")]
    public Zoom zoom;
    private Image zoomImage;
    public Sprite zoomInSprite;
    public Sprite zoomOutSprite;

    [Header("Sound Events")]
    [HideInInspector] public SoundEvent pickupEvent;
    [HideInInspector] public SoundEvent placeEvent;
    [HideInInspector] public SoundEvent dropEvent;

    [HideInInspector] public SoundEvent zoomInEvent;
    [HideInInspector] public SoundEvent zoomOutEvent;

    [HideInInspector] public SoundEvent doorOpenEvent;
    [HideInInspector] public SoundEvent doorCloseEvent;
    [HideInInspector] public SoundEvent doorLockedEvent;

    [HideInInspector] public SoundEvent buttonClickEvent;

    [HideInInspector] public SoundEvent blackboardRecordEvent;

    void Start() 
    {
        zoomImage = zoomGO.GetComponent<Image>();
    }

    void Update() 
    {
        PauseMenu();
        TaskMenu();
        InteractionButtons();
        Zooming();
        Joystick();
        Crosshair();
    }
    



    void PauseMenu()
    {
        pauseGO.SetActive(false);
        if(!isInTaskMenu)
        {
            pauseGO.SetActive(true);
        }
    }

    public void Pause()
    {
        pauseMenu.SetActive(true);
        pauseButton.SetActive(false);
        isPaused = true;
        Time.timeScale = 0.0f;
    }
    public void Resume()
    {
        pauseButton.SetActive(true);
        pauseMenu.SetActive(false);
        isPaused = false;
        Time.timeScale = 1.0f;
    }
    public void Quit()
    {
        SceneManager.LoadScene("Menu");
        Time.timeScale = 1.0f;
    }



    void TaskMenu()
    {
        taskGO.SetActive(false);
        if(!isPaused)
        {
            taskGO.SetActive(true);
        }
    }

    public void TaskOpen()
    {
        taskMenu.SetActive(true);
        taskButton.SetActive(false);
        isInTaskMenu = true;
    }

    public void TaskClose()
    {
        taskButton.SetActive(true);
        taskMenu.SetActive(false);
        isInTaskMenu = false;
    }



    void InteractionButtons()
    {
        interactionGO.SetActive(false);

        pickupButton.SetActive(false);
        dropButton.SetActive(false);
        placeButton.SetActive(false);
        interactButton.SetActive(false);
        recordButton.SetActive(false);

        if(!isPaused && !isInTaskMenu && !isRecording)
        {
            interactionGO.SetActive(true);

            if(objectInteract.canPickup)
            {
                pickupButton.SetActive(true);
            }
            if(objectInteract.playerParent.currentInventory != null)
            {
                dropButton.SetActive(true);
            }
            if(objectInteract.canPlace)
            {
                placeButton.SetActive(true);
            }
            if(doorInteract.canInteract | (buttonInteract != null && buttonInteract.canInteract))
            {
                interactButton.SetActive(true);
            }
            if(blackboardInteract.canWrite)
            {
                recordButton.SetActive(true);
            }
        }
        if(!blackboardInteract.canWrite | isPaused | isInTaskMenu)
        {
            blackboardGO.SetActive(false);
            isRecording = false;
        }
    }
    public void Pickup()
    {
        pickupEvent.Invoke();
        objectInteract.Pickup();
        pickupEvent.Devoke();
    }
    public void Drop()
    {
        dropEvent.Invoke();
        objectInteract.Drop();
        dropEvent.Devoke();
    }
    public void Place()
    {
        placeEvent.Invoke();
        objectInteract.Place();
        placeEvent.Devoke();
    }
    public void Interact()
    {
        if(doorInteract.canInteract)
        {
            if(!doorInteract.currentDoor.isLocked)
            {
                doorInteract.currentDoor.isOpen = !doorInteract.currentDoor.isOpen;

                if(doorInteract.currentDoor.prevOpen != doorInteract.currentDoor.isOpen)
                {
                    if(doorInteract.currentDoor.isOpen)
                    {
                        doorOpenEvent.Invoke();
                    }
                    else
                    {
                        doorCloseEvent.Invoke();
                    }
                }
            }
            else
            {
                doorLockedEvent.Invoke();
            }
        }
        doorOpenEvent.Devoke();
        doorCloseEvent.Devoke();
        doorLockedEvent.Devoke();
        
        if(buttonInteract != null && buttonInteract.canInteract)
        {
            buttonInteract.currentButton.Pressed();
            buttonClickEvent.Invoke();
        }
        buttonClickEvent.Devoke();
    }
    public void Record()
    {
        blackboardGO.SetActive(true);
        isRecording = true;

        blackboardRecordEvent.Invoke();
        blackboardRecordEvent.Devoke();
    }




    void Zooming()
    {
        zoomGO.SetActive(false);

        if(!isPaused && !isInTaskMenu)
        {
            zoomGO.SetActive(true);

            if(zoom.isZoomed)
            {
                zoomImage.sprite = zoomOutSprite;
                if(zoom.prevZoom != zoom.isZoomed)
                {
                    zoomInEvent.Invoke();
                }
            }
            if(!zoom.isZoomed)
            {
                zoomImage.sprite = zoomInSprite;
                if(zoom.prevZoom != zoom.isZoomed)
                {
                    zoomOutEvent.Invoke();
                }
            }
        }
        zoomInEvent.Devoke();
        zoomOutEvent.Devoke();
    }



    void Joystick()
    {
        joystickGO.SetActive(false);

        if(!isPaused && !isInTaskMenu)
        {
            joystickGO.SetActive(true);
        }
    }



    void Crosshair()
    {
        crosshairGO.SetActive(false);

        if(!isPaused && !isInTaskMenu)
        {
            crosshairGO.SetActive(true);
        }
    }
}
