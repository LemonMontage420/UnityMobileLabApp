using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointsRecordedEvent : MonoBehaviour
{
    public Blackboard blackboard;
    private bool[] pointsVisible;

    [HideInInspector] public TaskEvent completeEvent;
    [HideInInspector] public TaskEvent incompleteEvent;
    [HideInInspector] public TaskEvent progressEvent;

    void Start() 
    {
        pointsVisible = new bool[blackboard.pointVisibility.Length];
    }

    void Update()
    {
        bool finished = true;
        for (int i = 0; i < blackboard.pointVisibility.Length; i++)
        {
            if(!pointsVisible[i])
            {
                finished = false;

                if(blackboard.pointVisibility[i])
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
