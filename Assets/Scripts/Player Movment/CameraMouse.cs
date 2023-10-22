using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMouse : MonoBehaviour
{
    public Transform player;
    private Camera cam;
    public float cameraSensitivity;

    private float mouseX;
    private float mouseY;
    float pitchRot;

    private bool isZoomed;
    public Vector2 fovRange;
    private float currentFOV;
    public float zoomSpeed;
    
    // Start is called before the first frame update
    void Start()
    {
        cam = GetComponent<Camera>();

        Cursor.lockState = CursorLockMode.Locked;
        pitchRot = 0.0f;
        isZoomed = false;
        currentFOV = fovRange.y;
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

        if(Input.GetKeyDown(KeyCode.Z))
        {
            isZoomed = !isZoomed;
        }
        if(isZoomed)
        {
            currentFOV = Mathf.Lerp(currentFOV, fovRange.x, Time.deltaTime * zoomSpeed);
        }
        else
        {
            currentFOV = Mathf.Lerp(currentFOV, fovRange.y, Time.deltaTime * zoomSpeed);
        }
        cam.fieldOfView = currentFOV;
    }
}
