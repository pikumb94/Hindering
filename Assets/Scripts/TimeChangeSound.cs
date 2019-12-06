using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeChangeSound : MonoBehaviour
{
    bool fromStartToPause = true;

    [FMODUnity.EventRef]
    public string playToStopSound = "";
    [FMODUnity.EventRef]
    public string stopToPlaySound = "";
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire2"))
        {
            if (fromStartToPause)
            {
                FMODUnity.RuntimeManager.PlayOneShot(stopToPlaySound);
                fromStartToPause = !fromStartToPause;
            }
            else
            {
                FMODUnity.RuntimeManager.PlayOneShot(playToStopSound);
                fromStartToPause = !fromStartToPause;
            }
        }
    }
}
