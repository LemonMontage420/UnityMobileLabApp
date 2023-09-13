//TODO:
//Make It So That You Can Limit Which Interactables Can Parent To This Object (Should Be Controlled From This Script)
//Make Sure This System Is Robust Enough To Allow The Chaining Of Interactables (Parenting One Interactable To Another And Disallowing The Removal Of The First Interactable From That Heirarchy Before The Ones Below It Are Removed)
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableParent : MonoBehaviour
{
    public GameObject currentInventory;
    public GameObject[] legalInteractions;
    public Vector3 objectTargetPosition;
    public Vector3 objectTargetRotation;
    private bool isLegal;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void Parent(GameObject objectToParent)
    {
        isLegal = false;
        for (int i = 0; i < legalInteractions.Length; i++)
        {
            if(objectToParent == legalInteractions[i])
            {
                isLegal = true;
            }
        }
        if(isLegal)
        {
            Rigidbody rb = objectToParent.GetComponent<Rigidbody>();
        
            rb.isKinematic = true;
            objectToParent.transform.parent = transform;
            objectToParent.transform.localPosition = objectTargetPosition;
            objectToParent.transform.localRotation = Quaternion.Euler(objectTargetRotation);

            currentInventory = objectToParent;
        }
    }
    public void UnParent(GameObject objectToUnParent)
    {
        isLegal = false;
        if(transform.parent == null | transform.parent.GetComponent<InteractableParent>() == null)
        {
            isLegal = true;
        }
        
        if(isLegal)
        {
            Rigidbody rb = objectToUnParent.GetComponent<Rigidbody>();

            rb.isKinematic = false;
            objectToUnParent.transform.parent = null;

            currentInventory = null;
        }
    }
}
