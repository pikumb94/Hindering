using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//rinominerei questo script "ForceApplication" o una cosa simile
public class ForceLineApplication : MonoBehaviour
{
    public Animator _animator;
    //non ho capito
    public float hitRadius = 1f;
    public GameObject model;
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
    public GameObject pointerGameObject;
    [Range(0, 1)]
    public float fadePointer=.5f;
    bool isPointerOnFacingDir = true;
    Material[] materials;

    [FMODUnity.EventRef]
    public string punchSound = "";

    // Start is called before the first frame update
    void Start()
    {

        coll = GetComponent<MeshCollider>();
        playerLayer = transform.parent.gameObject.layer;
        catchInput = false;
        collidingObjects = new HashSet<Collider>();
    }

    // Update is called once per frame
    void Update()
    {
        _animator.SetBool("punch", false);

        //se sto collidendo con uno o piu oggetti..
        if ((facingRight && mouseScript.getDst().normalized.x >= -0.001f) || (!facingRight && mouseScript.getDst().normalized.x <= 0.001f))
            isPointerOnFacingDir = true;
        else
            isPointerOnFacingDir = false;
        if (!TimeHandler.Instance.time) {

            pointerGameObject.SetActive(true);

            if (isPointerOnFacingDir)
            {
                pointerGameObject.GetComponent<TrailRenderer>().enabled = true;
                Color c = pointerGameObject.GetComponent<SpriteRenderer>().material.color;
                c.a = 1;
                pointerGameObject.GetComponent<SpriteRenderer>().material.color = c;
                
            }
            else
            {
                pointerGameObject.GetComponent<TrailRenderer>().enabled = false;
                Color c = pointerGameObject.GetComponent<SpriteRenderer>().material.color;
                c.a = fadePointer;
                pointerGameObject.GetComponent<SpriteRenderer>().material.color = c;
                
            }

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
                                if (!c.isTrigger)
                                {
                                    forceHandler.addBaricentricForce(mouseScript.getDst().normalized, forceMagnitude, forceMagnitudeMaxValue);
                                    _animator.SetBool("punch", true);
                                    Debug.Log("pugnato");
                                    FMODUnity.RuntimeManager.PlayOneShot(punchSound);

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
        }
        else
        {
            pointerGameObject.SetActive(false);
        }

        inputFacingDir = Math.Sign(Input.GetAxis("Horizontal"));
        if (inputFacingDir != 0)
            facingRight = (inputFacingDir > 0 ? true : false);


        if (facingRight)
        {
            //model.transform.eulerAngles = new Vector3(0, 90, 0);
            facingIndicator.localScale = new Vector3(facingIndicator.localScale.x, Math.Abs(facingIndicator.localScale.y), facingIndicator.localScale.z);
        }
        else
        {
            //model.transform.eulerAngles = new Vector3(0, -90, 0);

            facingIndicator.localScale = new Vector3(facingIndicator.localScale.x, -Math.Abs(facingIndicator.localScale.y), facingIndicator.localScale.z);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        //se l'oggeto con cui mi sono scontrato non e un player
        //NB: qui forse c'era un modo piu corretto -> da sistemare alla fine nel caso
        if (other.gameObject.layer == LayerMask.NameToLayer("IsInteractive")/*other.gameObject.layer != gameObject.layer && other.gameObject.layer != LayerMask.NameToLayer("IsTransparent")*/)
        {

            //abilito l'applicazione della forza nell 'update
            catchInput = true;
            //aggiungo l'oggetto alla lista degli oggetti che stanno collidendo
            collidingObjects.Add(other);
            //set roba visiva
            MeshRenderer test = other.gameObject.GetComponent<MeshRenderer>();
            //Debug.Log(other.gameObject.name);
            if (test != null && fadeParameterCollidingObjects != 0)
            {
                for(int i =0;i< test.materials.Length; i++)
                {
                    StandardShaderUtils.ChangeRenderMode(test.materials[i],StandardShaderUtils.BlendMode.Fade);
                    Color c = other.gameObject.GetComponent<MeshRenderer>().materials[i].color;
                    c.a = fadeParameterCollidingObjects;
                    other.gameObject.GetComponent<MeshRenderer>().materials[i].color = c;
                }
                
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("IsInteractive")/*other.gameObject.layer != gameObject.layer && other.gameObject.layer != LayerMask.NameToLayer("IsTransparent")*/)
        {
            //rimuovo l'oggetto dalla lista dei colliding
            collidingObjects.Remove(other);
            if (collidingObjects.Count == 0)
                catchInput = false;
            //tolgo roba visiva
            MeshRenderer test = other.gameObject.GetComponent<MeshRenderer>();
            //Debug.Log(other.gameObject.name);
            if (test != null && fadeParameterCollidingObjects != 0)
            {
                for (int i = 0; i < test.materials.Length; i++)
                {
                    StandardShaderUtils.ChangeRenderMode(test.materials[i], StandardShaderUtils.BlendMode.Opaque);
                    Color c = other.gameObject.GetComponent<MeshRenderer>().materials[i].color;
                    c.a = 1;
                    other.gameObject.GetComponent<MeshRenderer>().materials[i].color = c;
                }

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
