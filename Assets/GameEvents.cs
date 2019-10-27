using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEvents : MonoBehaviour
{public static GameEvents current;
    public event Action onTimeStop;

    public void TimeStop()
    {
        if (onTimeStop != null)
        {
            onTimeStop();
        }
    }
        private void Awake()
    {
        current = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
