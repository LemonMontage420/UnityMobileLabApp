using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Spring : MonoBehaviour
{
    public Transform weight;
    private Mesh springMesh;
    private Material springMat;
    private float springLength;
    public float restLength;
    private float forceActingOnSpring;
    private float springForce;
    public float springStiffness;
    private float gravity = -9.81f;
    [Range(0.0f, 10.0f)]
    public float weightMass;
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

        springMat = GetComponent<MeshRenderer>().material;

        springLength = restLength;
    }

    // Update is called once per frame
    void Update()
    {
        forceActingOnSpring = gravity * weightMass;
        springForce = ((restLength - springLength) * springStiffness) - springDamper;
        float netForce = springForce - forceActingOnSpring;
        float springAccel = netForce / springMass;
        springVel += springAccel * Time.deltaTime;
        springLength += springVel * Time.deltaTime;
        springDamper = springVel * damperStiffness;

        springMat.SetFloat("_SpringLength", springLength);

        weight.position = transform.position + (-transform.up * (springLength - 0.02f));
    }
}
