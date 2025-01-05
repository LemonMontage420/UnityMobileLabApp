using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class Blackboard : MonoBehaviour
{
    public enum ChartType{Table, Slope};
    public ChartType chartType;

    //TABLE CHART
    [Header("Table Chart")]
    public TMP_Text[] tableFields;
    [HideInInspector] public bool[] tableValuesVisibility;
    [HideInInspector] public float[] tableRecordedValues;

    //SLOPE CHART
    [Header("Slope Chart")]
    public Material mat;
    private Vector4 pointXValues;
    private Vector4 pointYValues;
    private Vector4 pointVisibilityVec;
    public bool[] pointVisibility = new bool[4];
    [Range(0.0f, 0.8f)]
    public float point1Value;
    [Range(0.0f, 0.8f)]
    public float point2Value;
    [Range(0.0f, 0.8f)]
    public float point3Value;
    [Range(0.0f, 0.8f)]
    public float point4Value;

    public bool lineVisibility;

    private float[] x;
    private float[] y;

    // Start is called before the first frame update
    void Start()
    {
        if(chartType == ChartType.Table)
        {
            InitializeTableChart();
        }
        if(chartType == ChartType.Slope)
        {
            InitializeSlopeChart();   
        }
    }

    void InitializeTableChart()
    {
        for (int i = 0; i < tableFields.Length; i++)
        {
            tableFields[i].gameObject.SetActive(false);
        }
        tableValuesVisibility = new bool[tableFields.Length];
        tableRecordedValues = new float[tableFields.Length];
    }

    void InitializeSlopeChart()
    {
        x = new float[4];
        y = new float[4];
        x[0] = MapRangeClamped(1.0f, 0.0f, 10.0f, 0.0f, 0.8f) * 1.833333f;
        x[1] = MapRangeClamped(2.5f, 0.0f, 10.0f, 0.0f, 0.8f) * 1.833333f;
        x[2] = MapRangeClamped(5.0f, 0.0f, 10.0f, 0.0f, 0.8f) * 1.833333f;
        x[3] = MapRangeClamped(10.0f, 0.0f, 10.0f, 0.0f, 0.8f) * 1.833333f;

        pointXValues = new Vector4(4.9f, 7.35f, 11.0f, 18.35f);
        for (int i = 0; i < pointVisibility.Length; i++)
        {
            pointVisibility[i] = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(chartType == ChartType.Table)
        {
            CalculateTableChart();
        }
        if(chartType == ChartType.Slope)
        {
            CalculateSlopeChart();
        }
    }

    void CalculateTableChart()
    {

    }

    public void ProcessTableInputs(TMP_InputField field)
    {
        float value = (float)Mathf.RoundToInt(float.Parse(field.text) * 100.0f) / 100.0f;
        field.text = value.ToString();
        
        for (int i = 0; i < tableValuesVisibility.Length; i++)
        {
            if(field.transform.name == i.ToString())
            {
                tableValuesVisibility[i] = true;
                tableRecordedValues[i] = value;
                tableFields[i].gameObject.SetActive(true);
                tableFields[i].text = value.ToString();
            }   
        }
    }

    void CalculateSlopeChart()
    {
        pointYValues = new Vector4(MapRangeClamped(point1Value, 0.0f, 0.8f, 2.5f, 10.65f), MapRangeClamped(point2Value, 0.0f, 0.8f, 2.5f, 10.65f), MapRangeClamped(point3Value, 0.0f, 0.8f, 2.5f, 10.65f), MapRangeClamped(point4Value, 0.0f, 0.8f, 2.5f, 10.65f));

        mat.SetVector("_PointCoords12", new Vector4(pointXValues.x, pointYValues.x, pointXValues.y, pointYValues.y));
        mat.SetVector("_PointCoords34", new Vector4(pointXValues.z, pointYValues.z, pointXValues.w, pointYValues.w));

        if(pointVisibility[0])
        {
            pointVisibilityVec.x = 1.0f;
        }
        else
        {
            pointVisibilityVec.x = 0.0f;
        }
        if(pointVisibility[1])
        {
            pointVisibilityVec.y = 1.0f;
        }
        else
        {
            pointVisibilityVec.y = 0.0f;
        }
        if(pointVisibility[2])
        {
            pointVisibilityVec.z = 1.0f;
        }
        else
        {
            pointVisibilityVec.z = 0.0f;
        }
        if(pointVisibility[3])
        {
            pointVisibilityVec.w = 1.0f;
        }
        else
        {
            pointVisibilityVec.w = 0.0f;
        }
        mat.SetVector("_PointVisibility", pointVisibilityVec);

        CalculateLineOfBestFit();
    }

    void CalculateLineOfBestFit()
    {
        //Update The Vlues For The Slope-Intercept Calculation
        y[0] = point1Value;
        y[1] = point2Value;
        y[2] = point3Value;
        y[3] = point4Value;

        //Set The Intercept Of The Line
        mat.SetVector("_LineCoords", new Vector2(0.85f, MapRangeClamped(intercept(x, y), 0.0f, 0.8f, 0.9f, 0.575f)));

        //Calculate And Set The Angle Of The Line By Getting The Arctangent Of The Slope
        float lineAngle = slope(x, y);
        lineAngle = Mathf.Atan(lineAngle) * Mathf.Rad2Deg;
        mat.SetFloat("_LineAngle", lineAngle);

        //Set The Visibility Of The Line Based Off The Visibility Of The Points
        if(pointVisibility[0] && pointVisibility[1] && pointVisibility[2] && pointVisibility[3])
        {
            lineVisibility = true;
        }
        else
        {
            lineVisibility = false;
        }
        if(lineVisibility)
        {
            mat.SetFloat("_LineVisibility", 1.0f);
        }
        else
        {
            mat.SetFloat("_LineVisibility", 0.0f);
        }
    }

    //Controlled Via The UI Input Fields Of Each Of The Points
    public void RemapYValues(TMP_InputField field)
    {
        float value = float.Parse(field.text);
        value = Mathf.Clamp(value, 0.0f, 0.8f);
        field.text = value.ToString();
        if(field.transform.name == "1KG")
        {
            point1Value = value;
            pointVisibility[0] = true;
        }
        if(field.transform.name == "2.5KG")
        {
            point2Value = value;
            pointVisibility[1] = true;
        }
        if(field.transform.name == "5KG")
        {
            point3Value = value;
            pointVisibility[2] = true;
        }
        if(field.transform.name == "10KG")
        {
            point4Value = value;
            pointVisibility[3] = true;
        }
    }



//---------------------Math------------------------------
    float slope(float[] x, float[] y) 
    {
        float slope = 0.0f;
        slope = correlation(x, y) / totalSumOfSquares(x);
        return slope;
    }

    float intercept(float [] x, float [] y) 
    {
        float intercept = 0.0f;
        float xave = average(x);
        float yave = average(y);
        intercept = yave - slope(x, y) * xave;
        return intercept;
    }

    float correlation(float[] x, float[] y) 
    {
        float correlation = 0.0f;
        for (int i = 0; i < x.Length; ++i) 
        {
            correlation += x[i] * y[i];
        }
        float xave = average(x);
        float yave = average(y);
        correlation -= xave * yave * x.Length;
        return correlation;
    }

    float totalSumOfSquares(float[] values)
    {
        float sumOfSquares = 0.0f;

        float valueAverage = average(values);
        for (int i = 0; i < values.Length; i++)
        {
            sumOfSquares += Mathf.Pow(values[i] - valueAverage, 2.0f);
        }
        return sumOfSquares;
    }

    float average(float[] values) 
    {
        float average = 0.0f;
        float total = 0.0f;
        for (int i = 0; i < values.Length; i++)
        {
            total += values[i];
        }
        average = total / (float)values.Length;
        return average;
    }

    float MapRangeClamped(float value, float inRangeA, float inRangeB, float outRangeA, float outRangeB)
    {
            float result = Mathf.Lerp(outRangeA, outRangeB, Mathf.InverseLerp(inRangeA, inRangeB, value));
            return (result);
    }

    bool withinMargin(float value1, float value2, float margin)
    {
        bool inMargin = false;
        float difference = Mathf.Abs(value1 - value2);
        if(difference < margin)
        {
            inMargin = true;
        }
        return(inMargin);
    }
}
