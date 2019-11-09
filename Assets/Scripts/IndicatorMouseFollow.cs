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
        //metto in "plane" il piano parallelo alla telecamera e passante per il character
        //o meglio il piano formato dal vettore con direzione  -Vector3.forward (0,0,-1) e il origine transform.forward(posizione del player) 
        plane = new Plane(-Vector3.forward, transform.position);
    }
    // Update is called once per frame
    void Update()
    {
        //crea un raggio con origine la posizione del mouse nella scena
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        
            if(plane.Raycast (ray, out distanceToPlane))
        {
            
            mousePos=ray.GetPoint(distanceToPlane);
        }

        dst = mousePos - transform.position;
        //ruoto il disco con la pallina fino a allinearlo col mouse
        transform.up = dst;
  

    }

    public Vector3 getDst()
    {
        return dst;
    }
}
