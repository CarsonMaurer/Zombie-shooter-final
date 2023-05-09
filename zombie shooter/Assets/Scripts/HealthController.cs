using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HealthController : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;
    public TextMeshProUGUI healthText;
    public GameObject Player;
    public GameObject GameOverPanel;
    public GameObject StartScreen;
    public GameObject TutorialScreen;

    void Start()
    {
        currentHealth = maxHealth;
        StartScreen.SetActive(true);
        Time.timeScale = 0;
    }

    void Update()
    {
        healthText.text = currentHealth.ToString();
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            TakeDamage(10);
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Player.SetActive(false);
        Time.timeScale = 0;
        GameOverPanel.SetActive(true);
    }

    public void TryAgainButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1;

        
         
    }
    public void PlayButton()
    {

        StartScreen.SetActive(false);
        TutorialScreen.SetActive(false);
        Time.timeScale = 1;
    }

    public void TutorialButton()
    {
        TutorialScreen.SetActive(true);
        StartScreen.SetActive(false);
        Time.timeScale = 0;
        
    }
}

