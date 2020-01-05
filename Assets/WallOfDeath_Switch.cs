using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallOfDeath_Switch : MonoBehaviour
{
    public ParticleSystem particleSystem;
    public Collider collider;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            
            if (other.transform.position.x < transform.position.x) {
                
                collider.enabled = false;
                var emission = particleSystem.emission;
                emission.rateOverTime = 0f;
                Debug.Log("Player entrato a SX!");
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            if (other.transform.position.x < transform.position.x)
            {
                collider.enabled = true;
                var emission = particleSystem.emission;
                emission.rateOverTime = 20f;
                Debug.Log("Player uscito a SX!");
            }
        }
    }
}
