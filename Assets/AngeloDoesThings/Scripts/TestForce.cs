using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestForce : MonoBehaviour
{

    private GameObject _body;
    public float testForce = 50f;
    // Start is called before the first frame update
    void Start()
    {
        _body = transform.parent.parent.GetChild(0).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButton(1)) _body.GetComponent<Rigidbody>().AddForce(Vector3.right * testForce * Time.deltaTime, ForceMode.Impulse);
    }
}
