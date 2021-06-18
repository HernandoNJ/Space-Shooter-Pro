using System;
using System.Collections;
using System.Collections.Generic;
using Powerups;
using UnityEngine;
using Weapon.Lasers;
using static PowerupType;
using PlayerInput = PlayerNS.PlayerInput;

// **** todo increase fireRate with wave value
// **** todo OnAmmoChanged to ui manager
/* SPAWN MANAGER
* raise event for wave number
* listen to event here
* create a variable weaponsSpeedMultiplier
* set its value to waveNumber
*/

namespace Weapon
{
public class WeaponsManager : MonoBehaviour
{
    [SerializeField] private Transform[] firePoints;
    [SerializeField] private List<GameObject> weapons;
    //[SerializeField] private bool[] activeWeapons;
    [SerializeField] private bool hasAmmo;
    [SerializeField] private float fireRate;
    [SerializeField] private float laserFireRate;
    [SerializeField] private float tripleLaserFireRate;
    [SerializeField] private float multiShotFireRate;
    [SerializeField] private int ammoMax;
    [SerializeField] private int ammoCount;
    [SerializeField] private int ammoRefill;
    [SerializeField] private float time_Time;
    [SerializeField] private float timeToNextShoot;
    [SerializeField] private int laserIndex;
    [SerializeField] private int tripleLaserIndex;
    [SerializeField] private int multishotIndex;
    [SerializeField] private bool laserIsActive;
    [SerializeField] private bool tripleLaserIsActive;
    [SerializeField] private bool multipleShotIsActive;

    public static event Action<int,int> OnAmmoChanged;

    private void OnEnable()
    {
        Powerup.OnWeaponPowerupCollected += PowerupUpdate;
        PlayerInput.OnFireActive += FireWeapon;

        // activeWeapons[0] = laserIsActive;
        // activeWeapons[1] = tripleLaserIsActive;
        // activeWeapons[2] = multipleShotIsActive;
    }

    private void OnDisable()
    {
        Powerup.OnWeaponPowerupCollected -= PowerupUpdate;
        StopAllCoroutines();
    }

    private void Start()
    {
        laserIndex = 0;
        tripleLaserIndex = 1;
        multishotIndex = 2;

        laserFireRate = weapons[laserIndex].gameObject.GetComponent<LaserPlayer>().weaponData.fireRate;
        //laserIsActive = true;
        //SetActiveWeapon(laserIsActive); // todo ask Austin

        //SetActiveWeapon2(laserIndex);

        tripleLaserFireRate = laserFireRate;
        multiShotFireRate = 5;

        SetActiveWeapon3(laserIndex);
        ammoMax = 15;
        ammoRefill = 15;

        UpdateAmmo(15);
    }

    private void Update()
    {
        time_Time = Time.time;
    }

    private void UpdateAmmo(int newAmmoAmount)
    {
        ammoCount += newAmmoAmount;
        if (ammoCount > ammoMax) ammoCount = ammoMax;
        hasAmmo = ammoCount > 0;
        OnAmmoChanged?.Invoke(ammoCount, ammoMax);
    }

    private void PowerupUpdate(PowerupType powerupType)
    {
        switch (powerupType)
        {
            case RefillAmmo:
                UpdateAmmo(ammoRefill);
                break;
            case TripleLaser:
                StartCoroutine(TripleLaserRoutine());
                //SetActiveWeapon(tripleLaserIsActive);
                break;
            case MultipleShot:
                SetActiveWeapon3(multishotIndex);
                //SetActiveWeapon(multipleShotIsActive);
                break;
        }
    }

    // private void SetActiveWeapon(bool weaponToActivate)
    // {
    // 	var weaponsArray = new bool[3];
    // 	weaponsArray[0] = laserIsActive;
    // 	weaponsArray[1] = tripleLaserIsActive;
    // 	weaponsArray[2] = multipleShotIsActive;
    //
    // 	for (int i = 0; i < weaponsArray.Length; i++) // todo ask Austin
    // 	{
    // 		if (weaponsArray[i] != weaponToActivate)
    // 			weaponsArray[i] = false;
    // 	}
    // }

    // private void SetActiveWeapon2(int weaponIndex)
    // {
    // 	for (int i = 0; i < activeWeapons.Length; i++)
    // 	{
    // 		if (i != weaponIndex) activeWeapons[i] = false;
    // 	}
    // }

    private void SetActiveWeapon3(int weaponIndex)
    {
        switch (weaponIndex)
        {
            case 0:
                laserIsActive = true;
                tripleLaserIsActive = false;
                multipleShotIsActive = false;
                break;
            case 1:
                laserIsActive = false;
                tripleLaserIsActive = true;
                multipleShotIsActive = false;
                break;
            case 2:
                laserIsActive = false;
                tripleLaserIsActive = false;
                multipleShotIsActive = true;
                break;
            default:
                Debug.Log("Configure weapon Index");
                break;
        }
    }

    private bool FireEnabled()
    {
        if (!(Time.time > timeToNextShoot) || !hasAmmo) return false;
        timeToNextShoot = fireRate + Time.time;
        return true;
    }

    private void FireWeapon()
    {
        if (!FireEnabled()) return;

        if (laserIsActive) FireLaser(laserFireRate);
        else if (tripleLaserIsActive) FireTripleLaser(laserFireRate);
        else if (multipleShotIsActive) FireMultiShot(multiShotFireRate);
        else Debug.Log("configure weapon to be fired");
    }

    private void FireLaser(float fireRateArg)
    {
        fireRate = fireRateArg;
        Instantiate(weapons[laserIndex], firePoints[0].position, Quaternion.identity);
        UpdateAmmo(-1);
    }

    private void FireTripleLaser(float fireRateArg)
    {
        StartCoroutine(TripleLaserRoutine());
        fireRate = fireRateArg;
        var laser = weapons[tripleLaserIndex];
        var lasers = new GameObject[3];
        for (int i = 0; i < lasers.Length; i++)
        {
            Instantiate(laser, firePoints[i].position, Quaternion.identity);
        }

        UpdateAmmo(-1);
    }

    private IEnumerator TripleLaserRoutine()
    {
        SetActiveWeapon3(tripleLaserIndex);
        yield return new WaitForSeconds(5);
        SetActiveWeapon3(laserIndex);
        // StopCoroutine(tripleLaserRoutine()); // todo ask Austin
        //SetActiveWeapon(laserIsActive);
    }

    private void FireMultiShot(float fireRateArg)
    {
        fireRate = fireRateArg;
        Instantiate(weapons[multishotIndex], firePoints[0].transform.position, Quaternion.identity);
        SetActiveWeapon3(laserIndex);
        //SetActiveWeapon(laserIsActive);
    }
}
}
