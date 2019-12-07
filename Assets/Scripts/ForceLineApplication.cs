using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//rinominerei questo script "ForceApplication" o una cosa simile
public class ForceLineApplication : MonoBehaviour
{
    //non ho capito
    public float hitRadius = 1f;

    //magntudine della forza
    public float forceMagnitude = 10f;
    private bool m_isAxisInUse = false;
    public float forceMagnitudeMaxValue = 100f;
    MeshCollider coll;
    private int playerLayer;
    public IndicatorMouseFollow mouseScript;
    bool facingRight = true;
    float inputFacingDir;
    HashSet<Collider> collidingObjects;
    private bool catchInput;
    public Transform facingIndicator;
   // public LineParameters lParams;
    [Range(0,1)]
    public float fadeParameterCollidingObjects;
    bool isPointerOnFacingDir = true;

    // Start is called before the first frame update
    void Start()
    {

        coll = GetComponent<MeshCollider>();
        playerLayer = transform.parent.gameObject.layer;
        //mouseScript = GetComponent<IndicatorMouseFollow>();
        catchInput = false;
        collidingObjects = new HashSet<Collider>();
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(Input.GetAxis("Fire1"));
        //se sto collidendo con uno o piu oggetti..
        if ((facingRight && mouseScript.getDst().normalized.x >= -0.001f) || (!facingRight && mouseScript.getDst().normalized.x <= 0.001f))
            isPointerOnFacingDir = true;
        else
            isPointerOnFacingDir = false;

        if (catchInput)
        {
            /*
            //se premo il mouse sinistro
            if (Input.GetMouseButtonDown(0))
            {
                //per ogni oggetto con cui sto collidendo
                foreach (Collider c in collidingObjects)
                    //applico la forza nel punto in cui collide la pallina rossa
                    addPointForce(c);
            }*/
            if (Input.GetAxisRaw("Fire1") != 0 && isPointerOnFacingDir)
            {
                if (m_isAxisInUse == false)
                {
                    //per ogni oggetto con cui collido
                    foreach (Collider c in collidingObjects)
                    {
                        //aggiungo una forza al baricentro
                        ForceHandler forceHandler = c.GetComponent<ForceHandler>();
                        if (forceHandler == null)
                        {
                            Debug.Log("non posso applicare forze a corpi senza un ForceHandler");
                        }
                        else
                        {
                          if(!c.isTrigger)
                          {
                            forceHandler.addBaricentricForce(mouseScript.getDst().normalized, forceMagnitude, forceMagnitudeMaxValue);
                          }
                        }
                    }

                    m_isAxisInUse = true;
                }
            }
            if (Input.GetAxisRaw("Fire1") == 0)
            {
                m_isAxisInUse = false;
            }

        }

        inputFacingDir = Math.Sign(Input.GetAxis("Horizontal"));
        if (inputFacingDir != 0)
            facingRight = (inputFacingDir > 0 ? true : false);

        if (facingRight)
            facingIndicator.localScale = new Vector3(facingIndicator.localScale.x, Math.Abs(facingIndicator.localScale.y), facingIndicator.localScale.z);
        else
            facingIndicator.localScale = new Vector3(facingIndicator.localScale.x, - Math.Abs(facingIndicator.localScale.y), facingIndicator.localScale.z);

    }

    private void OnTriggerEnter(Collider other)
    {
        //se l'oggeto con cui mi sono scontrato non e un player
        //NB: qui forse c'era un modo piu corretto -> da sistemare alla fine nel caso
        if (other.gameObject.layer != gameObject.layer)
        {

            //abilito l'applicazione della forza nell 'update
            catchInput = true;
            //aggiungo l'oggetto alla lista degli oggetti che stanno collidendo
            collidingObjects.Add(other);
            //set roba visiva
            Component test = other.gameObject.GetComponent<MeshRenderer>();
            Debug.Log(other.gameObject.name);
            if (test != null && fadeParameterCollidingObjects!=0)
            {
                Color c = other.gameObject.GetComponent<MeshRenderer>().material.color;

                c.a = fadeParameterCollidingObjects;
                other.gameObject.GetComponent<MeshRenderer>().material.color = c;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer != gameObject.layer)
        {
            //rimuovo l'oggetto dalla lista dei colliding
            collidingObjects.Remove(other);
            if (collidingObjects.Count == 0)
                catchInput = false;
            //tolgo roba visiva
            Component test = other.gameObject.GetComponent<MeshRenderer>();
            if (test != null && fadeParameterCollidingObjects != 0)
            {
                Color c = other.gameObject.GetComponent<MeshRenderer>().material.color;
                c.a = 1;
                other.gameObject.GetComponent<MeshRenderer>().material.color = c;
                //se non ho piu oggetti in lista disabilito l'applicazione delle forze


            }
        }
    }



    //se ho capito bene controllo con un ray cast se la pallina interseca qualcosa
    void addPointForce(Collider other)
    {
        RaycastHit hit;

        if (Physics.Raycast(new Vector3(transform.position.x, transform.position.y, 0), mouseScript.getDst(), out hit, hitRadius))
        {

            //se interseza applico la forza nel punto
            ForceHandler forceHandler = hit.transform.gameObject.GetComponent<ForceHandler>();
            if (forceHandler == null)
            {
                Debug.Log("non posso applicare forze a corpi senza un forceHandler");
            }
            else
            {
                forceHandler.addPointForce(mouseScript.getDst().normalized,hit.point, forceMagnitude);
            }



        }
    }

}
