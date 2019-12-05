using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seesaw : TimeBehaviour
{
    // Start is called before the first frame update
    public GameObject arm1;
    public GameObject arm2;

    void Start()
    {
      base.Start();
    }

    // Update is called once per frame
    void Update()
    {

    }

    protected override void swapTime()
    {
      if (rb.isKinematic == true)
      {   //il tempo riprendere a scorrere..
          //reimposto il corpo a Dynamic e gli applico la velocità che aveva prima
          rb.isKinematic = false;
          rb.velocity = velocity;

          //applco le forze
          arm1.GetComponent<ForceHandlerSeesaw>().Apply();
          arm2.GetComponent<ForceHandlerSeesaw>().Apply();
      }
      else
      {   //il tempo si ferma..
          //salvo la velocità e cambio in kinematic
          velocity = rb.velocity;
          rb.isKinematic = true;
      }
    }
}
