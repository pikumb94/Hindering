﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewMagnetRB : MonoBehaviour
{
    
    //public GameObject _magnetWoop;
    private GameObject _body;
    public float magneticForce = 30f;
    public float boxForce = 10f;
    public float maxBoxForce = 100f;


    void Awake()
    {
        _body = transform.parent.parent.gameObject;
        //ObjectPoolingManager.Instance.CreatePool(_magnetWoop, 150, 600);
        transform.parent.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            RaycastHit hit;
            int mask = (1 << 10);


            if (Physics.Raycast((Vector2)transform.GetChild(0).transform.position, transform.up, out hit, Mathf.Infinity, mask))
            {
                Debug.DrawLine(hit.point, transform.GetChild(0).transform.position, Color.cyan);

                //SpawnWoop(hit);

                Vector3 dir = Vector3.Scale(transform.up, new Vector3(10, 1, 1));

                //force moving player
                _body.GetComponent<Rigidbody>().AddForce(dir * magneticForce * Time.deltaTime, ForceMode.Impulse);

                //force moving box
                hit.collider.GetComponent<ForceHandler>().addBaricentricForce(-transform.up, boxForce * Time.deltaTime, maxBoxForce);
            }

        }


        if (Input.GetKeyDown("1"))
        {
            GameObject punchMode = transform.parent.parent.GetChild(1).gameObject;
            punchMode.SetActive(true);
            transform.parent.gameObject.SetActive(false);
        }
    }

    void SpawnWoop(RaycastHit hit)
    {
        GameObject go = ObjectPoolingManager.Instance.GetObject("MagnetWoop");
        go.transform.position = hit.point;
        go.transform.rotation = transform.rotation;
    }

}