using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateAvatar : MonoBehaviour
{
    float xInput;
    float angle;
    public float rotateSpeed;
    bool facingRight = true;
    bool facingLeft = false;
    public float thresholdFacing = 0.01f;
    
    // Start is called before the first frame update
    void Start()
    {
        transform.rotation = Quaternion.Euler(Vector3.zero); ;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(GameManager.isInPlayMode);
        if (!GameManager.isInPlayMode)
        {

            xInput = Input.GetAxis("Horizontal");
            if (Mathf.Abs(transform.rotation.eulerAngles.y) <= thresholdFacing)
                facingRight = true;
            else
                facingRight = false;

            if (Mathf.Abs(180f - transform.rotation.eulerAngles.y) <= thresholdFacing)
                facingLeft = true;
            else
                facingLeft = false;

            if (xInput > 0 && !facingRight)
            {
                angle = rotateSpeed * Time.deltaTime;
                transform.rotation *= Quaternion.AngleAxis(xInput * angle, -Vector3.up);
            }

            if (xInput < 0 && !facingLeft)
            {
                angle = rotateSpeed * Time.deltaTime;
                transform.rotation *= Quaternion.AngleAxis(xInput * angle, -Vector3.up);
            }

        }

    }
}
