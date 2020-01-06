using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallOfDeath_Switch : MonoBehaviour
{
    public ParticleSystem particleSystem;
    public Collider collider;
    private Material m;

    private void Start()
    {
        //m=particleSystem.GetComponent<ParticleSystemRenderer>().trailMaterial;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            
            if (other.transform.position.x < transform.position.x) {
                
                collider.enabled = false;
                var emission = particleSystem.emission;
                emission.rateOverTime = 0f;
                m = particleSystem.GetComponent<ParticleSystemRenderer>().trailMaterial;
                StartCoroutine("FadeOn");
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
                m = particleSystem.GetComponent<ParticleSystemRenderer>().trailMaterial;
                StartCoroutine("FadeOff");
            }
        }
    }

    IEnumerator FadeOn()
    {
        Color c;
        for (float ft = 1f; ft >= 0f; ft -= 0.1f)
        {
            c = m.color;
            c.a = ft;
            m.color = c;
            yield return new WaitForSeconds(.1f);
        }
    }

    IEnumerator FadeOff()
    {
        Color c;
        for (float ft = 0; ft <= 1; ft += 0.1f)
        {
            c = m.color;
            c.a = ft;
            m.color = c;
            yield return new WaitForSeconds(.1f);
        }
    }
}
