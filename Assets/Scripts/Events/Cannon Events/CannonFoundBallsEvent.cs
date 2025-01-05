using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonFoundBallsEvent : MonoBehaviour
{
    public InteractableParent playerHand;
    public GameObject[] cannonBalls;
    private bool[] foundBalls;

    [HideInInspector] public TaskEvent completeEvent;
    [HideInInspector] public TaskEvent incompleteEvent;
    [HideInInspector] public TaskEvent progressEvent;

    // Start is called before the first frame update
    void Start()
    {
        foundBalls = new bool[cannonBalls.Length];
    }

    // Update is called once per frame
    void LateUpdate()
    {
        bool finished = true;
        for (int i = 0; i < foundBalls.Length; i++)
        {
            if(!foundBalls[i])
            {
                finished = false;

                if(playerHand.currentInventory == cannonBalls[i])
                {
                    foundBalls[i] = true;
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
