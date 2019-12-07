using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorSliding : TimeBehaviour
{

    public float standard_speed = 1f;
    public float maxMove = 2f;
    public GameObject door;
    Rigidbody rbDoor;

    public bool backward = true;
    public bool time = true;
    float zPos;

    void Start()
    {
      base.Start();
      zPos = door.transform.position.z;
      rbDoor = door.GetComponent<Rigidbody>();
    }

    void Update()
    {
      Debug.Log(backward);
      if(time && backward)
      {
          if(door.transform.position.z > zPos + maxMove)
          {
            rbDoor.velocity = Vector3.zero;
          }else
          {
            rbDoor.velocity = new Vector3(0, 0, standard_speed);
          }
      }else if(time)
      {
        if(door.transform.position.z < zPos)
        {
          rbDoor.velocity = Vector3.zero;
        }else
        {
          rbDoor.velocity = new Vector3(0, 0, -standard_speed);
        }

      }
    }

    protected override void swapTime()
    {

      if(time)
      {
        rbDoor.velocity = Vector3.zero;
        time = false;
      }else
      {
        time = true;
      }
    }
}
