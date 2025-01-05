using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonSimulatedCombinationsEvent : MonoBehaviour
{
    public Cannon cannon;
    public float[] masses;
    public float[] angles;
    Vector2[] combinations;
    bool[] simulatedCombinations;

    [HideInInspector] public TaskEvent completeEvent;
    [HideInInspector] public TaskEvent incompleteEvent;
    [HideInInspector] public TaskEvent progressEvent;

    void Start()
    {
        combinations = new Vector2[(masses.Length * angles.Length)];
        simulatedCombinations = new bool[combinations.Length];

        for (int i = 0; i < masses.Length; i++)
        {
            for (int j = 0; j < angles.Length; j++)
            {
                combinations[(i * masses.Length) + j] = new Vector2(masses[i], angles[j]);
            }
        }
    }

    void LateUpdate()
    {
        if(!cannon.airTimeIsStopped)
        {
            for (int i = 0; i < masses.Length; i++)
            {
                for (int j = 0; j < angles.Length; j++)
                {
                    if(!simulatedCombinations[(i * masses.Length) + j])
                    {
                        if(cannon.ballMass == combinations[(i * masses.Length) + j].x && cannon.cannonAngle == combinations[(i * masses.Length) + j].y)
                        {
                            progressEvent.Invoke();
                            simulatedCombinations[(i * masses.Length) + j] = true;
                        } 
                    }  
                }
            }
        }

        bool finished = true;
        for (int i = 0; i < simulatedCombinations.Length; i++)
        {
            if(!simulatedCombinations[i])
            {
                finished = false;
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
