using UnityEngine;

namespace ScriptableObjects.Inventory.Weapon {
[CreateAssetMenu(fileName = "WeaponData", menuName = "ScriptableObject/Inventory/WeaponData")]
public class WeaponData : ScriptableObject
{
    public string weaponName;
    public string description;
    public float speed;
    public int damageAmount;
    public Sprite model;
}
}