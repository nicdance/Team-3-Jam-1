using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public class HealthManager : MonoBehaviour
{
    public HealthBubbles[] healthBubbles;
    [SerializeField]
    protected int startingHealth;
    [SerializeField]
    protected int currentHealth;
    [SerializeField]
    protected int maxHealth;



    // Start is called before the first frame update
    void Start()
    {
        currentHealth = startingHealth;
        maxHealth = startingHealth;
        ActivateHealth();
        PlayerCollider.onUnlockHealth += UnlockHealth;
        PlayerCollider.onChangeHealth += ChangeHealth;
    }


    // Unlocks a new heart
    public void UnlockHealth() {
        maxHealth += 1;
        ActivateHealth();
    }

    // Activates  hears up to MaxHEalth
    public void ActivateHealth()
    {
        for (int i = 0; i < maxHealth; i++)
        {
            Debug.Log(i);
            healthBubbles[i].ActivateHealth();
        }
        DisplayHealth();
    }

    // Change currentHealth based on health
    public void ChangeHealth(int health) {
        currentHealth += health;
        if (currentHealth>maxHealth)
        {
            currentHealth = maxHealth;
        }
        else if (currentHealth <=0)
        {
            currentHealth = 0;
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
}
