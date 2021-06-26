using UnityEngine;

namespace Weapon
{
[CreateAssetMenu(fileName = "WeaponData", menuName = "ScriptableObject/Weapon/WeaponData")]
public class WeaponData : ScriptableObject
{
    public string weaponName;
    public string description;
    public string parentName;
    public float fireForce;
    public float fireRate;
    public int damage;
    public float timeAlive;
    
}
}
