using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagnetWoop : MonoBehaviour
{
    [SerializeField]
    private float _speed = 10f;
    private GameObject _woopDestination;
    // Start is called before the first frame update
    void Start()
    {
        _woopDestination = GameObject.Find("Player_CC");
        if(_woopDestination == null)
        {
            Debug.LogError("mannaggialamaiala");
        }
        _woopDestination = _woopDestination.transform.GetChild(1).transform.GetChild(0).gameObject;
    }

    // Update is called once per frame
    void Update()
    { 
        transform.position = Vector3.MoveTowards(transform.position, _woopDestination.transform.position, _speed * Time.deltaTime);
        if (transform.position == _woopDestination.transform.position)
            gameObject.SetActive(false);
    }
}
