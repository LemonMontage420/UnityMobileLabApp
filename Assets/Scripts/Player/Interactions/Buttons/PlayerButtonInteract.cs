using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerButtonInteract : MonoBehaviour
{
    [HideInInspector] public RaycastHit hit;
    private float rayLength = 2.0f;
    public LayerMask layerMask;
    [HideInInspector] public Button currentButton;

    [HideInInspector] public bool canInteract;

    void Update()
    {
        if (Physics.Raycast(transform.position, transform.forward, out hit, rayLength, layerMask))
        {
            //Fix To Make Sure The Previous Interactable Can't Open And Close Even When Not Looking At It
            if(currentButton != null && currentButton != hit.transform.gameObject.GetComponent<Button>())
            {   
                //For Interact Prompt Script
                canInteract = false;
            }
            currentButton = hit.transform.gameObject.GetComponent<Button>();

            //For InteractPrompt Script
            canInteract = true;
        }
        else
        {
            if(currentButton != null)
            {
                currentButton = null;

                //For Interact Prompt Script
                canInteract = false;
            }
        }
    }
}
