using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClockController : TimeBehaviour
{
    public bool time;
    float speed;
    public Clock clock;
    void Start()
    {
      base.Start();
      speed = clock.clockSpeed;
      Debug.Log(clock.clockSpeed);
    }

    protected override void swapTime()
    {
      if(time)
      {
        time = false;
        speed = clock.clockSpeed;
        clock.clockSpeed = 0;
      }else
      {
        time = true;
        clock.clockSpeed = speed;
      }
    }
}
