using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public GameObject Cube;



    // public Screen screen;

    // private GameObject _player;


    void Awake()
    {
        ObjectPoolingManager.Instance.CreatePool(Cube, 100, 200);


    }

    void Start()
    {

        FirstLevel();
    }
    // Update is called once per frame
    void Update()
    {

    }

    public void FirstLevel()
    {
        /*if (_player == null)
        {
            _player = Instantiate(Player, Vector3.zero, Quaternion.identity);
            _player.SetActive(true);
            GameObject go = ObjectPoolingManager.Instance.GetObject("Ufo");
            go.transform.position = new Vector2(5, 5);
            //            go.transform.position = screen.Random();
        }*/
    }

}