using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HellsGate : MonoBehaviour
{
    [SerializeField]
    private Respawner respawner;
    private List<GameObject> _itemsToRespawn;

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
       for(int i = 0; i<_itemsToRespawn.Count; i++)
        {
            if(other.name == _itemsToRespawn[i].name)
            {
                other.transform.position = respawner.GetSpawnPoints()[i];
                other.transform.rotation = respawner.GetSpawnAngles()[i];
            }
        }
        other.GetComponent<Rigidbody>().velocity = Vector3.zero;
        other.GetComponent<Rigidbody>().angularDrag = 0f;
    }
}
