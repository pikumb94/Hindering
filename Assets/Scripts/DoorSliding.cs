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
    public string doorOpeningSound = "";
    [FMODUnity.EventRef]
    public string doorClosingSound = "";
    private bool canPlay = false;



    void Start()
    {
        base.Start();
        zPos = door.transform.position.z;
    }

    void Update()
    {
        if (time && backward)
        {
            

            if (door.transform.position.z > zPos + maxMove)
            {
                rbDoor.velocity = Vector3.zero;
                canPlay = true;

            }
            else
            {
                rbDoor.velocity = new Vector3(0, 0, standard_speed);

                PlaySound(doorOpeningSound);
            }
        }
        else if (time)
        {

            if (door.transform.position.z < zPos)
            {
                rbDoor.velocity = Vector3.zero;
                canPlay = true;
            }
            else
            {
                rbDoor.velocity = new Vector3(0, 0, -standard_speed);

                PlaySound(doorClosingSound);
            }

        }
    }

    protected override void swapTime()
    {

        if (time)
        {
            rbDoor.velocity = Vector3.zero;
            time = false;
        }
        else
        {
            time = true;
        }
    }

    private void PlaySound(string path)
    {
        if (canPlay)
        {
            FMODUnity.RuntimeManager.PlayOneShot(path, GetComponent<Transform>().position);
            canPlay = false;
        }

    }
}
