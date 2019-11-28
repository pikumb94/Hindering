using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeverTrajObject : MonoBehaviour
{
    public TrajObject so;
    public GameObject pivot;
    public GameObject sphere;
    float pivotHeight;
    public bool on = false;


    void Start()
    {
      pivotHeight = pivot.transform.position.y;
    }

    void Update()
    {
      if(sphere.transform.position.y - pivotHeight > 0)
      {
        on = false;
        so.backward = !(on);
      }else
      {
        on = true;
        so.backward = !(on);
      }
    }
}
