using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{   
    public Slider healthBar;
    public int health = 100;

    void Start()
    {
        healthBar.value = health;
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        healthBar.value = health;
        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        // Handle player death (e.g., respawn, game over, etc.)
        Debug.Log("Player has died.");
    }


}
