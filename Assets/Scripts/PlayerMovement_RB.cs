using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement_RB : MonoBehaviour
{
    private Rigidbody rb;
    Vector3 playerDirection;
    bool isFalling = false;
    bool isGrounded = true;
    float inputX;
    float magnitudeXMov;
    private CapsuleCollider coll;
    LayerMask layerMask;



    public float speed = 12f;                       //Speed of the player: is a multiplier that can be used to tune player velocity when is gorunded

    public float forceJumpMagnitude = 1f;
    public ForceMode forceType;
    public float heightStair=0f;
    public float magnStairRaycast = .6f;



    private void Start()
    {
        layerMask = 1 << gameObject.layer;
        layerMask = ~layerMask;
        rb = gameObject.GetComponent<Rigidbody>();
        coll = gameObject.GetComponent<CapsuleCollider>();
    }
    // Update is called once per frame
    void Update()
    {
        if (Physics.CheckSphere(transform.position + -Vector3.up * coll.height/2, (3f/4f)*coll.radius, layerMask))
            isGrounded = true;
        else
            isGrounded = false;

        if (!isGrounded) //is jumping
        {
            if (rb.velocity.y >= 0)
                isFalling = false;
            else
                isFalling = true;
        }
        Debug.Log (isGrounded);
        if (Input.GetButtonDown("Jump") && isGrounded)
            playerJump(forceJumpMagnitude, forceType);



        /*
        float x = Input.GetAxis("Horizontal");
        playerDirection = Vector3.right * x * Time.deltaTime;
        //rb.MovePosition(transform.position + playerDirection);
        rb.velocity = playerDirection * speed;
        Debug.Log(rb.velocity);*/
    }

    private void FixedUpdate()
    {
        inputX = Input.GetAxis("Horizontal");
        magnitudeXMov = speed * inputX * Time.fixedDeltaTime;
        rb.velocity = new Vector3(magnitudeXMov, rb.velocity.y, 0);
        //Debug.Log(rb.velocity);

        

        Vector3 horizontalMove = new Vector3(Mathf.Sign(rb.velocity.x),0,0);
        RaycastHit hit;
        RaycastHit hitStair;
        // The raycast fix the intial step and slope problem
        
        Debug.DrawLine(transform.position + Vector3.up * heightStair, transform.position+ horizontalMove * magnStairRaycast + Vector3.up* heightStair, Color.red);
        if(Physics.Raycast(transform.position + Vector3.up * heightStair, horizontalMove, out hitStair, magnStairRaycast, layerMask))
        {
            Debug.Log("RAYCASTHITS");
            if (rb.SweepTest(horizontalMove, out hit, 0.05f))
            {
                Debug.Log("PLAYER COLLIDES!");
                // If so, stop the movement
                rb.velocity = new Vector3(0, rb.velocity.y, 0);
            }
        }
        
       
    }

    private void playerJump(float fJM, ForceMode type)
    {
        rb.AddForce(transform.up * fJM, type);
    }
}
