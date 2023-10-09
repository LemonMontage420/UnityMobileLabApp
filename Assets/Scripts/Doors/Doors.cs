using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Doors : MonoBehaviour
{
    public enum HingeAxis{X, Y, Z};
    public HingeAxis hingeAxis;
    public bool canInteract;
    public bool isOpen;
    public float hingeSpeed;
    public Vector2 hingeRange;
    private Vector3 hingeAxisRotMultiplier;
    // Start is called before the first frame update
    void Start()
    {
        hingeAxisRotMultiplier = Vector3.zero;
        if(hingeAxis == HingeAxis.X)
        {
            hingeAxisRotMultiplier.x = 1.0f;
        }
        if(hingeAxis == HingeAxis.Y)
        {
            hingeAxisRotMultiplier.y = 1.0f;
        }
        if(hingeAxis == HingeAxis.Z)
        {
            hingeAxisRotMultiplier.z = 1.0f;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (canInteract)
        {
            if(Input.GetKeyDown(KeyCode.E))
            {
                isOpen = !isOpen;
            }
        }

        if(isOpen)
        {
            transform.localRotation = Quaternion.Euler(Mathf.Lerp(transform.localEulerAngles.x, hingeRange.y * hingeAxisRotMultiplier.x, Time.deltaTime * hingeSpeed), Mathf.Lerp(transform.localEulerAngles.y, hingeRange.y * hingeAxisRotMultiplier.y, Time.deltaTime * hingeSpeed), Mathf.Lerp(transform.localEulerAngles.z, hingeRange.y * hingeAxisRotMultiplier.z, Time.deltaTime * hingeSpeed));
        }
        else
        {
             transform.localRotation = Quaternion.Euler(Mathf.Lerp(transform.localEulerAngles.x, hingeRange.x * hingeAxisRotMultiplier.x, Time.deltaTime * hingeSpeed), Mathf.Lerp(transform.localEulerAngles.y, hingeRange.x * hingeAxisRotMultiplier.y, Time.deltaTime * hingeSpeed), Mathf.Lerp(transform.localEulerAngles.z, hingeRange.x * hingeAxisRotMultiplier.z, Time.deltaTime * hingeSpeed));
        }
    }
}
