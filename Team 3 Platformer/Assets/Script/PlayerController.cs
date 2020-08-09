﻿using System.Collections;
using System.Collections.Generic;
using TreeEditor;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 8f;
    public float VerticalSpeed = 20f;
    public float laneChangeSpeed = 30f;
    public float jumpSpeed = 5f;
    public bool isgrounded = true;
    public float verticalDistance = 2f;
    public float laneOffset = 3f;


    protected const int startingLane = 1;

    protected int currentLane = startingLane;
    protected Vector3 targetPosition = Vector3.zero;
    protected Vector3 startingPosition = Vector3.forward;

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
            }
        }

        Vector3 verticalTargetPosition = targetPosition;
        verticalTargetPosition.y = transform.position.y;
        verticalTargetPosition.x = transform.position.x + (hAxis * distance);
        transform.position = Vector3.MoveTowards(transform.position, verticalTargetPosition, laneChangeSpeed * Time.deltaTime);
    }

    public void ChangeLane(int direction)
    {
        int targetLane = currentLane + direction;

        // Ignore, we are on the edge lanes.
        if (targetLane < 0 || targetLane > 2)
        {
            return;
        }

        currentLane = targetLane;
        targetPosition = new Vector3(transform.position.x, transform.position.y, (currentLane - 1) * laneOffset);

    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            isgrounded = true;
        }
    }

}