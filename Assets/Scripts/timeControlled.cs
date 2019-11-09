using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class timeControlled : MonoBehaviour
{
    //questa variabile è molto importante! ->> settata a true rende un oggetto soggetto alla fisica quando il tempo scorre e immobile quando il tempo si ferma
    //                                     ->> settata a false da il comportamento opposto
    public bool ActiveOnTime=true;
    
    private Rigidbody rb;
    //in order to save the velocity when siwitch to Kinematic we store a private Vector3
    private Vector3 velocity;
    // in order to save the forces added in stop mode i create a Vector3
    private Vector3 forceToApply;
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
            forceToApply = new Vector3();

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
        if (rb.isKinematic)
        {
           forceToApply += defaultForce;
        }
        else
        {
            //FORBIDDEN

          //  rb.AddForce(defaultForce);
        }
    }
    private void switchKinematic()
    {
        if (rb.isKinematic == true)
        {   //il tempo riprendere a scorrere..
            //reimposto il corpo a Dynamic e gli applico la velocità che aveva prima
            //poi gli aggiungo le forze applicate in stop mode e azzero forceToApply
            rb.isKinematic = false;
           rb.velocity = velocity;
            rb.AddForce(forceToApply);
            forceToApply = new Vector3(0, 0, 0);
        }
        else
        {   //il tempo si ferma..
            //salvo la velocità e cambio in kinematic
            velocity = rb.velocity;
            rb.isKinematic = true;
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
