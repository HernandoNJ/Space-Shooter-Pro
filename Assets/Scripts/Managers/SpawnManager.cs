using EnemyLib;
using System.Collections;
using UnityEngine;
using static UnityEngine.Debug;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private GameObject enemySpawner;
    [SerializeField] private GameObject[] powerups;
    [SerializeField] private int enemiesInWave;
    [SerializeField] private int waveNumber;
    [SerializeField] private int enemiesAmount;
    [SerializeField] private float startTime;

    [SerializeField] private bool isMultiShotActive;
    [SerializeField] private int randomPowerup;
    private bool isPlayerAlive;
    private bool isWaveRunning;

    private void OnEnable()
    {
        Enemy.onEnemyDestroyed += DecreaseEnemiesAmount;
        Player.onPlayerDestroyed += PlayerIsDeath;
    }
    private void Start()
    {
        waveNumber = 1;
        isWaveRunning = true;
        isPlayerAlive = true;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return) && isWaveRunning == false && enemiesAmount == 0)
        {
            StartCoroutine(SpawnEnemiesWaveRoutine());
        }
    }
    private void OnDisable()
    {
        Enemy.onEnemyDestroyed -= DecreaseEnemiesAmount;
        Player.onPlayerDestroyed -= PlayerIsDeath;
    }
    private void DecreaseEnemiesAmount()
    {
        enemiesAmount--;
    }
    public void PlayerIsDeath()
    {
        isPlayerAlive = false;
    }
    IEnumerator SpawnEnemiesWaveRoutine()
    {
        startTime = Time.time;

        while (Time.time - startTime < (waveNumber * 5f))
        {
            Log("Time - startTime " + (Time.time - startTime));

            float xRandomPos = Random.Range(-9.5f, 9.5f);
            Vector2 enemySpawnPos = new Vector2(xRandomPos, 5f);
            
            GameObject newEnemy = Instantiate(enemyPrefab, enemySpawnPos, Quaternion.identity);
            newEnemy.transform.parent = enemySpawner.transform;

            enemiesAmount++;

            yield return new WaitForSeconds(2f);
        }

        waveNumber++;
        isWaveRunning = false;
    }
    IEnumerator SpawnPowerupRoutine()
    {
        yield return new WaitForSeconds(3f);

        while (isPlayerAlive)
        {
            var powerupPos = new Vector2(Random.Range(-9.5f, 9.5f), 5f);
            //int randomPowerup = Random.Range(0, 6);
            randomPowerup = Random.Range(0, 6);
            if (randomPowerup != 5)
            {
                Instantiate(powerups[randomPowerup], powerupPos, Quaternion.identity);
                yield return new WaitForSeconds(Random.Range(2, 4));
            }
        }
    }
    IEnumerator SpawnMultipleShot()
    {
        yield return new WaitForSeconds(20);

        while (isPlayerAlive)
        {
            Log("multishot time: " +  Time.time);
            Vector2 powerupPos = new Vector2(Random.Range(-9.5f, 9.5f), 5f);
            Instantiate(powerups[5], powerupPos, Quaternion.identity);
            yield return new WaitForSeconds(20);
        }
    }
    public void StartSpawnning()
    {
        StartCoroutine(SpawnEnemiesWaveRoutine());
        StartCoroutine(SpawnPowerupRoutine());
        StartCoroutine(SpawnMultipleShot());
    }
}