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

    // Update is called once per frame
    void Update()
    {
        if(Physics.Raycast(transform.position + (transform.forward * 0.4f), transform.forward, out otherParentHit, rayLength, otherParentLayer, QueryTriggerInteraction.Collide))
        {
            otherParent = otherParentHit.collider.transform.GetComponent<InteractableParent>();
            if(Input.GetKeyDown(KeyCode.E))
            {
                if(playerParent.currentInventory != null && otherParent.currentInventory == null)
                {
                    GameObject objectToTransfer = playerParent.currentInventory;
                    playerParent.UnParent(objectToTransfer);
                    otherParent.Parent(objectToTransfer);
                }
            }
        }
        else
        {
            if(otherParent != null)
            {
                otherParent = null;
            }
        }

        if (Physics.Raycast(transform.position + (transform.forward * 0.4f), transform.forward, out interactableHit, rayLength, interactableLayer, QueryTriggerInteraction.Ignore))
        {
            currentObject = interactableHit.transform.gameObject;
            if(playerParent.currentInventory == null && Input.GetKeyDown(KeyCode.E))
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
        }
        else
        {
            if(playerParent.currentInventory != null && Input.GetKeyDown(KeyCode.E))
            {
                playerParent.UnParent(playerParent.currentInventory);
            }
            if(currentObject != null)
            {
                currentObject = null;
            }
        }
    }
}

