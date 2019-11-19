using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement_CC : MonoBehaviour
{
    public CharacterController controller;


    public float speed = 12f;                       //Speed of the player: is a multiplier that can be used to tune player velocity when is gorunded
    public float gravity = -9.81f;                  //Gravity that affects the player when is not grounded
    public float jumpHeight = 3f;                   //How many units the player jumps

    //Walk selectivly on to some grounds (wrt their layer). Reintroduce if something similar is needed
    //public Transform groundCheck;
    //public float groundDistance = 0.4f;
    //public LayerMask groundMask;

    Vector3 velocity;
    bool isGrounded;

    public float decreaseRateFlying = 0.5f;         //Velocity that slow downs the player movement when he/she is in the air
    public float increaseRateGrounded = 0.1f;       //Velocity to go from "initialFallenSpeed" to the final desired velocity when player is grounded
    public float initialFallenSpeed = 0.25f;        //Initial velocity of the player as soon as he/she touches the grounds and moves
    public float slopeLimitGrounded = 45.0f;        //Use this to tune slope limit instead of the CC slope field

    private float interpFall = 0f;
    private float interpFly = 0f;


    private void Start()
    {
        controller = gameObject.GetComponent<CharacterController>();
    }
    // Update is called once per frame
    void Update()
    {
       
        transform.position = new Vector3 (transform.position.x, transform.position.y,0);

        isGrounded = controller.isGrounded; //Physics.CheckSphere(groundCheck.position, groundDistance, groundMask); //controller.isGrounded;//

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
            controller.slopeLimit = slopeLimitGrounded;
            velocity.y = -8f;
        }

        float x = Input.GetAxis("Horizontal");

        float velMagnitude = x * speed * Time.deltaTime;

        if (isGrounded)
            controller.Move(transform.right * Mathf.Lerp(initialFallenSpeed * velMagnitude, velMagnitude, interpFall));
        else
            controller.Move(transform.right * Mathf.Lerp(velMagnitude, 0, interpFly));

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            controller.slopeLimit = 90f;
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

    }

    
}
