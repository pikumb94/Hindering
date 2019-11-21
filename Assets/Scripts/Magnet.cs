using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magnet : MonoBehaviour
{
  public float charge;
  public float maxForce;
  public Vector3 PointZeroRot;
  public GameObject pivot;
  public float stdMoment;
  float dis;
  float force;
  float torque;
  Vector3 disVec;
  Vector3 normDis;
  Vector3 rotation;
  Quaternion deviation;
  Vector3 rotAxis;
//  Vector3 forbiddenRot;
  Rigidbody rb;
  Rigidbody otherRb;

  void Start()
  {
    rb = GetComponent<Rigidbody>();
  }

  void OnTriggerStay(Collider other)
  {
    if(other.tag == "Metal")
    {
      disVec = other.transform.position - pivot.transform.position;
      dis = Mathf.Sqrt(Vector3.Dot(disVec, disVec));
      normDis = Vector3.Normalize(disVec);
      rotation = Vector3.Normalize(transform.rotation * PointZeroRot);
      deviation = Quaternion.FromToRotation(normDis, rotation);
      torque = Mathf.Sqrt(Quaternion.Dot(deviation, deviation));
      rotAxis = Vector3.Normalize(Vector3.Cross(rotation, normDis));
      /*forbiddenRot = Quaternion.FromToRotation(PointZeroRot, rotation) * Vector3.up;
      if(Mathf.Sqrt(Vector3.Dot(forbiddenRot - rotAxis, forbiddenRot - rotAxis)) != 0)
      {
        rotAxis = Vector3.Normalize(rotAxis - forbiddenRot);
      }*/
      otherRb = other.GetComponent<Rigidbody>();
      force = charge / (dis);
      if(force < maxForce)
      {
        rb.AddForce(normDis * force, ForceMode.Force);
        rb.AddTorque(rotAxis * torque * stdMoment);
        otherRb.AddForce(- normDis * force, ForceMode.Force);
      }else
      {
        rb.AddForce(normDis * maxForce, ForceMode.Force);
        rb.AddTorque(rotAxis * torque * stdMoment);
        otherRb.AddForce(- normDis * maxForce, ForceMode.Force);
      }
    }
  }

}
