using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement_RB : TimeBehaviour
{
    private Animator _animator;
    Vector3 playerDirection;
    bool isFalling = false;
    bool isGrounded = true;
    float inputX;
    float magnitudeXMov;
    private CapsuleCollider coll;
    LayerMask layerMask;
    bool canPlayerMove=false;
    float stairHeightFromPlayerCenter;
    float prevXinput=0;

    Vector3 horizontalMove;
    int numRays;
    bool playerHitsWall=false;



    public float speed = 12f;                       //Speed of the player: is a multiplier that can be used to tune player velocity when is gorunded

    public float forceJumpMagnitude = 1f;
    public ForceMode forceType;
    public float heightStair=0f;
    public float magnRaycast = .55f;
    public float magnStairRaycast = .6f;
    public float raycastInterspace = .25f;
    public MeshRenderer hitCollVisibility;

    private new void Start()
    {
        base.Start();
        layerMask = 1 << gameObject.layer;
        //layerMask = 1 << LayerMask.NameToLayer("IsTrigger");
        layerMask = ~layerMask;
        rb = gameObject.GetComponent<Rigidbody>();
        coll = gameObject.GetComponent<CapsuleCollider>();

        stairHeightFromPlayerCenter = Mathf.Abs(coll.height/2 - heightStair);
        numRays = (int) ((coll.height) /raycastInterspace);
        Debug.Log(numRays);

        GameObject go = transform.GetChild(0).gameObject;
        _animator= go.GetComponent<Animator>();

    }
    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.H))
            hitCollVisibility.enabled = (hitCollVisibility.enabled ? false : true);


        if (Physics.CheckSphere(transform.position - Vector3.up * (3f / 4f) * coll.height / 2f - Vector3.up * 0.01f, coll.radius, layerMask))
        {
            isGrounded = true;
            _animator.SetBool("isGrounded", true);
        }
        else
        { isGrounded = false;
            _animator.SetBool("isGrounded", false);
        }

        if (!isGrounded) //is jumping
        {
            if (rb.velocity.y >= 0)
                isFalling = false;
            else
                isFalling = true;
        }
        if (Input.GetButtonDown("Jump") && isGrounded && canPlayerMove)
        {
            _animator.SetBool("jump", true);

            playerJump(forceJumpMagnitude, forceType);
        }
        else
        {
            _animator.SetBool("jump", false);
        }


    }

    private void FixedUpdate()
    {
        inputX = Input.GetAxis("Horizontal");
        magnitudeXMov = speed * inputX * Time.fixedDeltaTime;
        if(canPlayerMove)
            rb.velocity = new Vector3(magnitudeXMov, rb.velocity.y, 0);

        if(Mathf.Sign(inputX)!=Mathf.Sign(prevXinput) && isGrounded)//added to prevent jumping when going rapidly the opposite direction when you're on a slope
            rb.velocity = new Vector3(rb.velocity.x, 0, 0);

        /*This Raycasting is to allow the player to climb stairs and slopes while allowing a little bit of edge climbing
         when the capsule collider is smooth enough to allow it in a fast way. If it requires too much time to climb the X input is ignored and the player falls.
         Later I will try to do it in a more general way but this way should work avoiding also to get stuck on air and mid air objects.*/
        horizontalMove = new Vector3(Math.Sign(rb.velocity.x),0,0);





        RaycastHit hit;
        RaycastHit hitLow;
        RaycastHit hitCenter;
        RaycastHit hitHigh;
        RaycastHit hitMidHigh;
        RaycastHit hitMidLow;
        RaycastHit hitKnee;

        // The raycast fix the intial step and slope problem
        for(int i = 0; i < numRays/2; i++)
        {
            float deltaP = raycastInterspace * (i+1);
            float deltaN = -raycastInterspace * (i+1);




                Debug.DrawLine(transform.position  + Vector3.up * deltaP, transform.position  + Vector3.up * deltaP + horizontalMove * magnRaycast, Color.yellow);
                if ((deltaP < stairHeightFromPlayerCenter))
                    Debug.DrawLine(transform.position  + Vector3.up * deltaN, transform.position  + Vector3.up * deltaN + horizontalMove * magnRaycast, Color.yellow);
        }
        Debug.DrawLine(transform.position + Vector3.up * coll.height/2, transform.position +Vector3.up * coll.height/2 + horizontalMove * magnRaycast, Color.cyan);
        Debug.DrawLine(transform.position , transform.position + horizontalMove * magnRaycast, Color.cyan);

        for (int i = 0; i < numRays / 2; i++)
        {
            float deltaP = raycastInterspace * (i + 1);
            float deltaN = -raycastInterspace * (i + 1);




            playerHitsWall = playerHitsWall || Physics.Raycast(transform.position + Vector3.up * deltaP, horizontalMove, out hit, magnRaycast, layerMask);
            if ((deltaP < stairHeightFromPlayerCenter))
                playerHitsWall = playerHitsWall || Physics.Raycast(transform.position + Vector3.up * deltaN, horizontalMove, out hit, magnRaycast, layerMask);
        }
        playerHitsWall = playerHitsWall || Physics.Raycast(transform.position + Vector3.up * coll.height / 2, horizontalMove, out hit, magnRaycast, layerMask);
        playerHitsWall = playerHitsWall || Physics.Raycast(transform.position, horizontalMove, out hit, magnRaycast, layerMask);
        /*
        Debug.DrawLine(transform.position + Vector3.up * heightStair, transform.position+ horizontalMove * magnStairRaycast + Vector3.up* heightStair, Color.red);
        Debug.DrawLine(transform.position + Vector3.up * heightStair/2f, transform.position + horizontalMove * magnStairRaycast + Vector3.up * heightStair/2f, Color.red);
        Debug.DrawLine(transform.position - Vector3.up * heightStair / 2f, transform.position + horizontalMove * magnStairRaycast - Vector3.up * heightStair / 2f, Color.red);
        Debug.DrawLine(transform.position, transform.position + horizontalMove * magnStairRaycast, Color.red);
        */
        Debug.DrawLine(transform.position - Vector3.up * stairHeightFromPlayerCenter, transform.position + horizontalMove * magnStairRaycast - Vector3.up * stairHeightFromPlayerCenter, Color.green);
        Debug.DrawLine(transform.position - Vector3.up * coll.height/2, transform.position + horizontalMove * magnRaycast - Vector3.up * coll.height / 2, Color.blue);

        if (playerHitsWall)/*Physics.Raycast(transform.position - Vector3.up * heightStair, horizontalMove, out hitLow, magnStairRaycast, layerMask) ||
            Physics.Raycast(transform.position - Vector3.up * heightStair * 4f / 5f, horizontalMove, out hitKnee, magnStairRaycast, layerMask) ||*/
             /*Physics.Raycast(transform.position - Vector3.up * heightStair / 2f, horizontalMove, out hitMidLow, magnStairRaycast, layerMask) ||
             Physics.Raycast(transform.position, horizontalMove, out hitCenter, magnStairRaycast, layerMask) ||//scala troppo alta! allora azzero
             Physics.Raycast(transform.position + Vector3.up * heightStair / 2f, horizontalMove, out hitMidHigh, magnStairRaycast, layerMask) ||
             Physics.Raycast(transform.position + Vector3.up * heightStair, horizontalMove, out hitHigh, magnStairRaycast, layerMask))*/
        {
                Debug.Log("PLAYER HITS RAYCAST!");
                rb.velocity = new Vector3(0, rb.velocity.y, 0);
        }
        else
        {
            if (Physics.Raycast(transform.position - Vector3.up * stairHeightFromPlayerCenter, horizontalMove, out hitKnee, magnStairRaycast, layerMask) &&
                Physics.Raycast(transform.position - Vector3.up * coll.height / 2, horizontalMove, out hitLow, magnRaycast, layerMask))
            {
                //Debug.Log(hitKnee.distance + "  " + hitLow.distance);
                if(Mathf.Abs(hitKnee.distance - hitLow.distance) <= Mathf.Epsilon)
                {
                    Debug.Log("Only last two and same distance");
                    rb.velocity = new Vector3(0, rb.velocity.y, 0);
                }

            }
            /*
            if (rb.SweepTest(horizontalMove, out hit, 0.01f))
            {
                Debug.Log("PLAYER COLLIDES SWEEPTEST!");
                // If so, stop the movement
                rb.velocity = new Vector3(0, rb.velocity.y, 0);
            }
            */
        }
        prevXinput = inputX;
        playerHitsWall = false;
    }

    private void playerJump(float fJM, ForceMode type)
    {
        rb.velocity = new Vector3(rb.velocity.x,0,0);
        rb.AddForce(transform.up * fJM, type);
    }

    override protected void swapTime()
    {
        if (canPlayerMove)
            canPlayerMove = false;
        else
            canPlayerMove = true;


    }


    void playSound(string path)
    {
        FMODUnity.RuntimeManager.PlayOneShot(path, GetComponent<Transform>().position);
    }
}
