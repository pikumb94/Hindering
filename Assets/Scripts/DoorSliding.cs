using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorSliding : TimeBehaviour
{

    //public float standard_speed = 1f;
    //float speed;
    public float range = 2f;
    public float smoothTime = 1f;
    float target;
    float zVelocity;
    float newPosition;
    //public float arcLength = 0;
    public GameObject door;
    public Collider colDoor;

    public bool backward = true;
    public bool time = true;
    float zPos;

    [FMODUnity.EventRef]
    public string doorOpeningSound = "";
    [FMODUnity.EventRef]
    public string doorClosingSound = "";
    private bool canPlay = false;



    void Start()
    {
        base.Start();
        zPos = door.transform.position.z;
        target = zPos + range;
    }

    // void Update()
    // {
    //   if(time && backward)
    //   {
    //       if(door.transform.position.z > zPos + maxMove)
    //       {
    //         rbDoor.velocity = Vector3.zero;
    //       }else
    //       {
    //         rbDoor.velocity = new Vector3(0, 0, standard_speed);
    //       }
    //   }else if(time)
    //   {
    //     if(door.transform.position.z < zPos)
    //     {
    //       rbDoor.velocity = Vector3.zero;
    //     }else
    //     {
    //       rbDoor.velocity = new Vector3(0, 0, -standard_speed);
    //     }
    //
    //   }
    // }
    //
    // protected override void swapTime()
    // {
    //
    //   if(time)
    //   {
    //     rbDoor.velocity = Vector3.zero;
    //     time = false;
    //   }else
    //   {
    //     time = true;
    //   }
    // }

    void Update()
    {
        if (time && !backward)
        {
            if (door.transform.position.z < target)
            {
                colDoor.isTrigger = false;
                Move(false);
            }
        }
        else if (time && backward)
        {
            if (door.transform.position.z > zPos)
            {
                Move(true);
            }
            else if(door.transform.position.z == zPos)
            {
                colDoor.isTrigger = false;
            }
        }
        /*
         if(time && !backward)
         {
           if(arcLength < 1)
           {
             colDoor.isTrigger = false;
             Move(false);
           }else
           {
             arcLength = 1;
           }
         }else if(time && backward)
         {
           if(arcLength > 0.01f)
           {
             Move(true);
           }else
           {
             door.transform.position = new Vector3(door.transform.position.x, door.transform.position.y, zPos);
             arcLength = 0;
             colDoor.isTrigger = false;
           }
         } */
        //
        // if(time && backward && arcLength < 1)
        // {
        //   colDoor.isTrigger = true;
        //   Move(true);
        // }else if(time && !(backward) && arcLength > 0)
        // {
        //   Move(false);
        //   if(arcLength <= 0)
        //   {
        //     colDoor.isTrigger = false;
        //   }
        // }
    }

    protected override void swapTime()
    {

      if(time)
      {
        time = false;
      }else
      {
        time = true;
      }
    }

    public void Move(bool backward){


        if (backward == false)
        {
            newPosition = Mathf.SmoothDamp(door.transform.position.z, target, ref zVelocity, smoothTime);
            door.transform.position = new Vector3(door.transform.position.x, door.transform.position.y, newPosition);
        }
        else
        {
            newPosition = Mathf.SmoothDamp(door.transform.position.z, zPos, ref zVelocity, smoothTime);
            door.transform.position = new Vector3(door.transform.position.x, door.transform.position.y, newPosition);
        }
    
        /*if (backward == false)
      {
            speed = standard_speed;
      }else
      {
        speed = -standard_speed;
      }


        Debug.Log("arcLength = "+ arcLength);
        Debug.Log("position = " + door.transform.position.z);
        if (range != 0)
      {
        door.transform.Translate(Vector3.forward * Time.deltaTime * speed, Space.World);
        arcLength = Mathf.Abs(door.transform.position.z - zPos) / Mathf.Abs(range);
      }else
      {
        arcLength += Time.deltaTime * speed;
      }*/
    }
  }
