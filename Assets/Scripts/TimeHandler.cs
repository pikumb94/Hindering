using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeHandler : Singleton<TimeHandler>
{
    public bool time = true;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("t"))
        {
            timeSwitch();
        }
    }

    void timeSwitch()
    {
        if (time == true)
        {
            time = false;
        }
        else
        {
            time = true;


        }
        GameEvents.current.TimeChange();





    }
}
