using UnityEngine;
using System.Collections;

public class CrateCollisionSound : MonoBehaviour
{

    [FMODUnity.EventRef]
    public string crateCollisionClipSoft = "";
    [FMODUnity.EventRef]
    public string crateCollisionClipHard = "";

    public string nameLayerToAvoid; 
    
    private float velToVol = .2F;
    private float velocityClipSplit = 10F;
    private float minimumThresshold = 1F;

    private void Start()
    {
        
    }

    void OnCollisionEnter(Collision coll)
    {

        float impactVelocity = coll.relativeVelocity.magnitude;
        float hitVol = impactVelocity * velToVol;

        //if(coll.gameObject.layer != LayerMask.NameToLayer(nameLayerToAvoid) && coll.rigidbody.isKinematic) {
        if(impactVelocity > minimumThresshold) {
            if (coll.relativeVelocity.magnitude < velocityClipSplit)
                FMODUnity.RuntimeManager.PlayOneShot(crateCollisionClipSoft, transform.position);
            else
                FMODUnity.RuntimeManager.PlayOneShot(crateCollisionClipHard, transform.position);
        }
            
        
        
    }

}