using System.Collections.Generic;
using UnityEngine;
using Weapon;

namespace Enemy
{
[CreateAssetMenu(fileName = "EnemyData", menuName = "ScriptableObject/Enemy/EnemyData", order = 0)]
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

public enum EnemyType { Default, Basic, DoubleShooter, Chaser, Aggressive, 
ShotAvoider, Shielded, Boss }
}