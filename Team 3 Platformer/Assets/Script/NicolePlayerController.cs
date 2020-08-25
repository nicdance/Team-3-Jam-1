using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using System.Transactions;
using TreeEditor;
using UnityEngine;

public class NicolePlayerController : MonoBehaviour
{
    //   public float xMoveSpeed;
    //   public float zMoveSpeed;
    //public float moveX;
    //public float moveZ;

    //   public void Update()
    //   {
    //       HandleMovement();
    //   }

    //   public void HandleMovement() { 
    //       moveX = Input.GetAxis("Horizontal");
    //       moveZ= Input.GetAxis("Vertical");

    //       if (moveX !=0 || moveZ!=0)
    //       {
    //           Vector3 direction = new Vector3(moveX* xMoveSpeed, transform.position.y, moveZ* zMoveSpeed);
    //           transform.Translate(direction*Time.deltaTime);
    //       }
    //   }

    //   public void OnCollisionEnter(Collision collision)
    //   {
    //       if (collision.gameObject.tag == "Ground")
    //       {
    //          //sgrounded = true;
    //       }
    //       else
    //       if (collision.gameObject.tag == "Obstacle")
    //       {
    //           moveZ = 0;
    //           moveX = 0;
    //       }
    //   }

    public float moveSpeed = 8f;
    public float VerticalSpeed = 20f;
    public float laneChangeSpeed = 30f;
    public float jumpSpeed = 5f;
    public bool isgrounded = true;
    public float verticalDistance = 2f;
    public float laneOffset = 3f;
    public bool candoublejump;



    protected const int startingLane = 1;

    protected int currentLane = startingLane;
    protected int previousDirection = 0;
    protected Vector3 targetPosition = Vector3.zero;
    protected Vector3 startingPosition = Vector3.forward;

    [SerializeField]
    protected float lastHAxis;
    [SerializeField]
    protected bool canMove =true;

    //to keep our rigid body
    Rigidbody rb;


    // Use this for initialization
    void Start()
    {
        //get the rigid body component for later use
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        // Set x and z velocities to zero
        rb.velocity = new Vector3(0, rb.velocity.y, 0);

        // Distance ( speed = distance / time --> distance = speed * time)
        float distance = moveSpeed * Time.deltaTime;

        // Distance ( speed = distance / time --> distance = speed * time)
        float verticalDistance = VerticalSpeed * Time.deltaTime;

        // Input on x ("Horizontal")
        float hAxis = Input.GetAxis("Horizontal");
        if (!canMove && lastHAxis == hAxis)
        {
            hAxis = 0;
        }
        else {
           canMove = true;
            lastHAxis = hAxis;
        }
        //if (hAxis ==-1)
        //{
        //    gameObject.transform.rotation
        //}
        if (Input.GetKeyDown(KeyCode.W))
        {
            ChangeLane(1);
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            ChangeLane(-1);
        }
        else if (Input.GetKeyDown(KeyCode.Space))
        {
            if (isgrounded)
            {
                rb.AddForce(Vector3.up * jumpSpeed, ForceMode.Impulse);
                isgrounded = false;
                candoublejump = true;
            }
            else
            {
                if (candoublejump)
                {
                    candoublejump = false;
                    rb.velocity = new Vector3(0, 0, 0);
                    rb.AddForce(Vector3.up * jumpSpeed, ForceMode.Impulse);


                }
            }
        }

        Vector3 newTargetPosition = targetPosition;
        newTargetPosition.y = transform.position.y;
        newTargetPosition.x = transform.position.x + (hAxis * distance);
        transform.position = Vector3.MoveTowards(transform.position, newTargetPosition, laneChangeSpeed * Time.deltaTime);
    }

    public void SetAsGrounded() {
        isgrounded = true;
    }
    public void ChangeLane(int direction)
    {
        Vector3 checkDirection;
        if (direction > 0)
        {
            checkDirection = Vector3.forward;
        }
        else {
            checkDirection = Vector3.back;
        }
        Debug.DrawRay(transform.position, checkDirection, Color.red, 3f);
        
        if (!Physics.Raycast(transform.position, checkDirection, 3f))
        {
            previousDirection = direction;
            int targetLane = currentLane + direction;

            // Ignore, we are on the edge lanes.
            if (targetLane < 0 || targetLane > 2)
            {
                return;
            }

            currentLane = targetLane;
            targetPosition = new Vector3(transform.position.x, transform.position.y, (currentLane - 1) * laneOffset);
        }



    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            isgrounded = true;
        }
        
        if (collision.gameObject.tag == "Obstacle")
        {
            //targetPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z);
            //transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
            canMove = false;
        }
    }

    public void OnCollisionStay(Collision collision)
    {
        if ( collision.gameObject.tag == "Obstacle")
        {
            canMove = false;
        }

    }
    public void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Obstacle")
        {
            canMove = true;
        }

    }

}
