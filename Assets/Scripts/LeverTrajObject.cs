using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeverTrajObject : TimeBehaviour
{
    public TrajObject so;
    public GameObject stick;
    public GameObject pivot;
    public bool on = false;
    public bool time = true;
    public float angleOn = 45f;
    public float angleOff = 135f;
    float angle = 135f;
    float angleSpeed = 200f;


    void Start()
    {
      base.Start();
    }

    void Update()
    {
      if (Input.GetKeyDown("f"))
      {
          on = true;
          so.backward = !(on);
      }
      if (Input.GetKeyDown("b"))
      {
          on = false;
          so.backward = !(on);
      }
      if(time && on && angle > angleOn)
      {
        stick.transform.RotateAround(pivot.transform.position, Vector3.left, Time.deltaTime * angleSpeed);
        angle -= Time.deltaTime * angleSpeed;
      }
      if(time && !(on) && angle < angleOff)
      {
        stick.transform.RotateAround(pivot.transform.position, Vector3.right, Time.deltaTime * angleSpeed);
        angle += Time.deltaTime * angleSpeed;
      }
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
}
