using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDoorInteract : MonoBehaviour
{
    [HideInInspector] public Doors currentDoor;
    private RaycastHit hit;
    private float rayLength = 2.0f;
    public LayerMask layerMask;

    [HideInInspector] public bool canInteract;

    void Update()
    {
        if (Physics.Raycast(transform.position, transform.forward, out hit, rayLength, layerMask))
        {
            //Fix To Make Sure The Previous Interactable Can't Open And Close Even When Not Looking At It
            if(currentDoor != null && currentDoor != hit.transform.gameObject.GetComponent<Doors>())
            {   
                //For Interact Prompt Script
                canInteract = false;
            }
            currentDoor = hit.transform.gameObject.GetComponent<Doors>();

            //For InteractPrompt Script
            canInteract = true;
        }
        else
        {
            if(currentDoor != null)
            {
                currentDoor = null;

                //For Interact Prompt Script
                canInteract = false;
            }
        }
    }
}
