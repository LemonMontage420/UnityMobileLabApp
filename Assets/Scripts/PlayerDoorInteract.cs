using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDoorInteract : MonoBehaviour
{
    private Doors currentDoor;
    private RaycastHit hit;
    private float rayLength = 2.0f;

    // Update is called once per frame
    void Update()
    {
        if (Physics.Raycast(transform.position, transform.forward, out hit, rayLength) && hit.transform.gameObject.GetComponent<Doors>() != null)
        {
            //Fix To Make Sure The Previous Interactable Can't Open And Close Even When Not Looking At It
            if(currentDoor != null && currentDoor != hit.transform.gameObject.GetComponent<Doors>())
            {
                currentDoor.canInteract = false;
            }
            
            currentDoor = hit.transform.gameObject.GetComponent<Doors>();
            currentDoor.canInteract = true;
        }
        else
        {
            if(currentDoor != null)
            {
                currentDoor.canInteract = false;
                currentDoor = null;
            }
        }
    }
}
