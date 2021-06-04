using System;
using System.Collections.Generic;
using ScriptableObjects.Inventory.Weapon;
using UnityEngine;

namespace Weapon
{
public class WeaponManager1 : MonoBehaviour
{
    [SerializeField] private List<WeaponData1> weaponData1 = new List<WeaponData1>();
    [SerializeField] private GameObject[] firePoints;
    [SerializeField] private int weaponIndex;

    // which weapon to use
    // scriptable object: weapon functionality 
    // listen to player input event
    
    
    
    private void Start()
    {
        for (int weao = 0; weao < UPPER; weao++)
        {
            weaponData1[weaponIndex].GetFirePoint(firePoints[weaponIndex]);
        }
       
    }

    public void active1(int amountofweapons)
    {
        // fori while index <= amount weapons
    }

    public void activate()
    {
        // deactivate all arrays
        // activate 3 
    }

    // private void OnEnable()
    // {
    //     
    //     weaponData1[0].fireVector = 
    //     GetComponentInParent<Player.PlayerInput>().OnFireActive += HandleFire;
    //     fireForce = weaponData.fireForce;
    //     weaponName = weaponData.weaponName;
    // }
    //
    // private void HandleFire()
    // {
    //     
    //     var spawnedWeapon = Instantiate(weaponPrefab, firePoint.position, firePoint.rotation);
    //     spawnedWeapon.GetComponent<Rigidbody2D>().AddForce(
    //         spawnedWeapon.transform.up * fireForce, ForceMode2D.Impulse);
    //     Destroy(spawnedWeapon, 2.0f);
    // }
}
}
