using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RulerAttachmentEvent : MonoBehaviour
{
    public InteractableParent attachmentPoint;

    public TaskEvent completeEvent;
    public TaskEvent incompleteEvent;

    void Update()
    {
        if(attachmentPoint.currentInventory != null)
        {
            completeEvent.Invoke();
        }
        else
        {
            incompleteEvent.Invoke();
        }

        completeEvent.Devoke();
        incompleteEvent.Devoke();
    }
}
