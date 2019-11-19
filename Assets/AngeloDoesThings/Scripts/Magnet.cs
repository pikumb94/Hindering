using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magnet : MonoBehaviour
{
    [SerializeField]
    private GameObject _magnetWoop;
    public float magneticForce = 10f;
    public float incrementalFore = 15f;
    public float magnitudeOnBox = 8f;
    

    // Start is called before the first frame update
    void Awake()
    {
        ObjectPoolingManager.Instance.CreatePool(_magnetWoop, 150, 600);
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            RaycastHit hit;
            int mask = (1 << 10);
            float scalingSpeed = magneticForce;


            if (Physics.Raycast(transform.GetChild(0).transform.position, transform.up, out hit, Mathf.Infinity,  mask)) //LayerMask.NameToLayer("Magnetic")))
            {
                Debug.DrawLine(hit.point, transform.GetChild(0).transform.position, Color.cyan);

                //SpawnWoop(hit);

                Vector3 playerPos = transform.parent.parent.transform.position;
                Vector3 moveTo = (hit.point - playerPos).normalized;
                Debug.Log(playerPos + " " + hit.point + " " + moveTo);

                //transform.parent.parent.GetComponent<Rigidbody>().AddForce(moveTo * magneticForce * Time.deltaTime, ForceMode.Impulse);
                transform.parent.parent.transform.position = Vector3.MoveTowards(playerPos, hit.point, scalingSpeed * Time.deltaTime);
              
                hit.collider.GetComponent<Rigidbody>().AddForce(-moveTo * magneticForce * magnitudeOnBox * Time.deltaTime, ForceMode.Impulse);
                //scalingSpeed += incrementalFore * Time.deltaTime;

            }

            //start a coroutine: wait fot tot seconds and set scalingSpeed back at magneticForce;
        }

        if (Input.GetKeyDown("1"))
        {
            transform.parent.GetChild(0).gameObject.SetActive(true);
            gameObject.SetActive(false);
        }
    }

    void SpawnWoop(RaycastHit hit)
    {
        GameObject go = ObjectPoolingManager.Instance.GetObject("MagnetWoop");
        go.transform.position = hit.point;
        go.transform.rotation = transform.rotation;
    }
}
