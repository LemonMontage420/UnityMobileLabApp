//TODO:
//Make It So That You Can Limit Which Interactables Can Parent To This Object (Should Be Controlled From This Script)
//Make Sure This System Is Robust Enough To Allow The Chaining Of Interactables (Parenting One Interactable To Another And Disallowing The Removal Of The First Interactable From That Heirarchy Before The Ones Below It Are Removed)
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableParent : MonoBehaviour
{
    public GameObject currentInventory;
    public Vector3 objectTargetPosition;
    public Vector3 objectTargetRotation;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void Parent(GameObject objectToParent)
    {
        Rigidbody rb = objectToParent.GetComponent<Rigidbody>();
        
        rb.isKinematic = true;
        objectToParent.transform.parent = transform;
        objectToParent.transform.localPosition = objectTargetPosition;
        objectToParent.transform.localRotation = Quaternion.Euler(objectTargetRotation);

        currentInventory = objectToParent;
    }
    public void UnParent(GameObject objectToUnParent)
    {
        Rigidbody rb = objectToUnParent.GetComponent<Rigidbody>();

        rb.isKinematic = false;
        objectToUnParent.transform.parent = null;

        currentInventory = null;
    }
}
