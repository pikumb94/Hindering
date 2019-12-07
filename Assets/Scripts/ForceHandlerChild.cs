using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceHandlerChild : ForceHandler
{

  public Rigidbody rbParent;

  // Start is called before the first frame update
  protected override void Start()
  {
      //inizializzo tutti i valori
      base.Start();
      rb = rbParent;
      BaricentricforceToApply = new Vector3();
      PointForceToApply = new Vector3();
      PointWhereApply = transform.position;
  }

  public override void Apply()
  {//applico prima la forza nel punto e poi quella nel baricentro
      //rb.AddForceAtPosition(PointForceToApply, PointWhereApply, ForceMode.Impulse);
      rb.AddForceAtPosition(BaricentricforceToApply, transform.position, fm);
      //Debug.Log(transform.name +" "+ BaricentricforceToApply.x+" "+ BaricentricforceToApply.y + " " + BaricentricforceToApply.z + " " + transform.position.x + " " + transform.position.y + " " + transform.position.z);
      //resetto tutti i parametri e tolgo il lineRenderer
      BaricentricforceToApply = new Vector3(0, 0, 0);
      PointForceToApply = new Vector3(0, 0, 0);
      PointWhereApply = transform.position;
      Destroy(gameObject.GetComponent<LineRenderer>());
  }
}
