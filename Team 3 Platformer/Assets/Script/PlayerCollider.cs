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
        if (collision.gameObject.tag == "Enemy")
        {
            onChangeHealth?.Invoke(-1);
        }
        else if (collision.gameObject.tag == "Health")
        {
            onChangeHealth?.Invoke(1);
        }
        else if (collision.gameObject.tag == "AddHealth")
        {
            onUnlockHealth?.Invoke();
        }
    }

        public void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.tag == "Enemy")
            {
                onChangeHealth?.Invoke(-1);
            }
            else if (other.gameObject.tag == "Health")
            {
                onChangeHealth?.Invoke(1);
                other.gameObject.SetActive(false);
            }
            else if (other.gameObject.tag == "AddHealth")
            {
                onUnlockHealth?.Invoke();
                other.gameObject.SetActive(false);
            }

        }
    }
