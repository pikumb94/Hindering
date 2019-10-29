
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    [Header("Object for Pools")]
    public GameObject Cube;
    public bool time=true;
    [Header("Sets handling")]
    public CubeSet cubeSet;
    [Header("Object Spawner")]
    public GameObject objPrefab;
    public int HowManyTimes = 1;
    public int ObjectQuantity = 1;
    public Transform spawnPosition;

    private List<TimeObject> objectsList = new List<TimeObject>();


    void Awake()
    {

        ObjectPoolingManager.Instance.CreatePool(Cube, 100, 200);


    }

    void Start()
    {
       // forceToApply = new List<Vector3>();
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

            foreach (var go in objectsList)
            {
                go.applyForce();
                go.resetForceToApply();

            }

        }


       

    }
    IEnumerator SpawnObjects()
    {
        for (int j=0; j< HowManyTimes;j++)
        {
            for (int i = 0; i < ObjectQuantity; i++)
            {
               // forceToApply.Add(new Vector3(0,0,0));

                GameObject obj = ObjectPoolingManager.Instance.GetObject(objPrefab.name);
                Vector3 basePosition = spawnPosition.position;
                Vector3 CasualAdd = new Vector3(Random.Range(-10, 10), 20, Random.Range(-5, 5));
                obj.transform.position = basePosition + CasualAdd;
                obj.transform.rotation = Random.rotation;
             
                objectsList.Add(new TimeObject(obj));

            }

            yield return new WaitForSeconds(3f);
        }
    }
    public void FixedUpdate()
    {
        if (Input.GetKeyDown("z"))
        {
            //int count1 = 0;
           Debug.Log(objectsList.Count);
               foreach (var go in objectsList)
           {
                if (time == false) {
                  
                    go.addForceToApply(new Vector3(0,600,0));
                    go.stampForcetoApply();
                     }
                else
                {
                    // more correct!:
                    go.setForceToApply(new Vector3(0, 600, 0));
                    go.applyForce();
                    //it's ok
                   // go.getObject().GetComponent<Rigidbody>().AddForce(0, 600, 0);
                }
         }
        }

    }
    public void FirstLevel()
    {


    }

}
public class TimeObject
{
    GameObject Object;
    Vector3 forceToApply;

    public GameObject getObject()
    {
        return Object;
    }
    public TimeObject(GameObject c)
    {
        Object = c;
        forceToApply = new Vector3(0, 0, 0);
    }
    public void setForceToApply(Vector3 force)
    {
        forceToApply = force;
    }
    public void addForceToApply(Vector3 force)
    {
        forceToApply= forceToApply + force;
    }
    public void applyForce()
    {
        Object.GetComponent<Rigidbody>().AddForce(forceToApply);

    }

    public void resetForceToApply()
    {
        forceToApply = new Vector3(0, 0, 0);
    }

    public void stampForcetoApply()
    {
        Debug.Log(forceToApply.ToString());
    }
}