using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPotion : MonoBehaviour
{
    public GameObject player;
    private int healAmount = 20;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject == player)
        {
            player.GetComponent<PlayerHealth>().Heal(healAmount); // Heal the player
            Destroy(gameObject); // Destroy the potion after use
        }
    }
}
