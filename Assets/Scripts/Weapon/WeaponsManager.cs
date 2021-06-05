using System;
using System.Collections.Generic;
using ScriptableObjects.Inventory.Weapon;
using UnityEngine;

public enum WeaponType{Laser,TripleLaser,Rocket}

namespace Weapon
{
public class WeaponsManager : MonoBehaviour
{
    [SerializeField] private int weaponIndex;
    [SerializeField] private GameObject[] firePoints;
    [SerializeField] private List<WeaponsListData> weaponsListData = new List<WeaponsListData>();
            
    // which weapon to use
    // scriptable object: weapon functionality 
    // listen to player input event

    public void SetOneLaserPosition()
    {
        // Check event with 
        // 
    }
    
    
    
    public void GetFirePoint(GameObject firePoint)
    {
        //weaponPrefab.transform.position = firePoint.transform.position;
    }
    
    
    private void Start()
    {
        //for (int weao = 0; weao < UPPER; weao++)
        {
            //weaponData1[weaponIndex].GetFirePoint(firePoints[weaponIndex]);
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
