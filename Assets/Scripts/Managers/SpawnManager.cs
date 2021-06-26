using System;
using System.Collections;
using System.Collections.Generic;
using PlayerNS;
using Starting;
using UnityEngine;
using ScriptableObjects.Waves;
using Random = UnityEngine.Random;

namespace Managers
{
public class SpawnManager : SingletonBP<SpawnManager>
{
    [SerializeField] private int randomPowerup;
    [SerializeField] private int currentWaveIndex;
    [SerializeField] private int currentWaveNumber;
    [SerializeField] private GameObject[] powerups;
    [SerializeField] private List<WaveData> waves = new List<WaveData>();
    [SerializeField] private Transform enemySpawner;
    [SerializeField] private bool isPlayerAlive;

    public static event Action<int> OnWaveStarted;

    // todo ask Austin what happens if I put code in OnTriggerEnter in different scripts attached to Player? does it will execute all of them?

    private void OnEnable()
    {
        Asteroid.OnAsteroidDestroyed += StartSpawning;
        Player.OnPlayerActive += SetPlayerAlive;
        PlayerHealth.OnPlayerDestroyed += SetPlayerDestroyed;
        PlayerStart.onPlayerDestroyed += SetPlayerDestroyed;
    }

    private void OnDisable()
    {
        Asteroid.OnAsteroidDestroyed -= StartSpawning;
        Player.OnPlayerActive -= SetPlayerAlive;
        PlayerHealth.OnPlayerDestroyed -= SetPlayerDestroyed;
        PlayerStart.onPlayerDestroyed -= SetPlayerDestroyed;
    }

    private void StartSpawning()
    {
        StartCoroutine(EnemyWaveRoutine());
        StartCoroutine(SpawnPowerupRoutine());
        StartCoroutine(SpawnMultipleShotRoutine());
    }

    private void SetPlayerAlive()
    {
        isPlayerAlive = true;
    }

    public void SetPlayerDestroyed()
    {
        isPlayerAlive = false;
    }

    private IEnumerator SpawnPowerupRoutine()
    {
        Debug.Log("entering powerup routine");
        // todo ask Austin: what happens if isPlayerAlive is false? the coroutine continues being executed waiting 3 seconds?
        yield return new WaitForSeconds(3f);
        while (isPlayerAlive)
        {
            var powerupPos = new Vector2(Random.Range(-9.5f, 9.5f), 5f);
            randomPowerup = Random.Range(0, 6);
            Instantiate(powerups[randomPowerup], powerupPos, Quaternion.identity);
            yield return new WaitForSeconds(Random.Range(4, 7));
        }
        Debug.Log("exiting spawn powerups: time: " + Time.time);
    }

    private IEnumerator SpawnMultipleShotRoutine()
    {
        Debug.Log("entering multishot routine");
        yield return new WaitForSeconds(5);
        while (isPlayerAlive)
        {
            var powerupPos = new Vector2(Random.Range(-9.5f, 9.5f), 5f);
            Instantiate(powerups[6], powerupPos, Quaternion.identity);
            yield return new WaitForSeconds(7); // todo ask Austin... trying to fix issue ... how to debug
        }

        Debug.Log("exiting spawn multishot: time: " + Time.time);
    }

    private IEnumerator EnemyWaveRoutine()
    {
        Debug.Log("entering enemies routine");
        while (isPlayerAlive)
        {
            Debug.Log(Time.time);
            currentWaveNumber = currentWaveIndex + 1;
            OnWaveStarted?.Invoke(currentWaveNumber);
            var currentWave = waves[currentWaveIndex].waveGameObjects; // Get objects list to instantiate
            var previousWave =
                new GameObject("PreviousWave"); // Create a new parent to be removed after wave is finished
            previousWave.transform.SetParent(enemySpawner);

            foreach (var obj in currentWave)
            {
                Instantiate(obj, previousWave.transform); // Instantiate obj in PreviousWave
                yield return new WaitForSeconds(1);
            }

            yield return new WaitForSeconds(1); // wait 5 seconds after wave is done
            Destroy(previousWave); // clear up wave objects
            currentWaveIndex++;

            if (currentWaveIndex != waves.Count) continue;
            break; // get out from while loop
        }

        Debug.Log("exiting waves routine");
        Debug.Log("Exiting time: " + Time.time);
    }
}
}
