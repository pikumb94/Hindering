using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CeilingFan : TimeBehaviour
{
    public float speed = 1f;
    public bool time;
    void Start()
    {
      base.Start();
    }

    void Update()
    {
      if(time)
      {
        transform.Rotate(0, speed * Time.deltaTime, 0);
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
