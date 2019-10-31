using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    [Header("Object Spawner")]
    public GameObject objPrefab;
    public int HowManyTimes = 1;
    public int ObjectQuantity = 1;
    public Transform spawnPosition;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown("g"))
        {
            StartCoroutine("SpawnObjects");
        }

    }

    IEnumerator SpawnObjects()
    {
        for (int j = 0; j < HowManyTimes; j++)
        {
            for (int i = 0; i < ObjectQuantity; i++)
            {
                // forceToApply.Add(new Vector3(0,0,0));

                GameObject obj = ObjectPoolingManager.Instance.GetObject(objPrefab.name);
                Vector3 basePosition = spawnPosition.position;
                Vector3 CasualAdd = new Vector3(Random.Range(-10, 10), 20, Random.Range(-5, 5));
                obj.transform.position = basePosition + CasualAdd;
                obj.transform.rotation = Random.rotation;

               // objectsList.Add(new TimeObject(obj));

            }

            yield return new WaitForSeconds(3f);
        }
    }
}
