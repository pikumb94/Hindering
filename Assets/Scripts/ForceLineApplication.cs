using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceLineApplication : MonoBehaviour
{

    LineRenderer lineRenderer;
    public float hitRadius = 1f;
    public float forceMagnitude = 1f;
    CapsuleCollider coll;
    private int playerLayer;
    private IndicatorMouseFollow mouseScript;
    HashSet<Collider> collidingObjects; 
    private bool catchInput;
    public LineParameters lParams;

    // Start is called before the first frame update
    void Start()
    {
        coll = GetComponent<CapsuleCollider>();
        playerLayer = transform.parent.gameObject.layer;
        mouseScript = GetComponent<IndicatorMouseFollow>();
        catchInput = false;
        collidingObjects = new HashSet<Collider>();
    }

    // Update is called once per frame
    void Update()
    {

        if (catchInput)
        {
            if (Input.GetMouseButtonDown(0))
            {
                foreach (Collider c in collidingObjects)
                    addPointForce(c);
            }

            if (Input.GetMouseButtonDown(1))
            {
                foreach (Collider c in collidingObjects)
                    addBarycentricForce(c);
            }
        }
        

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer != playerLayer)
        {
            catchInput = true;
            collidingObjects.Add(other);
            /* Fade out when colliding NOT WORKING!
            Color c = other.gameObject.GetComponent <MeshRenderer>().material.color;
            c.a = 0;
            other.gameObject.GetComponent<MeshRenderer>().sharedMaterial.color = c;
            */
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer != playerLayer)
        {
            
            collidingObjects.Remove(other);
            /* Fade in when get distance from the object NOT WORKING!
            Color c = other.gameObject.GetComponent<MeshRenderer>().material.color;
            c.a = 0;
            other.gameObject.GetComponent<MeshRenderer>().material.color = c;
            */
            if (collidingObjects.Count==0)
                catchInput = false;
        }
    }

    void updateForceLine(Vector3 hitPoint,LineRenderer line)
    {
        Vector3 startPos = line.GetPosition(0);
        Vector3 endPos = line.GetPosition(1);
        
        lineRenderer.SetPosition(0, (hitPoint+startPos)/2);
        lineRenderer.SetPosition(1, (hitPoint + startPos) / 2 +  ((endPos-startPos).magnitude+lParams.increaseFactor) * mouseScript.getDst().normalized);
    }

    void addPointForce(Collider other)
    {
        RaycastHit hit;

        if (Physics.Raycast(new Vector3(transform.position.x, transform.position.y, 0), mouseScript.getDst(), out hit, hitRadius))
        {

            //hit.rigidbody.AddForceAtPosition(mouseScript.getDst().normalized * forceMagnitude, hit.point, ForceMode.Impulse);
            lineRenderer = hit.transform.gameObject.GetComponent<LineRenderer>();
            if (lineRenderer)
            {
                updateForceLine(hit.point, lineRenderer);
            }
            else
            {
                lineRenderer = hit.transform.gameObject.AddComponent<LineRenderer>();

                lineRenderer.material = lParams.material;
                lineRenderer.useWorldSpace = lParams.useWorldSpace;
                lineRenderer.startWidth = lParams.startWidth;
                lineRenderer.endWidth = lParams.endWidth;
                lineRenderer.startColor = lParams.startColor;
                lineRenderer.endColor = lParams.endColor;

                lineRenderer.SetPosition(0, hit.point);
                lineRenderer.SetPosition(1, hit.point + mouseScript.getDst().normalized);
            }

        }
    }

    void addBarycentricForce(Collider other)
    {
        //other.attachedRigidbody.AddForceAtPosition(mouseScript.getDst().normalized * forceMagnitude, other.transform.position, ForceMode.Impulse);
        lineRenderer = other.transform.gameObject.GetComponent<LineRenderer>();
        if (lineRenderer)
        {
            updateForceLine(other.transform.position, lineRenderer);
        }
        else
        {
            lineRenderer = other.transform.gameObject.AddComponent<LineRenderer>();

            lineRenderer.material = lParams.material;
            lineRenderer.useWorldSpace = lParams.useWorldSpace;
            lineRenderer.startWidth = lParams.startWidth;
            lineRenderer.endWidth = lParams.endWidth;
            lineRenderer.startColor = lParams.startColor;
            lineRenderer.endColor = lParams.endColor;

            lineRenderer.SetPosition(0, other.transform.position);
            lineRenderer.SetPosition(1, other.transform.position + mouseScript.getDst().normalized);
        }
    }
}
