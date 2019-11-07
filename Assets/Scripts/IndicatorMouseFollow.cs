using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IndicatorMouseFollow : MonoBehaviour
{

    Vector3 mousePos;
    float distanceToPlane;
    Vector3 dst;
    Plane plane;


    void Start()
    {
        plane = new Plane(-Vector3.forward, transform.position);
    }
    // Update is called once per frame
    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        
            if(plane.Raycast (ray, out distanceToPlane))
        {
            mousePos=ray.GetPoint(distanceToPlane);
        }

        dst = mousePos - transform.position;
        transform.up = dst;
  

    }

    public Vector3 getDst()
    {
        return dst;
    }
}
