using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallOfDeath_Switch : MonoBehaviour
{
    public ParticleSystem particleSystem;
    public Collider collider;

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            Debug.Log("Player uscito!");
            if (other.transform.position.x > transform.position.x) {
                collider.enabled = false;
                var emission = particleSystem.emission;
                emission.rateOverTime = 0f;
                Debug.Log("ALLA DESTRA");
            }
            else
            {
                collider.enabled = true;
                var emission = particleSystem.emission;
                emission.rateOverTime = 20f;
                Debug.Log("ALLA SINISTRA");
            }
        }
    }
}
