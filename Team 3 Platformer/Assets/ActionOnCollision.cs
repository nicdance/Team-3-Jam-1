using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionOnCollision : MonoBehaviour
{
    [SerializeField]
    protected float modifier=10f;
    [SerializeField]
    protected CollisionAction action;
    public enum CollisionAction
    { 
        BOUNCY,
        STEAMPOWERED                    
    }

    public void PerformAction(PlayerController player) {
        player.rb.AddForce(Vector3.up * modifier, ForceMode.Impulse);
    }
}
