using System.Collections.Generic;
using ScriptableObjects.Inventory.Weapon;
using UnityEngine;

public enum EnemyType { Default, Basic, DoubleShooter, Chaser, Aggressive, ShotAvoider, Shielded, Boss }

[CreateAssetMenu(fileName = "EnemyData", menuName = "ScriptableObject/EnemySO/EnemyData", order = 0)]
public class EnemyData : ScriptableObject
{
    public string enemyName;
    public EnemyType enemyType;
    public string description;
    public int scorePoints;
    public float speed;
    public float fireRate;
    public int collisionDamage;
    public int maxHealth;
    public GameObject weapon;
    public List<WeaponData> weapons = new List<WeaponData>();
    public GameObject explosionPrefab;
}
