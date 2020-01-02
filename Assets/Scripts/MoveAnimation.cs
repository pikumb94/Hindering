﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveAnimation : MonoBehaviour
{
    [FMODUnity.EventRef]
    public string walkingEvent = "";
    [FMODUnity.EventRef]
    public string footstepEvent = "";
    [FMODUnity.EventRef]
    public string landingEvent = "";

    private Animator  _animator ;
    bool m_isAxisInUse;
    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        float vel = Input.GetAxis("Horizontal");
      // vel= Mathf.Abs(vel);
       // _animator.SetFloat("speed",vel);

       /* if (Input.GetAxisRaw("Fire1") != 0 )
        {
            if (m_isAxisInUse == false)
            {
                //per ogni oggetto con cui collido

                _animator.SetBool("punch", true);
                m_isAxisInUse = true;
            }
        }
        if (Input.GetAxisRaw("Fire1") == 0)
        {
            _animator.SetBool("punch", false);
            m_isAxisInUse = false;
        }*/
    }

    public void playWalking()
    {
        FMODUnity.RuntimeManager.PlayOneShot(walkingEvent);
    }

    public void playFootstep()
    {
        FMODUnity.RuntimeManager.PlayOneShot(footstepEvent);
    }

    public void playLanding()
    {
        FMODUnity.RuntimeManager.PlayOneShot(landingEvent);
    }

    
}