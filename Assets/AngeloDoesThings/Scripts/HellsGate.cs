using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HellsGate : MonoBehaviour
{
    [SerializeField]
    private Respawner respawner;
    private List<GameObject> _itemsToRespawn;
    public bool respawnsPlayer = true;

    // Start is called before the first frame update
    void Start()
    {
        _itemsToRespawn = respawner.itemsToRespawn;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("collided with " + other.name);

        if (other.tag == "Player")
        {
            if (respawnsPlayer)
            {
                Respawn(other);
            }
        }

        else Respawn(other);
    }

    private void Respawn(Collider other)
    {
        for (int i = 0; i < _itemsToRespawn.Count; i++)
        {
            if (other.name == _itemsToRespawn[i].name)
            {
                other.transform.position = respawner.GetSpawnPoints()[i];
                other.transform.rotation = respawner.GetSpawnAngles()[i];
            }
        }
        other.GetComponent<Rigidbody>().velocity = Vector3.zero;
        other.GetComponent<Rigidbody>().angularDrag = 0f;
    }
}
