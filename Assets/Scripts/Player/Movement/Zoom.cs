using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Zoom : MonoBehaviour
{
    public Camera cam;
    public bool isZoomed;
    public Vector2 fovRange;
    private float currentFOV;
    public float zoomSpeed;

    [HideInInspector] public bool prevZoom;

    // Start is called before the first frame update
    void Start()
    {
        isZoomed = false;
        currentFOV = fovRange.y;
    }

    // Update is called once per frame
    void Update()
    {        
        if(isZoomed)
        {
            currentFOV = Mathf.Lerp(currentFOV, fovRange.x, Time.deltaTime * zoomSpeed);
        }
        else
        {
            currentFOV = Mathf.Lerp(currentFOV, fovRange.y, Time.deltaTime * zoomSpeed);
        }
        cam.fieldOfView = currentFOV;

        prevZoom = isZoomed;
    }
    
    public void Zooming() //Used By UI System
    {
        isZoomed = !isZoomed;
    }
}
