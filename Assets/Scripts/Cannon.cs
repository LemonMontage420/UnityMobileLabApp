using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : MonoBehaviour
{
    float deltaTime;

    public Rigidbody cannonBallRB;
    public InteractableParent cannonBallAttachment;

    [Header("Parameters")]
    public Vector2 angleRange;
    public float cannonForce;
    [Tooltip("The Global Y Where The Ground Plane Is. Used To Stop The Air Time Counter")]
    public float collisionPlane = 1.255f;

    [Header("Variables")]
    public float ballMass;
    public float cannonAngle;
    public float currentAirTime;
    public bool airTimeIsStopped;

    [HideInInspector] public SoundEvent launchCannonEvent;

    void Start() 
    {
        airTimeIsStopped = true;    
    }

    void FixedUpdate()
    {
        deltaTime = Time.fixedDeltaTime;

        if(cannonBallAttachment.currentInventory != null)
        {
            cannonBallRB = cannonBallAttachment.currentInventory.GetComponent<Rigidbody>();
            ballMass = cannonBallRB.mass;
        }

        transform.localRotation = Quaternion.Slerp(transform.localRotation, Quaternion.Euler(new Vector3(cannonAngle, transform.localEulerAngles.y, transform.localEulerAngles.z)), deltaTime * 5.0f);

        if(cannonBallRB != null && cannonBallRB.position.y <= collisionPlane)
        {
            StopAirTime();
        }

        if(!airTimeIsStopped)
        {
            currentAirTime += deltaTime;
        }
    }

    public void RotateCannon(float angleIncrement)
    {
        cannonAngle += angleIncrement;
        cannonAngle = Mathf.Clamp(cannonAngle, angleRange.x, angleRange.y);
    }

    public void Launch()
    {
        if(cannonBallRB != null)
        {   
            cannonBallAttachment.UnParent(cannonBallRB.gameObject);
            cannonBallRB.transform.position = cannonBallAttachment.transform.position + (-cannonBallAttachment.transform.forward * 0.1f);
            int NonCollidingLayer = LayerMask.NameToLayer("Player");
            cannonBallRB.transform.gameObject.layer = NonCollidingLayer;

            float timeForceActedUponBall = 0.1f;
            cannonBallRB.velocity = -cannonBallAttachment.transform.forward * ((cannonForce / ballMass) * (timeForceActedUponBall * timeForceActedUponBall)); //Velocity Integrated Over The Span Of The Time The Cannon Force Acted Upon The Ball

            StartAirTime();

            launchCannonEvent.Invoke();
        }
        launchCannonEvent.Devoke();
    }

    void StartAirTime()
    {
        airTimeIsStopped = false;
        currentAirTime = 0.0f;
    }

    void StopAirTime()
    {
        airTimeIsStopped = true;
        int RegularLayer = LayerMask.NameToLayer("Interactables");
        cannonBallRB.transform.gameObject.layer = RegularLayer;
        cannonBallRB = null;
    }
}
