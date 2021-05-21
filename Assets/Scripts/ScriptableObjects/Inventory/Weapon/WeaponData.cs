using UnityEngine;

[CreateAssetMenu(fileName = "WeaponData", menuName = "ScriptableObject/Inventory/WeaponData")]
public class WeaponData : ScriptableObject
{
    public string weaponName;
    public string description;
    public string owner;
    public string direction;
    public float speed;
    public int damageAmount;
    public GameObject weapon;
 
}
