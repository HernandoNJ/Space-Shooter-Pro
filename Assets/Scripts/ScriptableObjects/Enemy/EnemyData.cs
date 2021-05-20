
using System.Collections.Generic;
using UnityEngine;
public enum EnemyType { Default, Basic, DoubleShooter, Shielded, Aggressive, Chaser, BackShooter, ShotAvoider, Boss }

[CreateAssetMenu(fileName = "EnemyData", menuName = "ScriptableObject/EnemySO/EnemyData", order = 0)]
public class EnemyData : ScriptableObject
{
    public string enemyName;
    public string description;
    public float speed;
    public float fireRate;
    public int maxHealth;
    public int updatedHealth;
    public EnemyType enemyType;
    public ModelData modelData;
    public GameObject weapon;
    public List<GameObject> weapons = new List<GameObject>();

    public GameObject explosionPrefab;

}
