using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class Enemy : MonoBehaviour
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
   

    public void Die()
    {

        Destroy(gameObject);
    }
        
        
}
