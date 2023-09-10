using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Spring : MonoBehaviour
{
    public bool onScale;
    public Transform weight;
    private Mesh springMesh;
    private Material springMat;
    private MeshRenderer springRenderer;
    private float springLength;
    public float restLength;
    private float forceActingOnSpring;
    private float springForce;
    public float springStiffness;
    private float gravity = -9.81f;
    private float weightMass;
    private float springMass = 1.0f;
    private float springVel;
    private float springDamper;
    public float damperStiffness;
    // Start is called before the first frame update
    void Awake()
    {
        //Make Sure The Spring Is Always Rendered As It Sometimes Disappears When Scaling In The Vertex Shader (Vertices Become Out Of The Mesh Bounds)
        springMesh = GetComponent<MeshFilter>().mesh;
        springMesh.bounds = new Bounds(Vector3.zero, new Vector3(100.0f, 100.0f, 100.0f));
        springRenderer = GetComponent<MeshRenderer>();
        springMat = springRenderer.material;

        springLength = restLength;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(onScale)
        {
            SpringOnScale();
        }
        else
        {
            SpringOffScale();
        }

        springMat.SetFloat("_SpringLength", springLength);
    }

    void SpringOnScale()
    {
        if(!springRenderer.enabled)
        {
            springRenderer.enabled = true;
        }

        if(weight != null)
        {
            weightMass = weight.GetComponent<Rigidbody>().mass;
        }
        else
        {
            weightMass = 0.0f;
        }

        forceActingOnSpring = gravity * weightMass;
        springForce = ((restLength - springLength) * springStiffness) - springDamper;
        float netForce = springForce - forceActingOnSpring;
        float springAccel = netForce / springMass;
        springVel += springAccel * Time.fixedDeltaTime;
        springLength += springVel * Time.fixedDeltaTime;
        springDamper = springVel * damperStiffness;

        if(weight != null)
        {
            weight.position = transform.position + (-transform.up * (springLength - 0.02f));
        }
    }

    void SpringOffScale()
    {
        if(springRenderer.enabled)
        {
            springRenderer.enabled = false;
        }
        springLength = restLength;
    }
}
