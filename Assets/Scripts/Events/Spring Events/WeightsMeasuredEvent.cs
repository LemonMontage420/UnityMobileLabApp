using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeightsMeasuredEvent : MonoBehaviour
{
    public InteractableParent attachmentPoint;
    public GameObject[] weights;
    private bool[] attachmentsMade;

    public TaskEvent completeEvent;
    public TaskEvent progressEvent;

    void Start() 
    {
        attachmentsMade = new bool[weights.Length];    
    }

    void Update()
    {
        for (int i = 0; i < weights.Length; i++)
        {
            if(attachmentPoint.currentInventory == weights[i] && !attachmentsMade[i])
            {
                attachmentsMade[i] = true;
                progressEvent.Invoke();
            }
        }

        bool complete = true;
        for (int i = 0; i < attachmentsMade.Length; i++)
        {
            if(!attachmentsMade[i])
            {
                complete = false;
            }
        }
        if(complete)
        {
            completeEvent.Invoke();
        }

        completeEvent.Devoke();
        progressEvent.Devoke();
    }
}
