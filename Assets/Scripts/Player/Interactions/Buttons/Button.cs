using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{
    public Cannon cannon;
    public enum CannonButtonType{Launch, Rotate};
    public CannonButtonType buttonType;
    public float angleIncrement;

    public Vector3 pressedInPosition;
    Vector3 unPressedPosition;
    float timer;
    bool isPressing;

    void Start() 
    {
        unPressedPosition = transform.position;
    }

    [ContextMenu("Record Pressed In Position")]
    void RecordPosition()
    {
        pressedInPosition = transform.position;
    }

    void LateUpdate() 
    {
        if(isPressing)
        {
            timer += Time.deltaTime * 15.0f;
        }
        if(timer >= Mathf.PI)
        {
            timer = 0.0f;
            isPressing = false;
        }

        transform.position = new Vector3(Mathf.Lerp(unPressedPosition.x, pressedInPosition.x, Mathf.Sin(timer)), Mathf.Lerp(unPressedPosition.y, pressedInPosition.y, Mathf.Sin(timer)), Mathf.Lerp(unPressedPosition.z, pressedInPosition.z, Mathf.Sin(timer)));
    }
    
    public void Pressed()
    {
        isPressing = true;
        Action();
    }
    
    void Action()
    {
        if(buttonType == CannonButtonType.Launch)
        {
            cannon.Launch();
        }
        if(buttonType == CannonButtonType.Rotate)
        {
            cannon.RotateCannon(angleIncrement);
        }
    }
}
