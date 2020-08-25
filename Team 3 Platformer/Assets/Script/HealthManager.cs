using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public class HealthManager : MonoBehaviour
{
    [Header("Health")]
    public HealthBubbles[] healthBubbles;
    [SerializeField]
    protected int startingHealth;
    [SerializeField]
    protected int currentHealth;
    [SerializeField]
    protected int maxHealth;

    [Header("Lives")]
    public Life[] lives;
    [SerializeField]
    protected int startingLives;
    [SerializeField]
    protected int currentLives;
    [SerializeField]
    protected int maxLives;


    public static event Action onPlayerLostLife;

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1;
        currentHealth = startingHealth;
        maxHealth = startingHealth;


        currentLives = startingLives;
        maxLives = startingLives;

        ActivateHealth();
        ActivateLives();

        PlayerManager.onUnlockHealth += UnlockHealth;
        PlayerManager.onChangeHealth += ChangeHealth;


        PlayerManager.onUnlockLife += UnlockLife;
        PlayerManager.onChangeLives += ChangeLives;
    }
    public void ResetHealth()
    {
        currentHealth = startingHealth;
        maxHealth = startingHealth;
        ActivateHealth();
    }

    // Activates  hears up to MaxHEalth
    public void ActivateLives()
    {
        for (int i = 0; i < maxLives; i++)
        {
            lives[i].ActivateLife();
        }
        DisplayLives();
    }
    public void DisplayLives()
    {
        for (int i = 0; i < maxLives; i++)
        {
            if (i >= currentLives)
            {
                lives[i].DrainLife();
            }
            else
            {
                lives[i].FillLife();
            }
        }
    }

    // Unlocks a new Life
    public void UnlockLife()
    {
        maxHealth += 1;
        if (maxLives > lives.Length)
        {
            maxLives = lives.Length;
        }
        ActivateHealth();
    }


    // Change currentLives based on live
    public void ChangeLives(int live)
    {
        currentLives += live;
        if (currentLives > maxLives)
        {
            currentLives = maxLives;
        }
        else if (currentLives <= 0)
        {
            currentLives = 0;
            GameManager.instance.GameOver();
            Time.timeScale = 0;
        }
        else if (live < 0) {
            onPlayerLostLife?.Invoke();
            ResetHealth();
        }
        DisplayLives();
    }





    // Activates  hears up to MaxHEalth
    public void ActivateHealth()
    {
        for (int i = 0; i < maxHealth; i++)
        {
            healthBubbles[i].ActivateHealth();
        }
        DisplayHealth();
    }

    public void DisplayHealth()
    {
        for (int i = 0; i < maxHealth; i++)
        {
            if (i >= currentHealth)
            {
                healthBubbles[i].DrainHeart();
            }
            else
            {
                healthBubbles[i].FillHeart();
            }
        }
    }

    // Unlocks a new heart
    public void UnlockHealth()
    {
        maxHealth += 1;
        currentHealth += 1;
        if (maxHealth > healthBubbles.Length)
        {
            maxHealth = healthBubbles.Length;
        }
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
        ActivateHealth();
    }


    // Change currentHealth based on health
    public void ChangeHealth(int health, GameObject other) {
        currentHealth += health;
        if (currentHealth>maxHealth)
        {
            currentHealth = maxHealth;
        }
        else if (currentHealth <=0)
        {
            currentHealth = 0;
            ChangeLives(-1);
        }
        else if(health>0)
        {
            other.SetActive(false);
        }
        DisplayHealth();
    }

}
