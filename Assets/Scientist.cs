using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scientist : TimeBehaviour
{private Animator _animator;
    private bool unlock = false;
    // Start is called before the first frame update
    private new void Start()
    {
        //base.Start();
        GameEvents.current.onTimeChange += swapTime;

        _animator = GetComponent<Animator>();
        if (TimeHandler.Instance.time == true)
        {
            _animator.enabled = true;
        }
        else
        {
            _animator.enabled = false;
            //retardedStopAnimator();
        }

    }
    private  IEnumerator retardedStopAnimator()
    {
       yield return new  WaitForSeconds(1);
        _animator.enabled = false;


    }
    // Update is called once per frame
    void Update()
    {
        
       
        
    }
    override protected void swapTime()
    {

        if (_animator.enabled)
        {
            _animator.enabled = false;
        }
        else
        {
            _animator.enabled = true;

        }
    }
}
