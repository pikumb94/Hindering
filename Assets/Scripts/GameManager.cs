﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    [Header("Object for Pools")]
    public GameObject Cube;
    public bool time=true;
    [Header("Sets handling")]
    public CubeSet cubeSet;
    private List<Vector3> forceToApply;
    [Header("Obj Generator")]
    public GameObject objPrefab;
    public int times = 1;
    public int quantity = 1;
    public Transform spawnPosition;
    private List<GameObject> cubeList = new List<GameObject>();

    // public Screen screen;

    // private GameObject _player;


    void Awake()
    {

        ObjectPoolingManager.Instance.CreatePool(Cube, 100, 200);


    }

    void Start()
    {
        forceToApply = new List<Vector3>();
        FirstLevel();
    }
    private void addCubeToList()
    {

    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("g"))
        {
            StartCoroutine("SpawnObjects");
        }
        if (Input.GetKeyDown("t")){
            timeSwitch();        }
    }

    void timeSwitch()
    {
        GameEvents.current.TimeChange();
        if (time == true)
        {
            time = false;
        }
        else
        {
            time = true;
            int count = 0;

            foreach (var go in cubeList)
            {
                go.GetComponent<Rigidbody>().AddForce(forceToApply[count]);
                forceToApply[count] = new Vector3(0, 0, 0);
                count++;

            }

        }


       

    }
    IEnumerator SpawnObjects()
    {
        for (int j=0; j< times;j++)
        {
            for (int i = 0; i < quantity; i++)
            {
                forceToApply.Add(new Vector3(0,0,0));
                GameObject obj = ObjectPoolingManager.Instance.GetObject(objPrefab.name);
                Vector3 basePosition = spawnPosition.position;
                Vector3 CasualAdd = new Vector3(Random.Range(-10, 10), 20, Random.Range(-5, 5));

                obj.transform.position = basePosition + CasualAdd;
                obj.transform.rotation = Random.rotation;
                // GameEvents.current.CubeCreation(obj);
                if (objPrefab.name == "Cube")
                {
                    cubeList.Add(obj);
                }
            }

            yield return new WaitForSeconds(3f);
        }
    }
    public void FixedUpdate()
    {
        if (Input.GetKeyDown("z"))
        {
            int count1 = 0;
            Debug.Log(cubeList.Count);
               foreach (var go in cubeList)
           {
                if (time == false) {
                    forceToApply[count1] = forceToApply[count1] + new Vector3(0, 600, 0);
                    count1++;
                     }
                else
                {
                    go.GetComponent<Rigidbody>().AddForce(0, 600, 0);
                }
         }
        }

    }
    public void FirstLevel()
    {


    }

}