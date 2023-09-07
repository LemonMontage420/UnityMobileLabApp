using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;
    public Transform groundCheck;
    public LayerMask groundMask;
    public float movementSpeed;
    public bool isGrounded;
    private float longInput;
    private float latInput;
    private float gravity = -9.81f;
    private float gravityVelocity;

    // Update is called once per frame
    void Update()
    {
        //Get The Inputs For The Lateral And Longitudinal Movement
        longInput = Input.GetAxisRaw("Vertical");
        latInput = Input.GetAxisRaw("Horizontal");
        //Relate The Inputs To Their Axis, Combine Them, And Normalize Them (This Prevents The Player From Exceeding The Max Speed (Hitting The Square Root Of Two The Max Speed) When Holding Down Both Directions)
        Vector3 movementDir = Vector3.Normalize((transform.right * latInput) + (transform.forward * longInput));
        //Do A Simple Ground Check To See If We Should Apply Gravity Or Not
        isGrounded = Physics.CheckSphere(groundCheck.position, controller.radius + controller.skinWidth, groundMask, QueryTriggerInteraction.Ignore);
        if(!isGrounded)
        {
            gravityVelocity += gravity * Time.deltaTime;
        }
        else
        {
            gravityVelocity = 0.0f;
        }

        //Create The Final Movement "Force" Vector By Combining The Movement With The Gravity
        Vector3 movement = (movementDir * movementSpeed * Time.deltaTime) + (Vector3.up * gravityVelocity * Time.deltaTime);
        //Apply The Movement "Force" Vector
        controller.Move(movement);
    }
}
