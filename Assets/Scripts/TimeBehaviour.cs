using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeBehaviour : MonoBehaviour
{
    protected Rigidbody rb;
    protected ForceHandler fh;
    //in order to save the velocity when switch to Kinematic we store a private Vector3
    protected Vector3 velocity;
    protected void Start()
    {
        velocity = new Vector3();

            //se sono un un oggetto

            //dico al sistema che quando viene chiamato "timeChange" io devo eseguire switchKinematic ecc..
           GameEvents.current.onTimeChange += swapTime;

            rb = GetComponent<Rigidbody>();

            //setto il rigidbody in base a ActiveOnTime
            if(!TimeHandler.Instance.time)
            {
              swapTime();
            }
    }


    //cambio il rigidbody
    protected void switchKinematic()
    {
        if (rb.isKinematic == true)
        {   //il tempo riprendere a scorrere..
            //reimposto il corpo a Dynamic e gli applico la velocità che aveva prima
            rb.isKinematic = false;
            rb.velocity = velocity;

            //applco le forze
            fh = gameObject.GetComponent<ForceHandler>();
            if(fh != null)
            {
              fh.Apply();
            }
        }
        else
        {   //il tempo si ferma..
            //salvo la velocità e cambio in kinematic
            velocity = rb.velocity;
            rb.isKinematic = true;
        }
    }
    protected virtual void swapTime()
    {
        switchKinematic();
    }
}
