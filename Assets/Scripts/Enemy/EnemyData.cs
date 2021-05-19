
using System.Collections.Generic;
using UnityEngine;
public enum EnemyType { Default, Basic, DoubleShooter, Shielded, Aggressive, Chaser, BackShooter, ShotAvoider, Boss }

[CreateAssetMenu(fileName = "EnemyData", menuName = "EnemyData", order = 0)]
public class EnemyData : ScriptableObject
{

    public string enemyName;
    public string description;
    public float speed;
    public float fireRate;
    public int maxHealth;
    public bool hasShield;
    public EnemyType enemyType;
    public GameObject model;
    public GameObject weapon;
    public List<GameObject> weapons = new List<GameObject>();
    public int updatedHealth;

}
