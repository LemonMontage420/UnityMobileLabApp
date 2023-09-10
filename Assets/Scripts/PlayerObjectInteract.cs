using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerObjectInteract : MonoBehaviour
{
    public Transform interactTarget;
    private InteractableObject currentObject;
    private RaycastHit hit;
    private float rayLength = 2.0f;
    [HideInInspector] public GameObject currentInventory;

    // Update is called once per frame
    void Update()
    {
        if (Physics.Raycast(transform.position + (transform.forward * 0.4f), transform.forward, out hit, rayLength) && hit.transform.gameObject.GetComponent<InteractableObject>() != null)
        {
            //Fix To Make Sure The Previous Interactable Can't Open And Close Even When Not Looking At It
            if(currentObject != null && currentObject != hit.transform.gameObject.GetComponent<InteractableObject>())
            {
                currentObject.canInteract = false;
            }
            
            currentObject = hit.transform.gameObject.GetComponent<InteractableObject>();
            if(currentInventory == null)
            {
                currentObject.canInteract = true;
            }
        }
        else
        {
            if(currentObject != null)
            {
                currentObject.canInteract = false;
                currentObject = null;
            }
        }

        //Get What's Currently Being Held
        if(interactTarget.childCount != 0)
        {
            currentInventory = interactTarget.GetChild(0).gameObject;
        }
        else
        {
            currentInventory = null;
        }
    }
}
