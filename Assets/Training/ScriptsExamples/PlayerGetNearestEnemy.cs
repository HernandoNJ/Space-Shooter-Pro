using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Training.ScriptsExamples
{
public class PlayerGetNearestEnemy : MonoBehaviour
{
    /* Unity3D Rants - Using LINQ in Unity - https://www.youtube.com/watch?v=1MEIAGoxafE
     * Class name is Player in video
     */

    public EnemyExample nearestEnemy;
    public EnemyExample[] allEnemies;

    private void Awake()
    {
        allEnemies = FindObjectsOfType<EnemyExample>();
    }

    private void OnEnable()
    {
        nearestEnemy = FindNearestEnemy();
        var enemiesMoreThan100 = allEnemies.Where(t => t.Health>100); // it's just the declaration, it doesn't do anything
        //var enemiesWithOne = allEnemies.Where(t => t.gameObject.name.Contains("1"));

        foreach (var tEnemy in enemiesMoreThan100)
        {

        }
    }

    private void Update()
    {
        nearestEnemy = FindNearestEnemy();
    }

    private EnemyExample FindNearestEnemy()
    {
        return allEnemies.OrderBy(t => Vector3.Distance(transform.position, t.transform.position))
            .FirstOrDefault();
    }

    private List<EnemyExample> GetNearestEnemies(int count)
    {
        return allEnemies.OrderBy(t => Vector3.Distance(transform.position, t.transform.position))
            .Take(count)
            .ToList();
    }
}
}
