using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerObjectInteract : MonoBehaviour
{
    public InteractableParent playerParent;
    private InteractableParent otherParent;
    private GameObject currentObject;
    private RaycastHit interactableHit;
    private RaycastHit otherParentHit;
    private float rayLength = 2.0f;
    public LayerMask interactableLayer;
    public LayerMask otherParentLayer;

    [HideInInspector] public bool canPickup;
    [HideInInspector] public bool canPlace;

    void Update()
    {
        canPlace = false;
        if(Physics.Raycast(transform.position + (transform.forward * 0.4f), transform.forward, out otherParentHit, rayLength, otherParentLayer, QueryTriggerInteraction.Collide))
        {   
            otherParent = otherParentHit.collider.transform.GetComponent<InteractableParent>();

            //For Interact Prompt Script
            if(playerParent.currentInventory != null && otherParent.currentInventory == null)
            {
                canPlace = true;
            }
        }
        else
        {
            if(otherParent != null)
            {
                otherParent = null;
            }
        }

        canPickup = false;
        if (Physics.Raycast(transform.position + (transform.forward * 0.4f), transform.forward, out interactableHit, rayLength, interactableLayer, QueryTriggerInteraction.Ignore))
        {
            currentObject = interactableHit.transform.gameObject;
            if(playerParent.currentInventory == null)
            {
                //For Interact Prompt Script
                canPickup = true;
            }
        }
        else
        {
            if(currentObject != null)
            {
                currentObject = null;
            }
        }
    }

    public void Pickup()
    {
        if(currentObject.transform.parent != null && currentObject.transform.parent.GetComponent<InteractableParent>() != null)
        {
            otherParent = currentObject.transform.parent.GetComponent<InteractableParent>();
            if(otherParent.currentInventory != currentObject)
            {
                playerParent.Parent(currentObject);
            }
            else
            {
                GameObject objectToTransfer = otherParent.currentInventory;
                otherParent.UnParent(objectToTransfer);
                playerParent.Parent(objectToTransfer);
            }
        }
        else
        {
            playerParent.Parent(currentObject);
        }
    }

    public void Place()
    {
        if(playerParent.currentInventory != null && otherParent.currentInventory == null)
        {
            GameObject objectToTransfer = playerParent.currentInventory;
            playerParent.UnParent(objectToTransfer);
            otherParent.Parent(objectToTransfer);
        }
    }

    public void Drop()
    {
        playerParent.UnParent(playerParent.currentInventory);
    }
}

