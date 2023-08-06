using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHealth : MonoBehaviour
{
    public int maxHealth = 500; // determines the maximum health of the boss.
    public int currentHealth; // represents the current health of the boss.
    public bool isVulnerable = false; // determines if the boss is currently vulnerable to damage.
    
    public GameObject deathEffect; // references a GameObject that represents the visual effect to play when the boss dies.
    public HealthBarSlider healthBar; // references "HealthBarSlider" component used to display the boss's health on a health bar UI.

    private void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    /*
     * Developer option for testing
     */
   // void Update()
   // {
   //     if (Input.GetKeyDown(KeyCode.Q))
   //     {
   //         TakeDamage(75);
   //     }
   // }

   /// <summary>
   /// Inflicts damage to the boss's health.
   /// </summary>
   /// <param name="damage">The amount of damage to inflict.</param>
   
   public void TakeDamage(int damage)
   {
       if (isVulnerable)
       {
           return;
       }

       currentHealth -= damage;
       healthBar.SetHealth(currentHealth);

       if (currentHealth <= 0)
       {
           Die();
       }
   }

    /// <summary>
    /// Handles the death of the boss.
    /// </summary>
    void Die()
    {
        Instantiate(deathEffect, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}