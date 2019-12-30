using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeIntensityLight : MonoBehaviour
{
    private Light light;
    private bool isPlayerInside=false;
    float newIntensity;
    float smoothTime = 1f;
    float velocity1;
    float velocity2;

    private void Start()
    {
        light = GetComponent<Light>();
        newIntensity = 0f;
    }

    private void Update()
    {
        if (isPlayerInside)
        {
            newIntensity = Mathf.SmoothDamp(light.intensity, 10f, ref velocity1, smoothTime);
        }
        else
        {
            newIntensity = Mathf.SmoothDamp(light.intensity, 0f, ref velocity2, smoothTime);
        }
        light.intensity = newIntensity;
    }

    private void OnTriggerEnter(Collider other)
    {
        isPlayerInside = true;
    }

    private void OnTriggerExit(Collider collision)
    {
        isPlayerInside = false;
    }
}
