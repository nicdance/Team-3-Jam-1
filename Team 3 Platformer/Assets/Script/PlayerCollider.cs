using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollider : MonoBehaviour
{

    public static event Action onUnlockHealth;
    public static event Action<int> onChangeHealth;

    public void OnCollisionEnter(Collision collision)
    {
        Debug.Log("OnCollisionEnter");
        if (collision.gameObject.tag  == "Enemy")
        {
            onChangeHealth?.Invoke(-1);
        }
        else if(collision.gameObject.tag == "Health")
        {
            onChangeHealth?.Invoke(1);
        }
        else if (collision.gameObject.tag == "AddHealth")
        {
            onUnlockHealth?.Invoke();
        }
    }
}
