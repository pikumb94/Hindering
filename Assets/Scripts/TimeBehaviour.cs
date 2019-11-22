using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeBehaviour : MonoBehaviour
{
    protected bool ActiveOnTime = true;
    protected Rigidbody rb;
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
            setKinematic(ActiveOnTime);
        
    }
    public void setKinematic(bool activeOnTime)
    {
        if (activeOnTime == true)
        {

            rb.isKinematic = !TimeHandler.Instance.time;
        }
        else
        {
            rb.isKinematic = TimeHandler.Instance.time;
        }
    }

    //cambio il rigidbody
    public void switchKinematic()
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
    public virtual void swapTime()
    {
        switchKinematic();
    }
}
