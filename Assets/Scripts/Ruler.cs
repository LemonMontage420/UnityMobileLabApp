//TODO:
//Make A Shader For The Ruler With A Bar That Fills Up To Wherever The Spring Height Is At, The Player Can Then Eyeball What The Value Is

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ruler : MonoBehaviour
{
    public Transform player;
    public InteractableParent playerParent;
    public InteractableParent springAttachmentPoint;
    public Spring spring;
    public float springLength;
    private RaycastHit hit;
    private float rayLength = 2.0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        springLength = 0.0f;
        if(playerParent.currentInventory == transform.gameObject)
        {
            if(Physics.Raycast(player.position + (player.forward * 0.4f), player.forward, out hit, rayLength) && hit.collider.transform == springAttachmentPoint.transform)
            {
                springLength = spring.springLength;
            }
        }
    }
}
