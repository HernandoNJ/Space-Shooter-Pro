using UnityEngine;

namespace ScriptableObjects.Inventory.Weapon
{
[CreateAssetMenu(fileName = "WeaponData1", menuName = "ScriptableObject/Inventory/WeaponData1")]
public class WeaponData1 : ScriptableObject
{
    public string weaponName;
    public string description;
    public float fireForce;
    public float timeAlive;
    public GameObject weaponPrefab;

    public void GetFirePoint(GameObject firePoint)
    {
        
    }
}
}
