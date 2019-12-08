using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorSliding : TimeBehaviour
{

    public float standard_speed = 1f;
    public float maxMove = 2f;
    public GameObject door;
    public Rigidbody rbDoor;

    public bool backward = true;
    public bool time = true;
    float zPos;

    [FMODUnity.EventRef]
    public string doorOpeningSound =  "";
    [FMODUnity.EventRef]
    public string doorClosingSound = "";


    void Start()
    {
      base.Start();
      zPos = door.transform.position.z;
    }

    void Update()
    {
      if(time && backward)
      {
        FMODUnity.RuntimeManager.PlayOneShot(doorOpeningSound);
          if (door.transform.position.z > zPos + maxMove)
          {
            rbDoor.velocity = Vector3.zero;
            

          }else
          {
            rbDoor.velocity = new Vector3(0, 0, standard_speed);
          }
      }else if(time)
      {
        FMODUnity.RuntimeManager.PlayOneShot(doorClosingSound);
          if (door.transform.position.z < zPos)
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
