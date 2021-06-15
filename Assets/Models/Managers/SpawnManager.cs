using System.Collections;
using Starting;
using UnityEngine;
using static UnityEngine.Debug;

namespace Managers
{
public class SpawnManager : SingletonBP<SpawnManager>
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
        Enemy.EnemyBase.onEnemyDestroyed += DecreaseEnemiesAmount;
        PlayerStart.onPlayerDestroyed += PlayerIsDeath;
    }
    
    private void OnDisable()
    {
        Enemy.EnemyBase.onEnemyDestroyed -= DecreaseEnemiesAmount;
        PlayerStart.onPlayerDestroyed -= PlayerIsDeath;
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

    public void StartSpawning()
    {
        StartCoroutine(SpawnEnemiesWaveRoutine());
        StartCoroutine(SpawnPowerupRoutine());
        StartCoroutine(SpawnMultipleShotRoutine());
    }
    
    private void DecreaseEnemiesAmount()
    {
        enemiesAmount--;
    }
    
    public void PlayerIsDeath()
    {
        isPlayerAlive = false;
    }

    private IEnumerator SpawnEnemiesWaveRoutine()
    {
        startTime = Time.time;

        while (Time.time - startTime < (waveNumber * 5f))
        {
            Log("Time - startTime " + (Time.time - startTime));

            var xRandomPos = Random.Range(-9.5f, 9.5f);
            var enemySpawnPos = new Vector2(xRandomPos, 5f);

            var newEnemy = Instantiate(enemyPrefab, enemySpawnPos, Quaternion.identity);
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

            if (randomPowerup == 5) continue;
            Instantiate(powerups[randomPowerup], powerupPos, Quaternion.identity);

            yield return new WaitForSeconds(Random.Range(2, 4));
        }
    }

    private IEnumerator SpawnMultipleShotRoutine()
    {
        var waitTime = Random.Range(20,30);
        yield return new WaitForSeconds(waitTime);

        while (isPlayerAlive)
        {
            var powerupPos = new Vector2(Random.Range(-9.5f, 9.5f), 5f);
            Instantiate(powerups[5], powerupPos, Quaternion.identity);

            yield return new WaitForSeconds(20);
        }
    }

    
}
}
