using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerObjectInteract : MonoBehaviour
{
    private InteractableObject currentObject;
    private RaycastHit hit;
    private float rayLength = 2.0f;
    private InteractableObject currentInventory;

    // Update is called once per frame
    void Update()
    {
        if (Physics.Raycast(transform.position, transform.forward, out hit, rayLength) && hit.transform.gameObject.GetComponent<InteractableObject>() != null)
        {
            currentObject = hit.transform.gameObject.GetComponent<InteractableObject>();
            currentObject.canInteract = true;
        }
    }
}
