using UnityEngine;
using System.Collections;

public class CrateCollisionSound : MonoBehaviour
{

    [FMODUnity.EventRef]
    public string crateCollisionClipSoft = "";
    [FMODUnity.EventRef]
    public string crateCollisionClipHard = "";

    public AudioSource source;
    
    private float velToVol = .2F;
    private float velocityClipSplit = 10F;



    void OnCollisionEnter(Collision coll)
    {
        float hitVol = coll.relativeVelocity.magnitude * velToVol;
        if (coll.relativeVelocity.magnitude < velocityClipSplit)
            FMODUnity.RuntimeManager.PlayOneShot(crateCollisionClipSoft, transform.position);
        else
            FMODUnity.RuntimeManager.PlayOneShot(crateCollisionClipHard, transform.position);
    }

}