using System;
using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using System.Transactions;
using TreeEditor;
using UnityEditorInternal;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    protected Animator animator;
    public GameObject character;
    public static event Action<int, GameObject> onChangeHealth;
    public static event Action onZoomCamera;


    public float moveSpeed = 8f;
    public float VerticalSpeed = 20f;
    public float laneChangeSpeed = 30f;
    public float jumpSpeed = 5f;
    public float doubleJumpModifier = 5f;
    public bool isgrounded = true;
    public float verticalDistance = 2f;
    public float laneOffset = 3f;
    public bool candoublejump;

    public float fallMultiplier = 2.5f;


    protected const int startingLane = 1;

    protected int currentLane = startingLane;
    protected int previousDirection = 0;
    protected Vector3 targetPosition = Vector3.zero;
    protected Vector3 startingPosition = Vector3.forward;

    [SerializeField]
    protected bool isFalling = false;
    protected Vector3 startFallPosition;

    [SerializeField]
    protected float lastHAxis;
    [SerializeField]
    protected bool canMove = true;
    public bool isAlive = true;

    //to keep our rigid body
    public Rigidbody rb;

    bool isMovingLanes = false;

    [SerializeField]
    protected float lastDirection = 1;
    // Use this for initialization
    void Start()
    {
        //get the rigid body component for later use
        rb = GetComponent<Rigidbody>();
        HealthManager.onPlayerLostLife += PlayerDied;
    }

    public void PlayerDied()
    {
        isAlive = false;
        animator.SetBool("isDead", true);
    }


    public void PlayerAlive()
    {
        isAlive = true;
        animator.SetBool("isDead", false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftAlt))
        {
            onZoomCamera?.Invoke();
        }
        if (isAlive)
        {

            // Set x and z velocities to zero
            rb.velocity = new Vector3(0, rb.velocity.y, 0);

            // Distance ( speed = distance / time --> distance = speed * time)
            float distance = moveSpeed * Time.deltaTime;

            // Distance ( speed = distance / time --> distance = speed * time)
            float verticalDistance = VerticalSpeed * Time.deltaTime;

            // Input on x ("Horizontal")
            float hAxis = Input.GetAxisRaw("Horizontal");

            if (hAxis != 0)
            {
                Vector3 direction = -(Vector3.forward * hAxis);// as the key is held down the h or v values will move toward full -1 or 1, thus handling the smooth rotation
                character.transform.rotation = Quaternion.LookRotation(direction);
                lastDirection = hAxis;

            }
            else
            {
                Vector3 direction = -(Vector3.forward * lastDirection );// as the key is held down the h or v values will move toward full -1 or 1, thus handling the smooth rotation
                character.transform.rotation = Quaternion.LookRotation(direction);
            }

            if (hAxis > 0)
            {
                rb.AddForce(transform.forward * moveSpeed, ForceMode.Impulse);
                animator.SetBool("Walk", true);
            }
            else if (hAxis < 0)
            {
                rb.AddForce(transform.forward * -moveSpeed, ForceMode.Impulse);
                animator.SetBool("Walk", true);

            }
            else
            {
                rb.AddForce(transform.forward * 0f);
                animator.SetBool("Walk", false);
            }

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
                    //rb.AddForce(Vector3.up * jumpSpeed, ForceMode.Impulse);
                    rb.velocity = Vector3.up * jumpSpeed;
                    isgrounded = false;
                    candoublejump = true;
                    animator.SetBool("Jump", true);
                }
                else
                {
                    if (candoublejump)
                    {
                        candoublejump = false;
                        //rb.velocity = new Vector3(0, 0, 0);
                        //rb.AddForce(Vector3.up * jumpSpeed, ForceMode.Impulse);
                        rb.velocity = Vector3.up * doubleJumpModifier;

                    }
                }
            }

            if (rb.velocity.y < 0)
            {
                rb.velocity += Vector3.up * Physics.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
                if (!isFalling)
                {
                    startFallPosition = transform.position;
                    isFalling = true;
                }
                if (!isgrounded && isFalling && startFallPosition.y - System.Math.Abs(transform.position.y) <= -10)
                {
                    onChangeHealth?.Invoke(-10, null);
                }
            }

            Vector3 newTargetPosition = targetPosition;
            newTargetPosition.y = transform.position.y;
            newTargetPosition.x = transform.position.x;
            transform.position = Vector3.MoveTowards(transform.position, newTargetPosition, laneChangeSpeed * Time.deltaTime);

        }
    }

    public void SetAsGrounded()
    {
        isgrounded = true;
        isFalling = false;

        animator.SetBool("Jump", false);
    }

    public void ChangeLane(int direction)
    {
        Vector3 checkDirection;
        if (direction > 0)
        {
            checkDirection = Vector3.forward;
        }
        else
        {
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

            isMovingLanes = true;
        }



    }

    public void OnCollisionEnter(Collision collision)
    {
        if (isgrounded == false)
        {
            rb.velocity = Vector3.zero;
        }
        if (collision.gameObject.tag == "Ground")
        {
            isgrounded = true;
        }

        else
        if (collision.gameObject.tag == "Bounce")
        {
            Debug.Log("Bounce");
            isgrounded = false;
            collision.gameObject.GetComponent<ActionOnCollision>().PerformAction(this);
        }

    }

    public void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "Obstacle")
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
