using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeverSpringObject : MonoBehaviour
{
    public SpringObject so;
    public GameObject stick;
    public GameObject pivot;
    public bool on = false;
    public float angleOn = 45f;
    public float angleOff = 135f;
    float angle = 135f;
    float angleSpeed = 200f;


    void Start()
    {

    }

    void Update()
    {
      if (Input.GetKeyDown("f"))
      {
          on = true;
          so.forward = on;
          so.backward = !( on );
      }
      if (Input.GetKeyDown("b"))
      {
          on = false;
          so.forward = on;
          so.backward = !(on);
      }
      if(on && angle > 45f)
      {
        stick.transform.RotateAround(pivot.transform.position, Vector3.left, Time.deltaTime * angleSpeed);
        angle -= Time.deltaTime * angleSpeed;
      }
      if(!(on) && angle < 135f)
      {
        stick.transform.RotateAround(pivot.transform.position, Vector3.right, Time.deltaTime * angleSpeed);
        angle += Time.deltaTime * angleSpeed;
      }
    }
}
