using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class Display : MonoBehaviour
{
    public Cannon cannon;

    public TMP_Text ballMassText;
    public TMP_Text airTimeText;
    public TMP_Text cannonAngleText;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        ballMassText.text = Convert.ToString(Mathf.RoundToInt(cannon.ballMass * 10.0f) / 10.0f) + " KG";
        airTimeText.text = Convert.ToString(Mathf.RoundToInt(cannon.currentAirTime * 100.0f) / 100.0f);
        cannonAngleText.text = Convert.ToString(Mathf.RoundToInt(cannon.transform.localEulerAngles.x * 100.0f) / 100.0f);
    }
}
