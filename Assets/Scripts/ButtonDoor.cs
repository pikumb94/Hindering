using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonDoor : TimeBehaviour
{
    public DoorSliding target;
    float pivotHeight;
    public float height;
    public float forceUp;
    public bool on = false;
    bool beforeState = false;


    void Start()
    {
      base.Start();
      pivotHeight = transform.position.y;
    }

    void Update()
    {
      rb.AddForce( new Vector3(0, forceUp, 0), ForceMode.VelocityChange );
      if(rb.velocity.y > 1f)
      {
        rb.velocity = new Vector3(0, 1f, 0);
      }
      if(transform.position.y > pivotHeight)
      {
        transform.position = new Vector3(transform.position.x, pivotHeight, transform.position.z);
      }
      if(transform.position.y < pivotHeight - height)
      {
        transform.position = new Vector3(transform.position.x, pivotHeight - height, transform.position.z);
      }
      if(transform.position.y > pivotHeight - height && transform.position.y < pivotHeight)
      {
        on = !beforeState;
        target.backward = !(on);
      }else if (transform.position.y == pivotHeight - height)
      {
        on = true;
        beforeState = on;
        target.backward = !(on);
      }else if (transform.position.y == pivotHeight)
      {
        on = false;
        beforeState = on;
        target.backward = !(on);
      }else
      {
        Debug.Log("Error with button position");
      }
    }
}
