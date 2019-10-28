using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generator : Singleton<Generator>

{public GameObject objPrefab;
    public int quantity=1;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("g"))
        {
            StartCoroutine("Fade");
        }
    }
    IEnumerator Fade()
    {
        for (float ft = 1f; ft >= 0; ft -= 0.1f)
        {
            for (int i =0; i< quantity;i++)
            {
                GameObject obj = ObjectPoolingManager.Instance.GetObject(objPrefab.name);
                Vector3 GeneratorPosition = transform.position;
                Vector3 CasualAdd =new Vector3(Random.Range(-5, 5),20, Random.Range(-5, 5));

                obj.transform.position = GeneratorPosition + CasualAdd;
                obj.transform.rotation = Random.rotation;
            }
           
            yield return new WaitForSeconds(3f);
        }
    }
}
