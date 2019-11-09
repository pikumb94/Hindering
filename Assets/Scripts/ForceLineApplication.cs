using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceLineApplication : MonoBehaviour
{

    LineRenderer lineRenderer;
    public float hitRadius = 1f;
    public float forceMagnitude = 10f;
    CapsuleCollider coll;
    private int playerLayer;
    private IndicatorMouseFollow mouseScript;
    HashSet<Collider> collidingObjects; 
    private bool catchInput;
    public LineParameters lParams;
    [Range(0,1)]
    public float fadeParameterCollidingObjects;

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
        //se sto collidendo con uno o piu oggetti..
        if (catchInput)
        {
            //se premo il mouse sinistro
            if (Input.GetMouseButtonDown(0))
            {
                //per ogni oggetto con cui sto collidendo
                foreach (Collider c in collidingObjects)
                    //applico la forza nel punto in cui collide la pallina rossa
                    addPointForce(c);
            }

            if (Input.GetMouseButtonDown(1))
            {
                //per ogni oggetto con cui collido
                foreach (Collider c in collidingObjects)
                    //aggiungo una forza al baricentro
                    addBarycentricForce(c);
            }
        }
        

    }

    private void OnTriggerEnter(Collider other)
    {
        //se l'oggeto con cui mi sono scontrato non e un player 
        //NB: qui forse c'era un modo un filo piu corretto -> da sistemare alla fine nel caso
        if (other.gameObject.layer != playerLayer)
        {
            //abilito l'applicazione della forza nell 'update
            catchInput = true;
            //aggiungo l'oggetto alla lista degli oggetti che stanno collidendo
            collidingObjects.Add(other);

            //set roba visiva
            Color c = other.gameObject.GetComponent <MeshRenderer>().material.color;
            c.a = fadeParameterCollidingObjects;
            other.gameObject.GetComponent<MeshRenderer>().material.color = c;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer != playerLayer)
        {
            //rimuovo l'oggetto dalla lista dei colliding
            collidingObjects.Remove(other);
            //tolgo roba visiva
            Color c = other.gameObject.GetComponent<MeshRenderer>().material.color;
            c.a = 1;
            other.gameObject.GetComponent<MeshRenderer>().material.color = c;
            //se non ho piu oggetti in lista disabilito l'applicazione delle forze
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
    //funzione che Aggiunge una forza al punto di contatto
    void addPointForce(Collider other)
    {
        RaycastHit hit;

        if (Physics.Raycast(new Vector3(transform.position.x, transform.position.y, 0), mouseScript.getDst(), out hit, hitRadius))
        {

           // hit.rigidbody.AddForceAtPosition(mouseScript.getDst().normalized * forceMagnitude, hit.point, ForceMode.Impulse);
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
    //funzione che aggiunge una forza al baricentro e crea la linea visiva
    void addBarycentricForce(Collider other)
    {
       // other.attachedRigidbody.AddForceAtPosition(mouseScript.getDst().normalized * forceMagnitude, other.transform.position, ForceMode.Impulse);
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
