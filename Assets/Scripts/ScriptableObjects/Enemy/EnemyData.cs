
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyData", menuName = "ScriptableObject/EnemySO/EnemyData", order = 0)]
public class EnemyData : ScriptableObject
{
    public string enemyName;
    public string description;
    public float speed;
    public float fireRate;
    public int collisionDamage;
    public int maxHealth;
    public ModelData modelData;
    public WeaponData weaponData;
    public List<GameObject> weapons = new List<GameObject>();
    public GameObject explosionPrefab;
}
