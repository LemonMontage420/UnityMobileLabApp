using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonPointsRecordedEvent : MonoBehaviour
{
    public Blackboard blackboard;
    private bool[] pointsVisible;

    [HideInInspector] public TaskEvent completeEvent;
    [HideInInspector] public TaskEvent incompleteEvent;
    [HideInInspector] public TaskEvent progressEvent;

    void Start()
    {
        pointsVisible = new bool[blackboard.tableFields.Length];
    }

    void LateUpdate()
    {
        bool finished = true;

        for (int i = 0; i < pointsVisible.Length; i++)
        {
            if(!pointsVisible[i])
            {
                finished = false;
                if(blackboard.tableValuesVisibility[i])
                {
                    pointsVisible[i] = true;
                    progressEvent.Invoke();
                }
            }
        }
        if(finished)
        {
            completeEvent.Invoke();
        }
        else
        {
            incompleteEvent.Invoke();
        }

        completeEvent.Devoke();
        incompleteEvent.Devoke();
        progressEvent.Devoke(); 
    }
}
