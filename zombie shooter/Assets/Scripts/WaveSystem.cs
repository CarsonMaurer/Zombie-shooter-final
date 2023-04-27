using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WaveSystem : MonoBehaviour
{
    public List<GameObject> enemyPrefabs; // List of enemy prefabs to spawn
    public List<int> enemiesPerWave; // Number of enemies to spawn for each wave
    public float timeBetweenWaves = 3.0f; // Time between waves in seconds
    public TextMeshProUGUI waveText; // UI text for displaying wave information
    public GameObject bossPrefab; // Boss enemy prefab
    public int bossWave = 5; // Wave number to spawn the boss
    private int waveNum = 1; // Current wave number
    private int numEnemiesRemaining; // Number of enemies remaining in the current wave

    void Start()
    {
        StartWave();
    }

    void Update()
    {
        if (numEnemiesRemaining == 0)
        {
            StartCoroutine(StartNextWave());
        }
    }

    void StartWave()
    {
        if (waveNum == bossWave)
        {
            SpawnBoss();
        }
        else
        {
            numEnemiesRemaining = enemiesPerWave[waveNum - 1];
            Debug.Log("Num enemies remaining: " + numEnemiesRemaining);
            waveText.text = "Wave " + waveNum + "\n" + numEnemiesRemaining + " enemies remaining";
            SpawnEnemies();
        }
    }

    IEnumerator StartNextWave()
    {
        yield return new WaitForSeconds(timeBetweenWaves);
        waveNum++;
        if (waveNum > enemiesPerWave.Count)
        {
            // End of waves
            waveText.text = "Wave Over!";
        }
        else
        {
            StartWave();
        }
    }

    void SpawnEnemies()
    {
        for (int i = 0; i < numEnemiesRemaining; i++)
        {
            // Choose a random enemy prefab to spawn
            int index = Random.Range(0, enemyPrefabs.Count);
            GameObject enemyPrefab = enemyPrefabs[index];

            // Choose a random spawn position within the specified range
            Vector3 spawnPos = new Vector3(Random.Range(35.73f, -19.51f), 0.0f, Random.Range(-34.9f, 39.99f));

            // Instantiate the enemy prefab at the spawn position
            GameObject enemy = Instantiate(enemyPrefab, spawnPos, Quaternion.identity);

            // Register the enemy's OnDeath event
            enemy.GetComponent<Enemy>().OnDeath.AddListener(OnEnemyDeath);
        }
    }

    void SpawnBoss()
    {
        waveText.text = "Boss Wave!";
        // Spawn the boss at a random location within the specified range
        Vector3 spawnPos = new Vector3(Random.Range(-10.0f, 10.0f), 0.0f, Random.Range(-10.0f, 10.0f));
        GameObject boss = Instantiate(bossPrefab, spawnPos, Quaternion.identity);
        // Register the boss's OnDeath event
        boss.GetComponent<Enemy>().OnDeath.AddListener(OnBossDeath);
    }

    void OnEnemyDeath()
    {
        numEnemiesRemaining--;
    }

    void OnBossDeath()
    {
        waveText.text = "You Win!";
    }
}
