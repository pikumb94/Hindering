using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleFront : TimeBehaviour
{
    public bool time = true;
    public float startingState = 1f;
    public float sspeed = 0.5f;
    private ParticleSystem ps;

    void Start()
    {
        ps = GetComponent<ParticleSystem>();
        ps.Simulate(startingState + Random.Range(0, 1f));
        ps.Play();
        base.Start();
    }

    protected override void swapTime()
    {
        var main = ps.main;
        Debug.Log(gameObject);
        if(time){
          main.simulationSpeed = 0f;
          time = false;
        }else{
          time = true;
          main.simulationSpeed = sspeed;
        }
    }
}
