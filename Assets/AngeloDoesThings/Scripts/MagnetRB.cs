using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagnetRB : MonoBehaviour
{
    [SerializeField]
    private GameObject _magnetWoop;
    [SerializeField]
    private GameObject _body;
    public float magneticForce = 30f;
    public float boxForce = 10f;

    

    void Awake()
    {
        ObjectPoolingManager.Instance.CreatePool(_magnetWoop, 150, 600);
        transform.parent.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            RaycastHit hit;
            int mask = (1 << 10);


            if (Physics.Raycast((Vector2)transform.GetChild(0).transform.position, transform.up, out hit, Mathf.Infinity, mask))
            {
                Debug.DrawLine(hit.point, transform.GetChild(0).transform.position, Color.cyan);

                //SpawnWoop(hit);

                Vector3 dir = Vector3.Scale(transform.up, new Vector3(10, 1, 1));

                //force moving player
                _body.GetComponent<Rigidbody>().AddForce(dir * magneticForce * Time.deltaTime, ForceMode.Impulse);

                if (_body.GetComponent<Rigidbody>() == null) Debug.LogError("lamadonnatroia");
                else Debug.Log("non oggi, jesoo");

                //force moving box w/out start stop (to be REMOVED in final version)
                //hit.collider.GetComponent<Rigidbody>().AddForce(-transform.up * boxForce * Time.deltaTime, ForceMode.Impulse);

                //force moving box
                hit.collider.GetComponent<ForceHandler>().addBaricentricForce(-transform.up, boxForce*Time.deltaTime, 100);
            }

        }


        if (Input.GetKeyDown("1"))
        {
            transform.parent.parent.GetChild(1).gameObject.SetActive(true);
            transform.parent.gameObject.SetActive(false);
        }
    }

    void SpawnWoop(RaycastHit hit)
    {
        GameObject go = ObjectPoolingManager.Instance.GetObject("MagnetWoop");
        go.transform.position = hit.point;
        go.transform.rotation = transform.rotation;
    }
}
