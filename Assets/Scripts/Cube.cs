using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cube : MonoBehaviour
{
    public CubeSet cubeSet;
    

    // Start is called before the first frame update
    void Start()
    {
      
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
       
    }
}
