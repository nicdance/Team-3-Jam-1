﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerManager : MonoBehaviour
{
    //[Header("")]
    [SerializeField]
    protected Transform spawnPoint;
    [SerializeField]
    protected bool canTakeDamage = true;
    [SerializeField]
    protected float timeSinceDamage = 0;
    [SerializeField]
    protected float delaybetweenDamage = 1;

    public static event Action onUnlockHealth;
    public static event Action<int, GameObject> onChangeHealth;

    public static event Action onUnlockLife;
    public static event Action<int> onChangeLives;


    public void Start()
    {
        gameObject.transform.position = spawnPoint.position;
        HealthManager.onPlayerLostLife += ResetPlayer;
    }
    public void Update()
    {
        if (!canTakeDamage)
        {
            timeSinceDamage += Time.deltaTime;
            if (timeSinceDamage > delaybetweenDamage)
            {
                canTakeDamage = true;
            }
        }
    }
    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            onChangeHealth?.Invoke(-1, collision.gameObject);
        }
        else if (collision.gameObject.tag == "Health")
        {
            onChangeHealth?.Invoke(1, collision.gameObject);
        }
        else if (collision.gameObject.tag == "AddHealth")
        {
            onUnlockHealth?.Invoke();
        }
        else if (collision.gameObject.tag == "MovingPlatform")
        {
            gameObject.transform.parent = collision.transform;
        }
    }
    public void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "MovingPlatform")
        {
                gameObject.transform.parent = null;
        }

    }
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            if (canTakeDamage)
            {
                onChangeHealth?.Invoke(-1, other.gameObject);
                canTakeDamage = false;
                timeSinceDamage = 0f;
            }
        }
        else if (other.gameObject.tag == "Health")
        {
            onChangeHealth?.Invoke(1, other.gameObject);
            //   other.gameObject.SetActive(false);
        }
        else if (other.gameObject.tag == "AddHealth")
        {
            onUnlockHealth?.Invoke();
            other.gameObject.SetActive(false);
        }
        else if (other.gameObject.tag == "SpawnPoint")
        {
            spawnPoint = other.transform;
        }
        else if (other.gameObject.tag == "InstantDeath")
        {
            onChangeHealth?.Invoke(-10,null);
        }
    }

    public void ResetPlayer() {
        transform.position = spawnPoint.position;// + spawnPoint.parent.position;
    }
}
