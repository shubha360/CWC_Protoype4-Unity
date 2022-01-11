using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] enemyPrefabs;
    public GameObject[] powerupPrefabs;

    private float spawnRange = 8.5f;

    private int enemyCount;
    private int waveNumber = 1;

    public GameObject bossPrefab;
    public GameObject[] miniEnemyPrefabs;
    public int bossRound;

    // Start is called before the first frame update
    void Start()
    {
        int randomPowerUp = Random.Range(0, powerupPrefabs.Length);
        Instantiate(powerupPrefabs[randomPowerUp], GenerateSpawnPosition(), powerupPrefabs[randomPowerUp].transform.rotation);

        SpawnEnemyWave(waveNumber);
    }

    // Update is called once per frame
    void Update()
    {
        enemyCount = FindObjectsOfType<Enemy>().Length;

        if (enemyCount == 0)
        {
            waveNumber++;
            if (waveNumber % bossRound == 0)
            {
                SpawnBossWave(waveNumber);
            } 
            else
            {
                SpawnEnemyWave(waveNumber);
            }

            int randomPowerUp = Random.Range(0, powerupPrefabs.Length);
            Instantiate(powerupPrefabs[randomPowerUp], GenerateSpawnPosition(), powerupPrefabs[randomPowerUp].transform.rotation);
        }
    }

    void SpawnEnemyWave(int enemiesToSpawn)
    {
        int randomEnemy = Random.Range(0, enemyPrefabs.Length);

        for (int i = 0; i < enemiesToSpawn; i++) {
            
            Instantiate(
                enemyPrefabs[randomEnemy], 
                GenerateSpawnPosition(), 
                enemyPrefabs[randomEnemy].transform.rotation);
        }
    }

    void SpawnBossWave(int currentRpund)
    {
        int miniEnemysToSpawn;

        if (bossRound != 0)
        {
            miniEnemysToSpawn = currentRpund / bossRound;
        }
        else
        {
            miniEnemysToSpawn = 1;
        }

        var boss = Instantiate(bossPrefab, GenerateSpawnPosition(), bossPrefab.transform.rotation);
        boss.GetComponent<Enemy>().miniEnemySpawnCount = miniEnemysToSpawn;
    }

    public void SpawnMiniEnemy(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            int randomMini = Random.Range(0, miniEnemyPrefabs.Length);

            Instantiate(miniEnemyPrefabs[randomMini], GenerateSpawnPosition(), miniEnemyPrefabs[randomMini].transform.rotation);
        }
    }

    private Vector3 GenerateSpawnPosition()
    {
        float spawnPosX = Random.Range(-spawnRange, spawnRange);
        float spawnPosZ = Random.Range(-spawnRange, spawnRange);

        Vector3 spawnPosition = new Vector3(spawnPosX, 0, spawnPosZ);

        return spawnPosition;
    }
}
