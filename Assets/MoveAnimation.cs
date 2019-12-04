using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveAnimation : MonoBehaviour
{private Animator  _animator ;
    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        float vel = Input.GetAxis("Horizontal");
       vel= Mathf.Abs(vel);
        _animator.SetFloat("speed",vel);
        
    }
}
