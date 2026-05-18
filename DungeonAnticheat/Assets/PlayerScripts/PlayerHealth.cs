using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{   
    public Slider healthBar;
    public int health = 100;
    public int currentHealth;

    void Start()
    {
        currentHealth = health;
        healthBar.value = currentHealth;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        healthBar.value = currentHealth;
        if (currentHealth <= 0)
        {
            Debug.Log("Player has died.");
            Die();
        }
    }

    void Die()
    {
        
        Debug.Log("Player has died.");
    }


}
