using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;

    public GameObject particlePrefab;
    public float shootForce = 10f;
    public float detectionRange = 10f;

    private HealthController playerHealth;

    void Start()
    {
        currentHealth = maxHealth;
        playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<HealthController>();
    }

    void Update()
    {
        // Check if the player is within range
        if (Vector3.Distance(transform.position, playerHealth.transform.position) <= detectionRange)
        {
            // Shoot the particle at the player
            Shoot();
        }
    }

    void Shoot()
    {
        // Instantiate the particle prefab
        GameObject particle = Instantiate(particlePrefab, transform.position, Quaternion.identity);

        // Calculate the direction towards the player
        Vector3 direction = (playerHealth.transform.position - transform.position).normalized;

        // Apply a force to the particle to shoot it towards the player
        Rigidbody particleRigidbody = particle.GetComponent<Rigidbody>();
        particleRigidbody.AddForce(direction * shootForce, ForceMode.Impulse);

        // Destroy the particle after a certain time if it doesn't collide with the player
        Destroy(particle, 5f);
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
        // Destroy the boss GameObject
        Destroy(gameObject);
    }
}
