using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMouse : MonoBehaviour
{
    public Transform player;
    public float cameraSensitivity;

    private float mouseX;
    private float mouseY;
    float pitchRot;
    
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        pitchRot = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        mouseX = Input.GetAxis("Mouse X") * Time.deltaTime * cameraSensitivity;
        player.Rotate(player.up * mouseX); //Rotates The Character Controller Transform In The Yaw Axis

        mouseY = Input.GetAxis("Mouse Y") * Time.deltaTime * cameraSensitivity;
        pitchRot -= mouseY;
        pitchRot = Mathf.Clamp(pitchRot, -90.0f, 90.0f);
        transform.localRotation = Quaternion.Euler(pitchRot, 0.0f, 0.0f); //Rotates The Camera Transform In The Pitch Axis
    }
}
