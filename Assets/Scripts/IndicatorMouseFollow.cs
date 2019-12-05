using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IndicatorMouseFollow : MonoBehaviour
{
    public Transform playerPosition;
    Vector3 mousePos;
    float distanceToPlane;
    Vector3 dst;
    Plane plane;
    float snapAngleRad;
    float prevMousMagn;

    bool usingJoystick = false;


    void Start()
    {
        //metto in "plane" il piano parallelo alla telecamera e passante per il character
        //o meglio il piano formato dal vettore con direzione  -Vector3.forward (0,0,-1) e il origine transform.forward(posizione del player) 
        plane = new Plane(-Vector3.forward, playerPosition.position + new Vector3(0, 0.5f, -0.6f));

    }
    // Update is called once per frame
    void Update()
    {
        //transform.position = playerPosition.position + new Vector3(0, 0.5f, -0.6f);//SERVE SE è FUORI IL RB
        //crea un raggio con origine la posizione del mouse nella scena
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (plane.Raycast(ray, out distanceToPlane))
        {

            mousePos = ray.GetPoint(distanceToPlane);
        }

        float rightXAxisJoy = Input.GetAxisRaw("Mouse X");
        float rightYAxisJoy = Input.GetAxisRaw("Mouse Y");

        if (Mathf.Abs(rightXAxisJoy) > 0.2f || Mathf.Abs(rightYAxisJoy) > 0.2f) { 
            dst = new Vector3(rightXAxisJoy, -rightYAxisJoy, 0);
            usingJoystick = true;
        }
        else{
            if(!usingJoystick)
                dst = mousePos - transform.position;
        }
        //ruoto il disco con la pallina fino a allinearlo col mouse

        //Hold shift to snap the angles
        if (Input.GetButton("Fire3"))
        {
            snapAngleRad = snapRadiants(Mathf.Atan2(dst.y, dst.x));
            dst = new Vector3(Mathf.Cos(snapAngleRad), Mathf.Sin(snapAngleRad),0);
        }
        //Debug.Log(dst + " "+ transform.forward);

        transform.right = dst;
        //transform.rotation = Quaternion.LookRotation(dst, transform.forward);
        transform.eulerAngles = new Vector3(0, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z);

        if (prevMousMagn != mousePos.sqrMagnitude)
            usingJoystick = false;

        prevMousMagn = mousePos.sqrMagnitude;

    }

    public Vector3 getDst()
    {
        return dst;
    }

    float snapRadiants(float currAngleRad)
    {
        bool isNeg = (Mathf.Sign(currAngleRad)<0?true:false);
        float res=Mathf.Abs(currAngleRad);

        
        switch (res)
        {
            case float n when (n <= Mathf.PI/8f):
                res = 0f;
                break;

            case float n when (n > Mathf.PI / 8f && n <=3f* Mathf.PI / 8f):
                res = Mathf.PI/4f;
                break;

            case float n when (n > 3f*Mathf.PI / 8f && n <= 5f * Mathf.PI / 8f):
                res = Mathf.PI / 2f;
                break;

            case float n when (n > 5f * Mathf.PI / 8f && n <= 7f * Mathf.PI / 8f):
                res = 3f*Mathf.PI / 4f;
                break;
            case float n when (n > 7f * Mathf.PI / 8f && n <= Mathf.PI):
                res = Mathf.PI;
                break;
        }
        return (isNeg?-1f* res:res);
    }
}
