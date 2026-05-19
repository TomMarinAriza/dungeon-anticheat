using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrampsDamage : MonoBehaviour
{
    [SerializeField] private GameObject player;

    private int damage = 10;

    private float damageCooldown = 1f;
    private float nextDamageTime;

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject == player)
        {
            if (Time.time >= nextDamageTime)
            {
                player.GetComponent<PlayerHealth>().TakeDamage(damage);

                nextDamageTime = Time.time + damageCooldown;
            }
        }
    }
}