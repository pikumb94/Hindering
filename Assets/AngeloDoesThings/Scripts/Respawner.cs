using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawner : MonoBehaviour
{
    public List<GameObject> boxList = new List<GameObject>();
    private List<Vector3> _spawnPoints;
    private List<Quaternion> _spawnAngles;
    // Start is called before the first frame update
    void Start()
    {
        _spawnPoints = new List<Vector3>();
        _spawnAngles = new List<Quaternion>();
        for (int i = 0; i<boxList.Count; i++)
        {
            _spawnPoints.Add(boxList[i].transform.position);
            _spawnAngles.Add(boxList[i].transform.rotation);
        }
       
    }

    // Update is called once per frame
    void Update()
    {
        
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
