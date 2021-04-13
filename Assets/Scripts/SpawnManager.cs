﻿using System.Collections;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private GameObject enemySpawnner;
    [SerializeField] private GameObject[] powerups;
    [SerializeField] private int enemiesInWave;
    [SerializeField] private int waveNumber;
    [SerializeField] private int enemiesAmount;
    [SerializeField] private float startTime;

    private bool isPlayerAlive = true;
    private bool isWaveRunning;

    private void Start()
    {
        waveNumber = 1;
        isWaveRunning = true;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return) && isWaveRunning == false && enemiesAmount == 0)
        {
            StartCoroutine(SpawnEnemiesWaveRoutine());
        }
    }

    public void DecreaseEnemiesAmount()
    {
        enemiesAmount--;
    }

    public void StartSpawnning()
    {
        StartCoroutine(SpawnEnemiesWaveRoutine());
        StartCoroutine(SpawnPowerupRoutine());
        StartCoroutine(SpawnMultipleShot());
    }

    IEnumerator SpawnEnemiesWaveRoutine()
    {
        startTime = Time.time;

        while (Time.time - startTime < (waveNumber * 5f))
        {
            Debug.Log("Time - startTime " + (Time.time - startTime));

            float xRandomPos = Random.Range(-9.5f, 9.5f);
            Vector2 enemySpawnPos = new Vector2(xRandomPos, 5f);

            GameObject newEnemy = Instantiate(enemyPrefab, enemySpawnPos, Quaternion.identity);
            newEnemy.transform.parent = enemySpawnner.transform;

            enemiesAmount++;

            yield return new WaitForSeconds(1f);
        }

        waveNumber++;
        isWaveRunning = false;
    }

    IEnumerator SpawnPowerupRoutine()
    {
        yield return new WaitForSeconds(4f);

        while (isPlayerAlive)
        {
            Vector2 powerupPos = new Vector2(Random.Range(-9.5f, 9.5f), 5f);
            int randomPowerup = Random.Range(0, 5);
            Instantiate(powerups[randomPowerup], powerupPos, Quaternion.identity);
            yield return new WaitForSeconds(Random.Range(4, 7));
        }
    }

    IEnumerator SpawnMultipleShot()
    {
        yield return new WaitForSeconds(20);

        while (isPlayerAlive)
        {
            Vector2 powerupPos = new Vector2(Random.Range(-9.5f, 9.5f), 5f);
            Instantiate(powerups[5], powerupPos, Quaternion.identity);
            yield return new WaitForSeconds(20);
        }
    }

    public void OnPlayerDestroyed()
    {
        isPlayerAlive = false;
    }
}