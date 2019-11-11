using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magnet : MonoBehaviour
{
    [SerializeField]
    private GameObject _magnetWoop;
    // Start is called before the first frame update
    void Awake()
    {
        ObjectPoolingManager.Instance.CreatePool(_magnetWoop, 150, 600);
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;

            if(Physics.Raycast(transform.GetChild(0).transform.position, transform.up, out hit))
            {
                Debug.DrawLine(hit.point, transform.GetChild(0).transform.position, Color.cyan);
                GameObject go = ObjectPoolingManager.Instance.GetObject("MagnetWoop");
                go.transform.position = hit.point;
                go.transform.rotation = transform.rotation;
            }
        }

        if (Input.GetKeyDown("1"))
        {
            transform.parent.GetChild(0).gameObject.SetActive(true);
            gameObject.SetActive(false);
        }
    }
}
