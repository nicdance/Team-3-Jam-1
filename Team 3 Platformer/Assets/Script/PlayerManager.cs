using System.Collections;
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

    [SerializeField]
    protected PlayerController playerController;


    public void Start()
    {
        playerController= gameObject.GetComponent<PlayerController>();
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
        StartCoroutine(Fade());
//        transform.position = spawnPoint.position;// + spawnPoint.parent.position;

//        GameManager.instance.cameraStart.SetupCamera();
    }

    IEnumerator Fade()
    {
        float alpha = 1;
        float increment = .002f;
        float delay = 2.1f;
        for (float ft = 0f; ft < alpha; ft += increment)
        {
            Color c = GameManager.instance.fadePanel.color;
            c.a = ft;
            GameManager.instance.fadePanel.color = c;
            yield return null;
        }
        transform.position = spawnPoint.position;// + spawnPoint.parent.position;

        GameManager.instance.cameraStart.SetupCamera();

        yield return new WaitForSeconds(delay);
        /////////
        //playerController.isAlive = true;
        playerController.PlayerAlive();

        for (float ft = alpha; ft > 0; ft -= increment)
        {
            Debug.Log(ft);
            Color c = GameManager.instance.fadePanel.color;
            c.a = ft;
            GameManager.instance.fadePanel.color = c;
            yield return null;
        }
    }

}
