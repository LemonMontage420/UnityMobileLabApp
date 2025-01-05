using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMouse : MonoBehaviour
{
    public DeviceType deviceType;
    private bool isMobile;

    public PlayerMovement movement;
    public Transform player;
    public float cameraSensitivity;

    private float mouseX;
    private float mouseY;
    float pitchRot;
    
    void Start()
    {
        Input.multiTouchEnabled = true;
        if(deviceType == DeviceType.Handheld)
        {
            isMobile = true;
        }
        if(deviceType == DeviceType.Desktop)
        {
            isMobile = false;
        }

        //Only Lock On PC
        if(!isMobile)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Input.multiTouchEnabled = false;
        }
        pitchRot = 0.0f;
    }

    void Update()
    {
        float inputX = 0.0f;
        float inputY = 0.0f;
        if(isMobile)
        {
            if(Input.touchCount != 0)
            {
                if(movement.isMoving)
                {
                    if(Input.touchCount > 1)
                    {
                        for (int i = 0; i < Input.touchCount; i++)
                        {
                            if(i != movement.touchID)
                            {
                                inputX = Input.GetTouch(i).deltaPosition.x / 100.0f;
                                inputY = Input.GetTouch(i).deltaPosition.y / 100.0f;
                            }
                        }
                    }
                }
                else
                {
                    inputX = Input.GetTouch(0).deltaPosition.x / 100.0f;
                    inputY = Input.GetTouch(0).deltaPosition.y / 100.0f;
                }
            }
        }
        else
        {
            inputX = Input.GetAxis("Mouse X");
            inputY = Input.GetAxis("Mouse Y");
        }
        mouseX = inputX * Time.deltaTime * cameraSensitivity;
        player.Rotate(player.up * mouseX); //Rotates The Character Controller Transform In The Yaw Axis

        mouseY = inputY * Time.deltaTime * cameraSensitivity;
        pitchRot -= mouseY;
        pitchRot = Mathf.Clamp(pitchRot, -90.0f, 90.0f);
        transform.localRotation = Quaternion.Euler(pitchRot, 0.0f, 0.0f); //Rotates The Camera Transform In The Pitch Axis
    }
}
