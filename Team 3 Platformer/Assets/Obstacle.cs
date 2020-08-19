using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            ConstrainPlayer(collision.gameObject);
        }
    }

    private void ConstrainPlayer(GameObject player)
    {
            player.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
            player.GetComponent<Rigidbody>().constraints =  //RigidbodyConstraints.FreezePositionY | 
                                                            RigidbodyConstraints.FreezePositionX |
                                                            RigidbodyConstraints.FreezeRotationX |
                                                            RigidbodyConstraints.FreezeRotationY |
                                                            RigidbodyConstraints.FreezeRotationZ;
       
    }
}
