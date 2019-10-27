using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cube : MonoBehaviour
{
    public CubeSet cubeSet;
    private Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {

        GameEvents.current.onTimeStop += switchToKinematic;
//        GameEvents.current.onTimeStop += ;

        rb = GetComponent<Rigidbody>();
        if (GameManager.Instance.time == true)
        {
            rb.isKinematic = false;
        }
        else
        {
            rb.isKinematic = true;
        }
    }
    private void switchToKinematic()
    {
        if (rb.isKinematic == true)
        {
            rb.isKinematic = false;
        }
        else
        {
            rb.isKinematic = true;
        }
    }
    private void OnEnable()
    {
cubeSet.Add(this);        
    }
    private void OnDisable()
    {
        cubeSet.Remove(this);
    }
    // Update is called once per frame
    void Update()
    {
        /*if (Input.GetKeyDown("c"))
        {
            rb.AddForce(0,600,0);
        }*/
    }
}
