using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trajectory : MonoBehaviour
{

    public Vector2 ranges;
    public SpringObject.Types type;
    public float speed = 0;
    Vector3 initialPos;
    Vector3 finalPos;
    float center;
    float parameterA;
    float ray;


    public Vector3 InitialPos()
    {
      return initialPos;
    }
    public void SetInitialPos(Vector3 initialPos)
    {
      this.initialPos = initialPos;
    }
    public Vector3 FinalPos()
    {
      return finalPos;
    }
    public void SetFinalPos(Vector3 finalPos)
    {
      this.finalPos = finalPos;
    }


    public float Center()
    {
      return center;
    }
    public void SetCenter(float center)
    {
      this.center = center;
    }


    public float ParameterA()
    {
      return parameterA;
    }
    public void SetParameterA(float parameterA)
    {
      this.parameterA = parameterA;
    }


    public float Ray()
    {
      return ray;
    }
    public void SetRay(float ray)
    {
      this.ray = ray;
    }

}
