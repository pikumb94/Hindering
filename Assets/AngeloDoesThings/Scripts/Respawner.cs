using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawner : MonoBehaviour
{
    public List<GameObject> itemsToRespawn = new List<GameObject>();
    private List<Vector3> _spawnPoints;
    private List<Quaternion> _spawnAngles;
    
    void Start()
    {
        _spawnPoints = new List<Vector3>();
        _spawnAngles = new List<Quaternion>();
        for (int i = 0; i<itemsToRespawn.Count; i++)
        {
            _spawnPoints.Add(itemsToRespawn[i].transform.position);
            _spawnAngles.Add(itemsToRespawn[i].transform.rotation);
        }
        
       
    }
    

    public List<Vector3> GetSpawnPoints()
    {
        return _spawnPoints;
    }

    public List<Quaternion> GetSpawnAngles()
    {
        return _spawnAngles;
    }
}
