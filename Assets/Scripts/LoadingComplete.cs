using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadingComplete : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }
    private bool isFirstUpdate=true;
    // Update is called once per frame
    void Update()
    {
        if (isFirstUpdate == true)
        {
            isFirstUpdate = false;
            SceneLoader.LoadTargetScene();
        }
    }
}
