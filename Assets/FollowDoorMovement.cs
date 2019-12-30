using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowDoorMovement : MonoBehaviour
{
    public Transform doorToFollow;
    public bool isOneTimeOpened=false;
    private float initPos;

    private void Start()
    {
        initPos = transform.position.x;
    }
    void Update()
    {
        if (isOneTimeOpened) { 
            if (initPos - transform.position.x >= 1.9f)
                return;
            else
                transform.localPosition = new Vector3(doorToFollow.localPosition.x, transform.localPosition.y, transform.localPosition.z);
        }
        else
        transform.localPosition = new Vector3(doorToFollow.localPosition.x, transform.localPosition.y, transform.localPosition.z);
    }
}
