using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowDoorMovement : MonoBehaviour
{
    public Transform doorToFollow;
    public bool isOneTimeOpened=false;
    private float initPos;

    [FMODUnity.EventRef]
    public string openSound = "";
    bool isOpen = false;

    private void Start()
    {
        initPos = transform.position.x;
    }
    void Update()
    {
        if (!isOneTimeOpened || transform.localPosition.x<1.95f)
        {
            transform.localPosition = new Vector3(doorToFollow.localPosition.x, transform.localPosition.y, transform.localPosition.z);
            Debug.Log("MUOVO");

        }
        else
        {
            Debug.Log("Non muovo");
            
            if(!isOpen)
            {
                FMODUnity.RuntimeManager.PlayOneShot(openSound);
                isOpen = true;
            }
        }
    }
}
