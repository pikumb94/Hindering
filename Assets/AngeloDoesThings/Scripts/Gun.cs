using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField]
    private GameObject _bullet;
    [SerializeField]
    private float _force = 200f;
    private void Awake()
    {
        ObjectPoolingManager.Instance.CreatePool(_bullet, 75, 200);
    }

    // Start is called before the first frame update
    void Start()
    {
        gameObject.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            GameObject go = ObjectPoolingManager.Instance.GetObject("Bullet");
            go.transform.position = transform.GetChild(0).transform.position;
            go.transform.rotation = transform.rotation;
            go.transform.GetComponent<Rigidbody>().AddForce(transform.up * _force * Time.deltaTime, ForceMode.Impulse);
        }

        if (Input.GetKeyDown("2"))
        {
            transform.parent.GetChild(1).gameObject.SetActive(true);
            gameObject.SetActive(false);
        }
        
    }
}
