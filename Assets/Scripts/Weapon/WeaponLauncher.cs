using ScriptableObjects.Inventory.Weapon;
using UnityEngine;

namespace Weapon
{
public class WeaponLauncher:MonoBehaviour 
{
    public WeaponData weaponData;
    [SerializeField] private string weaponName;
    [SerializeField] private GameObject weaponPrefab;
    [SerializeField] private Transform firePoint;
    [SerializeField] private float fireForce;
    
    private void OnEnable()
    {
        GetComponentInParent<Player.PlayerInput>().OnFireActive += HandleFire;
        fireForce = weaponData.fireForce;
        weaponName = weaponData.weaponName;
    }

    private void HandleFire()
    {
        var spawnedWeapon = Instantiate(weaponPrefab, firePoint.position, firePoint.rotation);
        spawnedWeapon.GetComponent<Rigidbody2D>().AddForce(
            spawnedWeapon.transform.up * fireForce, ForceMode2D.Impulse);
        Destroy(spawnedWeapon, 2.0f);
    }
}
}


