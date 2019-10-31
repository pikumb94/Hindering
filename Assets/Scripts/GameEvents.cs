using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEvents : MonoBehaviour
{public static GameEvents current;
    public event Action onTimeChange;
    public event Action onAddForceAll;
    public void TimeChange()
    {
        if (onTimeChange != null)
        {
            onTimeChange();
        }
    }
    private void Awake()
    {
        current = this;
    }
    public void AddForceAll()
    {
        if(onAddForceAll!= null)
        {
            onAddForceAll();
        }
    }
}
