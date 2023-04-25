using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;

    void Start()
    {
        currentHealth = maxHealth;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            currentHealth -= 25;
            Destroy(collision.gameObject);
            if (currentHealth <= 0)
            {
                Die();
            }
        }
    }

    void Die()
    {
        // Play death animation or sound, remove object from scene, etc.
        Destroy(gameObject);
    }
}
