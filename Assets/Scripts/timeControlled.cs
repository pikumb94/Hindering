using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeControlled : MonoBehaviour
{
    //questa variabile è molto importante! ->> settata a true rende un oggetto soggetto alla fisica quando il tempo scorre e immobile quando il tempo si ferma
    //                                     ->> settata a false da il comportamento opposto
    public bool ActiveOnTime=true;

    private Rigidbody rb;
    //in order to save the velocity when siwitch to Kinematic we store a private Vector3
    private Vector3 velocity;
    // in order to save the forces added in stop mode i create a Vector3
    private Vector3 BaricentricforceToApply;
    private Vector3 PointForceToApply;
    private Vector3 PointWhereApply;
    private Vector3 defaultForce = new Vector3(0, 600, 0);
    // Start is called before the first frame update
    void Start()
    {
        velocity = new Vector3();
        if (gameObject.tag == "Player")
        {
            GameEvents.current.onTimeChange += switchBehaviour;
            setComponents(ActiveOnTime);
        }
        else
        {
            BaricentricforceToApply = new Vector3();
            PointForceToApply = new Vector3();
            PointWhereApply = transform.position;
            //dico al sistema che quando viene chiamato "timeChange" io devo eseguire switchKinematic ecc..
            GameEvents.current.onTimeChange += switchKinematic;
            GameEvents.current.onAddForceAll += addForce;

            rb = GetComponent<Rigidbody>();

            //setto il rigidbody in base a ActiveOnTime
            setKinematic(ActiveOnTime);
        }
    }

    private void switchBehaviour()
    {
        if (gameObject.GetComponent<PlayerMovement_CC>().enabled==true)
        {
            stopPlayer();
        }
        else
        {
            startPlayer();
        }

    }
    private void stopPlayer()
    {
        PlayerMovement_CC playerMovement = gameObject.GetComponent<PlayerMovement_CC>();
        playerMovement.enabled = false;
        ForceLineApplication forceApplication = GetComponentInChildren<ForceLineApplication>();
        forceApplication.enabled = false;
    }
    private void startPlayer()
    {
        PlayerMovement_CC playerMovement = gameObject.GetComponent<PlayerMovement_CC>();
        playerMovement.enabled = true;
        ForceLineApplication forceApplication = GetComponentInChildren<ForceLineApplication>();
        forceApplication.enabled = true;
    }

    private void setComponents(bool activeOnTime)
    {
        if (activeOnTime == false)
        {
            stopPlayer();
        }
        else
        {
            startPlayer();
        }

    }



    public void addBaricentricForce(IndicatorMouseFollow mouseScript,float magnitude,LineParameters lParams)
    {
        if (rb.isKinematic)
        {
            LineRenderer lineRenderer;

            lineRenderer = transform.gameObject.GetComponent<LineRenderer>();
            if (lineRenderer)
            {
                updateForceLine(transform.position, lineRenderer, mouseScript,lParams);
            }
            else
            {
                lineRenderer = transform.gameObject.AddComponent<LineRenderer>();

                lineRenderer.material = lParams.material;
                lineRenderer.useWorldSpace = lParams.useWorldSpace;
                lineRenderer.startWidth = lParams.startWidth;
                lineRenderer.endWidth = lParams.endWidth;
                lineRenderer.startColor = lParams.startColor;
                lineRenderer.endColor = lParams.endColor;

                lineRenderer.SetPosition(0, transform.position);
                lineRenderer.SetPosition(1, transform.position + mouseScript.getDst().normalized);
            }

            BaricentricforceToApply += mouseScript.getDst().normalized * magnitude;
        }
        else
        {
            //FORBIDDEN

            //  rb.AddForce(defaultForce);
        }
       
    }

    private void updateForceLine(Vector3 hitPoint, LineRenderer line,IndicatorMouseFollow mouseScript, LineParameters lParams)
    {

        Vector3 startPos = line.GetPosition(0);
        Vector3 endPos = line.GetPosition(1);

        line.SetPosition(0, (hitPoint + startPos) / 2);
        line.SetPosition(1, (hitPoint + startPos) / 2 + ((endPos - startPos).magnitude + lParams.increaseFactor) * mouseScript.getDst().normalized);

    }

    private void setKinematic(bool activeOnTime)
    {
        if (activeOnTime==true)
        {
            
                rb.isKinematic = !TimeHandler.Instance.time;
        }
        else
        {
            rb.isKinematic = TimeHandler.Instance.time;
        }
    }
    public void addForce()
    {
       
    }
    private void switchKinematic()
    {
        if (rb.isKinematic == true)
        {   //il tempo riprendere a scorrere..
            //reimposto il corpo a Dynamic e gli applico la velocità che aveva prima
            //poi gli aggiungo le forze applicate in stop mode e azzero BaricentricforceToApply
            rb.isKinematic = false;
           rb.velocity = velocity;


            rb.AddForceAtPosition(PointForceToApply,PointWhereApply,ForceMode.Impulse);
            rb.AddForceAtPosition(BaricentricforceToApply,transform.position,ForceMode.Impulse);

            BaricentricforceToApply = new Vector3(0, 0, 0);
            PointForceToApply = new Vector3(0, 0, 0);
            PointWhereApply = transform.position;
        }
        else
        {   //il tempo si ferma..
            //salvo la velocità e cambio in kinematic
            velocity = rb.velocity;
            rb.isKinematic = true;
        }
    }
    //funzione che Aggiunge una forza al punto di contatto
  public   void addPointForce(IndicatorMouseFollow mouseScript,LineParameters lParams,float hitRadius,RaycastHit hit,float magnitude )
    {
                
            LineRenderer lineRenderer;
        PointForceToApply = mouseScript.getDst().normalized * magnitude;
        PointWhereApply = hit.point;
        // hit.rigidbody.AddForceAtPosition(mouseScript.getDst().normalized * forceMagnitude, hit.point, ForceMode.Impulse);
            lineRenderer = hit.transform.gameObject.GetComponent<LineRenderer>();
            if (lineRenderer)
            {
                updateForceLine(hit.point, lineRenderer,mouseScript,lParams);
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
    // Update is called once per frame
    void Update()
    {
        
    }
}
