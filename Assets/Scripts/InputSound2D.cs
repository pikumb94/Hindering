using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputSound2D : MonoBehaviour
{
    //bool fromStartToPause = true;

    [FMODUnity.EventRef]
    public string playToStopSound = "";
    [FMODUnity.EventRef]
    public string stopToPlaySound = "";
    [FMODUnity.EventRef]
    public string stoppedSnapshotRef = "";
    FMOD.Studio.EventInstance stoppedSnapshot;

    private float initialTimeValue;

    // Start is called before the first frame update
    void Start()
    {
        if (TimeHandler.Instance.time)
        {
            initialTimeValue = 0f;
        }
        else
        {
            initialTimeValue = 100f;
        }

        stoppedSnapshot = FMODUnity.RuntimeManager.CreateInstance(stoppedSnapshotRef);
        stoppedSnapshot.setParameterByName("SnapshotIntensity", initialTimeValue);
        stoppedSnapshot.start();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire2"))
        {
            if (TimeHandler.Instance.time)
            {
                FMODUnity.RuntimeManager.PlayOneShot(stopToPlaySound);
                stoppedSnapshot.setParameterByName("SnapshotIntensity", (float)0f);
            }
            else
            {
                FMODUnity.RuntimeManager.PlayOneShot(playToStopSound);
                stoppedSnapshot.setParameterByName("SnapshotIntensity", (float)100f);

            }

            
        }

    }
}
