using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement_RB : MonoBehaviour
{
    private Rigidbody rb;
    Vector3 velocityVector;
    bool isFalling = false;
    bool isGrounded = true;
    

    public float speed = 12f;                       //Speed of the player: is a multiplier that can be used to tune player velocity when is gorunded
    public float gravity = -9.81f;                  //Gravity that affects the player when is not grounded
    public float jumpHeight = 3f;                   //How many units the player jumps

    //Walk selectivly on to some grounds (wrt their layer). Reintroduce if something similar is needed
    //public Transform groundCheck;
    //public float groundDistance = 0.4f;
    //public LayerMask groundMask;


    public float forceJumpMagnitude = 1f;
    public ForceMode forceType;
    public float decreaseRateFlying = 0.5f;         //Velocity that slow downs the player movement when he/she is in the air
    public float increaseRateGrounded = 0.1f;       //Velocity to go from "initialFallenSpeed" to the final desired velocity when player is grounded
    public float initialFallenSpeed = 0.25f;        //Initial velocity of the player as soon as he/she touches the grounds and moves
    public float slopeLimitGrounded = 45.0f;        //Use this to tune slope limit instead of the CC field

    private float interpFall = 0f;
    private float interpFly = 0f;
    

    private void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
    }
    // Update is called once per frame
    void Update()
    {
        /*
         transform.position = new Vector3 (transform.position.x, transform.position.y,0);

         if (isGrounded)
         {
             interpFall = (interpFall < 1 ? interpFall + increaseRateGrounded * Time.deltaTime : 1);
             interpFly = 0;
         }
         else
         {
             interpFall = 0;
             interpFly = (interpFly < 1 ? interpFly + decreaseRateFlying * Time.deltaTime : 1);
         }


         if (isGrounded && velocity.y < 0)
         {
             //rb.slopeLimit = slopeLimitGrounded;
             //velocity.y = -8f;
         }

         float x = Input.GetAxis("Horizontal");

         float velMagnitude = x * speed * Time.deltaTime;

         if (isGrounded)
             velocity=transform.right * Mathf.Lerp(initialFallenSpeed * velMagnitude, velMagnitude, interpFall);
         else
             velocity=transform.right * Mathf.Lerp(velMagnitude, 0, interpFly);


         if (Input.GetButtonDown("Jump") && isGrounded)
         {
             //rb.slopeLimit = 90.0f;
            // velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
         }

         //velocity.y += gravity * Time.deltaTime;
         Debug.Log(velocity);
         rb.velocity=velocity;
         */
        if (Mathf.Abs(rb.velocity.y) <= 0.001f)
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

        if (Input.GetButtonDown("Jump") && isGrounded)
            playerJump(forceJumpMagnitude, forceType);


        float x = Input.GetAxis("Horizontal");
        velocityVector = Vector3.right * x * speed * Time.deltaTime;
        rb.MovePosition(transform.position + velocityVector);
        Debug.Log(rb.velocity);
    }

    private void FixedUpdate()
    {
        // Get the velocity
        Vector3 horizontalMove = rb.velocity;
        // Don't use the vertical velocity
        horizontalMove.y = 0;
        // Calculate the approximate distance that will be traversed
        float distance = horizontalMove.magnitude * Time.fixedDeltaTime;
        // Normalize horizontalMove since it should be used to indicate direction
        horizontalMove.Normalize();
        RaycastHit hit;
        
        // Check if the body's current velocity will result in a collision
        Debug.DrawLine(transform.position, transform.position+horizontalMove*2f, Color.red);
        if (rb.SweepTest(horizontalMove, out hit, 2f))
        {
            Debug.Log("PLAYER COLLIDES!");
            // If so, stop the movement
            rb.velocity = new Vector3(0, rb.velocity.y, 0);
        }

    }

    private void playerJump(float fJM, ForceMode type)
    {
        rb.AddForce(transform.up * fJM, type);
    }
}
