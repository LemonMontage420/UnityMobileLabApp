using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableParent : MonoBehaviour
{
    public InteractableParent playerInteractableParent;
    public GameObject validInteractionSphere;
    [System.Serializable]
    public struct LegalInteractions
    {
        public GameObject gameObject;
        public Vector3 targetPosition;
        public Vector3 targetRotation;
    };

    public GameObject currentInventory;
    public LegalInteractions[] legalInteractions;
    private bool isLegal;

    void LateUpdate() 
    {
        if(playerInteractableParent != null && validInteractionSphere != null)
        {
            validInteractionSphere.SetActive(false);
            if(playerInteractableParent.currentInventory != null && currentInventory == null)
            {
                for (int i = 0; i < legalInteractions.Length; i++)
                {
                    if(playerInteractableParent.currentInventory == legalInteractions[i].gameObject)
                    {
                        validInteractionSphere.SetActive(true);
                    }
                }
            } 
        }   
    }

    public void Parent(GameObject objectToParent)
    {
        Vector3 currentTargetPos = Vector3.zero;
        Vector3 currentTargetRot = Vector3.zero;

        isLegal = false;
        for (int i = 0; i < legalInteractions.Length; i++)
        {
            if(objectToParent == legalInteractions[i].gameObject)
            {
                isLegal = true;
                currentTargetPos = legalInteractions[i].targetPosition;
                currentTargetRot = legalInteractions[i].targetRotation;
            }
        }
        if(isLegal)
        {
            Rigidbody rb = objectToParent.GetComponent<Rigidbody>();
        
            rb.isKinematic = true;
            objectToParent.transform.parent = transform;
            objectToParent.transform.localPosition = currentTargetPos;
            objectToParent.transform.localRotation = Quaternion.Euler(currentTargetRot);

            currentInventory = objectToParent;
        }
    }
    public void UnParent(GameObject objectToUnParent)
    {
        Rigidbody rb = objectToUnParent.GetComponent<Rigidbody>();

        rb.isKinematic = false;
        objectToUnParent.transform.parent = null;
        currentInventory = null;
    }
}
