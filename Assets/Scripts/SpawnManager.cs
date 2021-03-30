using System.Collections;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private GameObject enemySpawnner;

    private bool isPlayerAlive = true;

    private void Start()
    {
        StartCoroutine(SpawnEnemies());
    }

    IEnumerator SpawnEnemies()
    {
        while (isPlayerAlive)
        {
            float xRandomPos = Random.Range(-9.5f, 9.5f);
            Vector3 enemySpawnPos = new Vector3(xRandomPos, 5f, 0f);
            
            GameObject newEnemy = Instantiate(enemyPrefab, enemySpawnPos, Quaternion.identity);
            newEnemy.transform.parent = enemySpawnner.transform;

            yield return new WaitForSeconds(0.5f);
        }
    }

    public void OnPlayerDestroyed()
    {
        isPlayerAlive = false;
    }
}