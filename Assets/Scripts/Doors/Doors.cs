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
            transform.localRotation = Quaternion.Lerp(Quaternion.Euler(transform.localEulerAngles), Quaternion.Euler(new Vector3(hingeRange.y * hingeAxisRotMultiplier.x, hingeRange.y * hingeAxisRotMultiplier.y, hingeRange.y * hingeAxisRotMultiplier.z)), Time.deltaTime * hingeSpeed);
        }
        else
        {
            transform.localRotation = Quaternion.Lerp(Quaternion.Euler(transform.localEulerAngles), Quaternion.Euler(new Vector3(hingeRange.x * hingeAxisRotMultiplier.x, hingeRange.x * hingeAxisRotMultiplier.y, hingeRange.x * hingeAxisRotMultiplier.z)), Time.deltaTime * hingeSpeed);
        }
    }
}
