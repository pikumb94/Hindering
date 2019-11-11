using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField]
    private float _time2Live = 3f;
    void OnEnable()
    {
        StartCoroutine("BulletLifeCoroutine");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator BulletLifeCoroutine()
    {
        yield return new WaitForSeconds(_time2Live);
        gameObject.SetActive(false);
    }
}
