using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using EnemyNS;
using PlayerNS;
using Starting;
using UnityEngine;
using Waves;
using Random = UnityEngine.Random;

// Notes:
/* How to Create a Random Loot Table in Unity C#
* https://www.youtube.com/watch?v=OUlxP4rZap0
*/
/* var int[] intArray = {60,30,10} --> must be in descendant order
 * swordA, swordB, swordC
 *
 * randNum = 49 --> is 49<60 ...y... swordA
 * randNum = 68 --> is 68 < 60 ... n --> 68 - 60 = 8
 * is 8<30 ... y ... swordB
 *
 * randNum = 95 --> is 95<60 ..n.. 35<30 ..n..
 * 5<10 ..y.. swordC
 */
/* Enemies random probability weights
* aggresive = 5
* basic 40
* pickup destroyer 15
* shielded 20
* shotAvoider 5
* shotBackwards 10
* zigzag 5
*/
/* EnemyWaveRoutine2() for enemies except boss*/

namespace Managers
{
public class SpawnManager : SingletonBP<SpawnManager>
{
    #region variables

    [SerializeField] private List<GameObject> powerups;
    [SerializeField] private List<GameObject> enemies;
    [SerializeField] private List<WaveData> waves = new List<WaveData>();
    [SerializeField] private List<WaveNumData> waveNums = new List<WaveNumData>();
    [SerializeField] private Transform enemySpawner;
    [SerializeField] private float randPowerupNumber;
    public int randEnemyNumber;
    [SerializeField] private int currentWaveIndex;
    [SerializeField] private int currentWaveNumber;
    [SerializeField] private int currentWaveNumIndex;
    [SerializeField] private bool isPlayerAlive;

    public GameObject thisNewEnemy;
    public GameObject thisNewPowerup;

    #endregion

    public static event Action<int> OnWaveStarted;

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
        StopAllCoroutines();
    }

    private void SetPlayerAlive() => isPlayerAlive = true;
    public void SetPlayerDestroyed() => isPlayerAlive = false;

    private void StartSpawning()
    {
        StartCoroutine(SpawnPowerupRoutine());
        StartCoroutine(EnemyWaveRoutine1());
        //StartCoroutine(EnemyWaveRoutine2());
    }

    private GameObject SetNewPowerup()
    {
        randPowerupNumber = Random.value;
        GameObject randPowerup = null;

        if (randPowerupNumber < 0.2) randPowerup = powerups[0]; // Triple shoot
        if (randPowerupNumber >= 0.2 && randPowerupNumber < 0.3) randPowerup = powerups[1]; // Speed boost
        if (randPowerupNumber >= 0.3 && randPowerupNumber < 0.4) randPowerup = powerups[2]; // Shield
        if (randPowerupNumber >= 0.4 && randPowerupNumber < 0.7) randPowerup = powerups[3]; // Refill Ammo
        if (randPowerupNumber >= 0.7 && randPowerupNumber < 0.85) randPowerup = powerups[4]; // Health
        if (randPowerupNumber >= 0.85 && randPowerupNumber < 0.9) randPowerup = powerups[5]; // Damage pickup
        if (randPowerupNumber >= 0.9 && randPowerupNumber < 0.95) randPowerup = powerups[6]; // MultiShot
        if (randPowerupNumber >= 0.95) randPowerup = powerups[7]; // Laser MultiSeek

        if (randPowerup == null) Debug.LogError("Set a valid powerup in spawnManager");
        thisNewPowerup = randPowerup; // for testing
        return randPowerup;
    }

    private GameObject SetNewEnemy()
    {
        // See notes above this script's name
        var newRandNum = Random.Range(1, 101);
        randEnemyNumber = newRandNum; // for testing
        var randTable = new[] {5, 40, 15, 20, 5, 10, 5}; //run from left to right in the for loop

        for (int i = 0; i < randTable.Length; i++)
        {
            if (newRandNum < randTable[i])
            {
                Debug.Log("randNum; " + newRandNum);
                Debug.Log("randTable - i  " + randTable[i]);
                Debug.Log("enemy returned: " + enemies[i]
                    .name);
                thisNewEnemy = enemies[i]; // for testing
                return enemies[i];
            }

            Debug.Log("randNum; " + newRandNum);
            Debug.Log("randTable - i  " + randTable[i]);
            newRandNum -= randTable[i];
            Debug.Log("new randNum: " + newRandNum);
        }

        Debug.LogError("enemy is null in spawn manager");
        return null;
    }

    private IEnumerator SpawnPowerupRoutine()
    {
        yield return new WaitForSeconds(3f);

        while (isPlayerAlive)
        {
            var powerupPos = new Vector2(Random.Range(-9.5f, 9.5f), 5f);
            var newPowerup = SetNewPowerup();

            Instantiate(newPowerup, powerupPos, Quaternion.identity);
            yield return new WaitForSeconds(2);
        }
    }

    private IEnumerator EnemyWaveRoutine1()
    {
        yield return new WaitForSeconds(3);

        while (isPlayerAlive)
        {
            currentWaveNumber = currentWaveIndex + 1;
            OnWaveStarted?.Invoke(currentWaveNumber);
            var currentWave = waves[currentWaveIndex]
                .waveGameObjects; // Get objects list to instantiate
            var previousWave =
                new GameObject("PreviousWave"); // Create a new parent to be removed after wave is finished
            previousWave.transform.SetParent(enemySpawner);

            foreach (var obj in currentWave)
            {
                var newEnemy = Instantiate(obj, previousWave.transform); // Instantiate obj in PreviousWave
                if (newEnemy.gameObject.name == "EnemyBoss(Clone)")
                {
                    //newEnemy.transform.position = new Vector2(0, 0);
                    yield return new WaitForSeconds(60);
                }
                else
                {
                    newEnemy.transform.position = new Vector2(Random.Range(-9.5f, 9.5f), 5f);
                    //yield return new WaitForSeconds(Random.Range(2, 5));
                    yield return new WaitForSeconds(2);
                }
            }

            //yield return new WaitForSeconds(5); // wait 5 seconds after wave is done
            yield return new WaitForSeconds(10); // wait 5 seconds after wave is done

            Destroy(previousWave); // clear up wave objects
            currentWaveIndex++;

            if (currentWaveIndex != waves.Count) continue;
            break; // get out from while loop
        }
    }

    private IEnumerator EnemyWaveRoutine2()
    {
        yield return new WaitForSeconds(3);

        while (isPlayerAlive)
        {
            currentWaveNumber = currentWaveNumIndex + 1;
            OnWaveStarted?.Invoke(currentWaveNumber);

            var objAmount = waveNums[currentWaveNumIndex]
                .objectsAmount; // Get amount of objects to instantiate
            var previousWave =
                new GameObject("PreviousWave"); // Create a new parent to be removed after wave is finished
            previousWave.transform.SetParent(enemySpawner);

            for (int i = objAmount; i > 0; i--)
            {
                var randEnemy = SetNewEnemy();

                var newEnemy = Instantiate(randEnemy, previousWave.transform); // Instantiate randEnemy in PreviousWave
                newEnemy.transform.position = new Vector2(Random.Range(-9.5f, 9.5f), 5f);

                yield return new WaitForSeconds(Random.Range(2, 5));
            }

            yield return new WaitForSeconds(5); // wait 5 seconds after wave is done
            Destroy(previousWave); // clear up wave objects
            currentWaveNumIndex++;

            if (currentWaveNumIndex != waveNums.Count) continue;
            break; // get out from while loop
        }
    }

}
}
