using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lever : MonoBehaviour
{
    public SpringObject so;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
      if (Input.GetKeyDown("f"))
      {
          so.forward = true;
          so.backward = false;
      }
      if (Input.GetKeyDown("b"))
      {
          so.backward = true;
          so.forward = false;
      }

    }
}
