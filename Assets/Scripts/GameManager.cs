
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{

    [Header("Object for Pools")]
    public GameObject Cube;

    [Header("Sets handling")]
    public CubeSet cubeSet;

    //private List<TimeObject> objectsList = new List<TimeObject>();


    private void Awake()
    {
        


    }

    void Start()
    {
        // Carico tutti gli oggetti persistenti
        Debug.Log("Loading persistent managers...");
        //carico il soundManager etc

        //Carico tutte le Objects pools
        Debug.Log("Starting to build pools...");
        ObjectPoolingManager.Instance.CreatePool(Cube, 100, 200);
        Debug.Log("pooled 200 cubes...");

        //Carico la prima scena ---> IL MENU
        Debug.Log("All done... Loading Menu");
        SceneLoader.LoadMainMenu();

       // FirstLevel();
    }
   // private void addCubeToList(){}
    
    // Update is called once per frame
    void Update()
    {
      
    }

   

    public void FixedUpdate()
    {
        if (Input.GetKeyDown("z"))
        {
            //quando premo Z chiamo un evento 'addForceAll'

            GameEvents.current.AddForceAll();

     
        }

    }


    public void FirstLevel()
    {


    }

}

//useless for the moment..kept just in case
/*
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
        forceToApply = forceToApply + force;
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
}*/