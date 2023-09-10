using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Triggers : MonoBehaviour
{
    private MeshRenderer meshRenderer;
    public bool canInteractInitial;
    public GameObject[] targetObjects;
    public PlayerObjectInteract player;
    public bool canInteract;
    public Material selectedMaterial;
    public Material deselectedMaterial;
    // Start is called before the first frame update
    void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        canInteract = false;
        if(canInteractInitial)
        {
            for (int i = 0; i < targetObjects.Length; i++)
            {
                if(player.currentInventory == targetObjects[i])
                {
                    canInteract = true;
                }
            }
        }
        if(canInteract)
        {
            if(meshRenderer.material != selectedMaterial)
            {
                meshRenderer.material = selectedMaterial;
            }
        }
        else
        {
            if(meshRenderer.material != deselectedMaterial)
            {
                meshRenderer.material = deselectedMaterial;
            }
        }
    }
}
