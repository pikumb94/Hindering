using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*
 
     IMPORTANTE!!
     QUESTO COMPONENTE SI PUO METTERE SUL PLAYER O SU UN ALTRO OGGETTO QUALSIASI PER RENDERLO SENSIBILE AI CAMBIAMENTI DEL TEMPO (LE DUE FASI)
     MA AVRA DUE COMPORTAMENTI DIFFERENTI:
     SUL PLAYER-> ABILITA E DISABILITA GLI SCRIPT DI MOVIMENTO E DI APPLICAZIONE DELLE FORZE
     SUGLI OGGETTI-> ABILITA E DISABILITA LA SPUNTA "KINEMATIK" DEL RIGIDBODY
     
     
     */
public class TimeControlled : MonoBehaviour
{
    //questa variabile è molto importante! ->> settata a true rende un oggetto soggetto alla fisica quando il tempo scorre e immobile quando il tempo si ferma
    //                                     ->> settata a false da il comportamento opposto
    public bool ActiveOnTime=true;
    private Rigidbody rb;
    //in order to save the velocity when switch to Kinematic we store a private Vector3
    private Vector3 velocity;
    // Start is called before the first frame update
    void Start()
    {
        velocity = new Vector3();
        //se sono su un player
        if (gameObject.tag == "Player")
        {
            GameEvents.current.onTimeChange += switchBehaviour;
            setComponents(ActiveOnTime);
        }
        else
        {
            //se sono un un oggetto

            //dico al sistema che quando viene chiamato "timeChange" io devo eseguire switchKinematic ecc..
            GameEvents.current.onTimeChange += switchKinematic;

            rb = GetComponent<Rigidbody>();

            //setto il rigidbody in base a ActiveOnTime
            setKinematic(ActiveOnTime);
        }
    }

    //cambio da stop a play mode e viceversa per il player
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
    //disabilito movimento e forze
    private void stopPlayer()
    {
        PlayerMovement_CC playerMovement = gameObject.GetComponent<PlayerMovement_CC>();
        playerMovement.enabled = false;
        ForceLineApplication forceApplication = GetComponentInChildren<ForceLineApplication>();
        forceApplication.enabled = false;
    }
    //abilito movimento e forze
    private void startPlayer()
    {
        PlayerMovement_CC playerMovement = gameObject.GetComponent<PlayerMovement_CC>();
        playerMovement.enabled = true;
        ForceLineApplication forceApplication = GetComponentInChildren<ForceLineApplication>();
        forceApplication.enabled = true;
    }
    //inizializzo il comportamento in base alla variabile activeOnTime
    private void setComponents(bool activeOnTime)
    {
        if (activeOnTime != TimeHandler.Instance.time)
        {
            stopPlayer();
        }
        else
        {
            startPlayer();
        }

    }



    
    
    //inizializzo kinematic in base alla variabile activeontime
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
    
    //cambio il rigidbody
    private void switchKinematic()
    {
        if (rb.isKinematic == true)
        {   //il tempo riprendere a scorrere..
            //reimposto il corpo a Dynamic e gli applico la velocità che aveva prima
            rb.isKinematic = false;
           rb.velocity = velocity;

            //applco le forze
            gameObject.GetComponent<ForceHandler>().Apply();
        }
        else
        {   //il tempo si ferma..
            //salvo la velocità e cambio in kinematic
            velocity = rb.velocity;
            rb.isKinematic = true;
        }
    }
    //funzione che Aggiunge una forza al punto di contatto
    // Update is called once per frame
    void Update()
    {
        
    }
}
