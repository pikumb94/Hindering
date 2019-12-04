using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HellsGate : MonoBehaviour
{
    [SerializeField]
    private Respawner respawner;
    private List<GameObject> _boxList;

    // Start is called before the first frame update
    void Start()
    {
        _boxList = respawner.boxList;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {        
       for(int i = 0; i<_boxList.Count; i++)
        {
            if(other.name == _boxList[i].name)
            {
                other.transform.position = respawner.GetSpawnPoints()[i];
                other.transform.rotation = respawner.GetSpawnAngles()[i];
            }
        }
        other.GetComponent<Rigidbody>().velocity = Vector3.zero;
        other.GetComponent<Rigidbody>().angularDrag = 0f;
    }
}
