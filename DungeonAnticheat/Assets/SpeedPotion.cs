using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedPotion : MonoBehaviour
{
    public float speedBoost = 5f;
    public float duration = 5f;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerMovement playerMovement = collision.GetComponent<PlayerMovement>();

            if (playerMovement != null)
            {
                StartCoroutine(SpeedBoostCoroutine(playerMovement));

                Destroy(gameObject);
            }
        }
    }

    IEnumerator SpeedBoostCoroutine(PlayerMovement player)
    {
        player.Speed += speedBoost;

        yield return new WaitForSeconds(duration);

        player.Speed -= speedBoost;
    }
}
