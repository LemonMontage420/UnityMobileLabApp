using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBlackboardInteract : MonoBehaviour
{
    public GameObject blackboardUI;
    public LayerMask layerMask;
    private RaycastHit hit;
    private float rayLength = 4.0f;
    private bool canInteract;
    private bool isInteracting;

    [HideInInspector] public bool canWrite;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //For Interact Prompt Script
        canWrite = false;

        if (Physics.Raycast(transform.position, transform.forward, out hit, rayLength, layerMask))
        {
            //canInteract = true;

            //For Interact Prompt Script
            canWrite = true;
        }
        else
        {
            //canInteract = false;

            //For Interact Prompt Script
            canWrite = false;
        }

        // if(canInteract)
        // {
        //     if(Input.GetKeyDown(KeyCode.T))
        //     {
        //         isInteracting = !isInteracting;
        //     }
        // }
        // else
        // {
        //     isInteracting = false;
        // }

        // if(isInteracting)
        // {
        //     blackboardUI.SetActive(true);

        //     //For Interact Prompt Script
        //     canWrite = false;
        // }
        // else
        // {
        //     blackboardUI.SetActive(false);
        // }
    }
}
