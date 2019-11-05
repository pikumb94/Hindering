using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class timeControlled : MonoBehaviour
{

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
        forceToApply = new Vector3();
        GameEvents.current.onTimeChange += switchKinematic;
        GameEvents.current.onAddForceAll += addForce;
        rb = GetComponent<Rigidbody>();
        if (TimeHandler.Instance.time == true)
        {
            rb.isKinematic = false;
        }
        else
        {
            rb.isKinematic = true;
        }
    }
    private void addForce()
    {
        if (rb.isKinematic)
        {
            forceToApply += defaultForce;
        }
        else
        {
            rb.AddForce(defaultForce);
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
