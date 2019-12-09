using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatePlayerModel : MonoBehaviour
{

    public float turnSmoothTime = 0.2f;
    float turnSmoothVelocity;

    //public float speedSmoothTime = 0.1f;

    void Start()
    {

    }

    void Update()
    {

        // input
        Vector2 input = new Vector2(Input.GetAxisRaw("Horizontal"), 0);
        Vector2 inputDir = input.normalized;

        if(!TimeHandler.Instance.time && !TimeHandler.Instance.isMenuActive)
            Move(inputDir);


    }

    void Move(Vector2 inputDir)
    {
        if (inputDir != Vector2.zero )
        {
            float targetRotation = (inputDir.x > 0 ? 90 : -90);
            transform.eulerAngles = Vector3.up * Mathf.SmoothDampAngle(transform.eulerAngles.y, targetRotation, ref turnSmoothVelocity, GetModifiedSmoothTime(turnSmoothTime));
        }
    }


    float GetModifiedSmoothTime(float smoothTime)
    {

        return smoothTime;
    }
}

