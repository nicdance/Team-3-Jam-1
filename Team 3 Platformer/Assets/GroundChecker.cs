using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundChecker : MonoBehaviour
{
    [SerializeField]
    protected NicolePlayerController controller;
    public void OnTriggerEnter(Collider other)
    {
        Debug.Log("gorund checker hit ground");
        controller.SetAsGrounded();
    }
}
