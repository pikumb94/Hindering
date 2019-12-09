using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawnable : MonoBehaviour
{

    private Vector3 _initialPos;
    private Quaternion _initialRot;
    void Start()
    {
        _initialPos = transform.position;
        _initialRot = transform.rotation;
    }

    public void Respawn()
    {
        transform.position = _initialPos;
        transform.rotation = _initialRot;
        gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
        gameObject.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
    }
}

