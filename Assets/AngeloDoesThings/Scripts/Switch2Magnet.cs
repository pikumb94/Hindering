using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch2Magnet : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("2"))
        {
            transform.parent.GetChild(2).gameObject.SetActive(true);
            gameObject.SetActive(false);
        }
    }
}
