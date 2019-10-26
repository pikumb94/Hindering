using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generator : MonoBehaviour
{
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
            GameObject cube= ObjectPoolingManager.Instance.GetObject("Cube");
            cube.transform.position = new Vector2(0,5);
            yield return new WaitForSeconds(3f);
        }
    }
}
