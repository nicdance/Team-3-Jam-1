using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform player;
    public Vector3 startPosition;
    public Vector3 offset;

    public void Start()
    {
        startPosition = transform.position;
    }
    public void LateUpdate()
    {
         Vector3 newPosition = new Vector3(player.position.x,player.position.y, 0);
         //Vector3 newPosition = new Vector3(player.position.x,player.position.y, 0);
         transform.position = newPosition + startPosition+offset;
        //transform.position = newPosition + offset;
    }
}