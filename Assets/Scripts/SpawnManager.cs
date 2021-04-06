using System.Collections;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private GameObject enemySpawnner;
    [SerializeField] private GameObject[] powerups;

    private bool isPlayerAlive = true;

    public void StartSpawnning()
    {
        StartCoroutine(SpawnEnemiesRoutine());
        StartCoroutine(SpawnPowerupRoutine());
    }

    IEnumerator SpawnEnemiesRoutine()
    {
        yield return new WaitForSeconds(2f);

        while (isPlayerAlive)
        {
            float xRandomPos = Random.Range(-9.5f, 9.5f);
            Vector2 enemySpawnPos = new Vector2(xRandomPos, 5f);

            GameObject newEnemy = Instantiate(enemyPrefab, enemySpawnPos, Quaternion.identity);
            newEnemy.transform.parent = enemySpawnner.transform;

            yield return new WaitForSeconds(4f);
        }
    }

    IEnumerator SpawnPowerupRoutine()
    {
        yield return new WaitForSeconds(4f);

        while (isPlayerAlive)
        {
            Vector2 powerupPos = new Vector2(Random.Range(-9.5f, 9.5f), 5f);
            int randomPowerup = Random.Range(0, 4); // Modify value to 4
            Instantiate(powerups[randomPowerup], powerupPos, Quaternion.identity);
            yield return new WaitForSeconds(Random.Range(4,7));
        }
    }

    public void OnPlayerDestroyed()
    {
        isPlayerAlive = false;
    }
}