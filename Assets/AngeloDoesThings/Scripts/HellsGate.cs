using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HellsGate : MonoBehaviour
{
    public bool respawnsPlayer = true;

    
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("collided with " + other.name);

        if (other.tag == "Player")
        {
            if (respawnsPlayer)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        }

        else other.gameObject.GetComponent<Respawnable>().Respawn();
    }

    
}

