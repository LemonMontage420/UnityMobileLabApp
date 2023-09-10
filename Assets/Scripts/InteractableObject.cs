using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableObject : MonoBehaviour
{
    private Rigidbody rb;
    public Transform locationTarget;
    public Vector3 rotationTarget;
    public bool canInteract;
    public bool isPickedUp;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.E))
        {
            if(isPickedUp)
            {
                isPickedUp = false;
            }
            if(!isPickedUp && canInteract)
            {
                isPickedUp = true;
            }
        }
        
        if(isPickedUp)
        {
            PickupObject();
        }
        else
        {
            DropObject();
        }
    }

    void PickupObject()
    {
        rb.isKinematic = true;
        transform.parent = locationTarget;
        transform.position = locationTarget.position;
        transform.localRotation = Quaternion.Euler(rotationTarget);
    }
    void DropObject()
    {
        rb.isKinematic = false;
        transform.parent = null;
    }
}
