using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IndicatorMouseFollow : MonoBehaviour
{

    Vector3 mousePos;
    float distanceToPlane;
    Vector3 dst;
    Plane plane;
    public float hitRadius = 1f;
    public float forceMagnitude = 1f;
    CapsuleCollider coll;
    // Start is called before the first frame update
    void Start()
    {
        plane = new Plane(-Vector3.forward, transform.position);
        coll = GetComponent<CapsuleCollider>();
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
        if (Input.GetMouseButtonDown(0))
        {
            Debug.DrawRay(transform.position, dst, Color.green,1);
            RaycastHit hit;

            if (Physics.Raycast(new Vector3(transform.position.x, transform.position.y,0), dst, out hit, hitRadius))
            {
               
                hit.rigidbody.AddForceAtPosition(dst.normalized* forceMagnitude, hit.point, ForceMode.Impulse);
                Debug.DrawRay(new Vector3(transform.position.x, transform.position.y, 0), dst*0.5f, Color.red, 1);
            }
            Debug.Log(hit.transform.name);
        }
        
    }


}
