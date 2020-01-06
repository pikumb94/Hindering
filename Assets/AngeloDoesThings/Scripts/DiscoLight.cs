using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiscoLight : TimeBehaviour
{
    public float delay; // amount of time before toggling
    public float minIntensity; // the minimum intensity of the light
    public float maxIntensity; // the maximum intensity of the light
    public bool startAtMin;
    public bool time=true;

    private Color[] colors =
    {
        Color.red,
        Color.green,
        Color.blue,
        Color.cyan,
        Color.white,
        Color.magenta,
    };

    // variable to hold a reference to the Light component on this gameObject
    private Light myLight;

    // variable to hold the amount of time that has passed
    private float timeElapsed;

    // this function is called once by Unity the moment the game starts
    private void Awake()
    {
        myLight = transform.GetComponent<Light>();
    }

    private void Start()
    {
        base.Start();
    }

    // this function is called every frame by Unity
    private void Update()
    {
        if(time)
        {
            // add the amount of time that has passed since last frame
            timeElapsed += Time.deltaTime;

                // if the amount of time passed is greater than or equal to the delay
            if (timeElapsed >= delay)
            {
                // reset the time elapsed
                timeElapsed = 0;
                // toggle the light
                ToggleLight();
            }
        }
         
        
    }

    // function to toggle between two intensities
    public void ToggleLight()
    {
        // if the variable is not empty
        if (myLight != null)
        {
            // if the intensity is currently the minimum, switch to max
            if (myLight.intensity == minIntensity)
            {
                myLight.intensity = maxIntensity;
                myLight.color = colors[Random.Range(0, colors.Length - 1)];
            }
            // if the intensity is currently the max, switch to min
            else if (myLight.intensity == maxIntensity)
            {
                myLight.intensity = minIntensity;
            }
        }
    }

    protected override void swapTime()
    {
        if (time) time = false;

        else time = true;
    }
}


