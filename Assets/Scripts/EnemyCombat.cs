using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCombat : MonoBehaviour
{
    [SerializeField] private float damage;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if the colliding object is the player
        PlayerController player = collision.gameObject.GetComponent<PlayerController>();
        if (player != null)
        {
            // Apply damage to the player
            player.TakeDamage(damage); // Damage amount
            Debug.Log("Player collided with enemy and took damage.");
        }
    }
}