using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTriggerInteract : MonoBehaviour
{
    private RaycastHit hit;
    private float rayLength = 2.0f;
    public LayerMask layerMask;
    private Triggers currentTrigger;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Physics.Raycast(transform.position + (transform.forward * 0.4f), transform.forward, out hit, rayLength, layerMask, QueryTriggerInteraction.UseGlobal))
        {
            //Fix To Make Sure The Previous Interactable Can't Open And Close Even When Not Looking At It
            if(currentTrigger != null && currentTrigger != hit.transform.gameObject.GetComponent<Triggers>())
            {
                currentTrigger.canInteractInitial = false;
            }
            
            currentTrigger = hit.transform.gameObject.GetComponent<Triggers>();
            currentTrigger.canInteractInitial = true;
        }
        else
        {
            if(currentTrigger != null)
            {
                currentTrigger.canInteractInitial = false;
                currentTrigger = null;
            }
        }
    }
}
