using System;
using System.Collections;
using System.Collections.Generic;
using Powerups;
using UnityEngine;
using Weapon.Lasers;
using static PowerupType;
using PlayerInput = PlayerNS.PlayerInput;

namespace Weapon
{
public class WeaponsManager : MonoBehaviour
{
    [SerializeField] private Transform[] firePoints;
    [SerializeField] private List<GameObject> weapons;
    [SerializeField] private bool hasAmmo;
    [SerializeField] private int ammoMax;
    [SerializeField] private int ammoCount;
    [SerializeField] private int ammoRefill;
    [SerializeField] private float fireRate;
    [SerializeField] private float fireBonus;
    [SerializeField] private float laserFireRate;
    [SerializeField] private float tripleLaserFireRate;
    [SerializeField] private float multiShotFireRate;
    [SerializeField] private float laserMultiSeekFireRate;
    [SerializeField] private int laserIndex;
    [SerializeField] private int tripleLaserIndex;
    [SerializeField] private int multishotIndex;
    [SerializeField] private int laserMultiSeekIndex;
    [SerializeField] private bool laserIsActive;
    [SerializeField] private bool tripleLaserIsActive;
    [SerializeField] private bool multipleShotIsActive;
    [SerializeField] private bool laserMultiSeekIsActive;
    [SerializeField] private float timeToNextShoot;
    [SerializeField] private float time_Time;
    [SerializeField] private int currentWaveValue;

    public static event Action<int, int> OnAmmoChanged;

    private void OnEnable()
    {
        Powerup.OnWeaponPowerupCollected += PowerupUpdate;
        Powerup.OnLaserMultiSeekPowerupCollected += PowerupUpdate;
        PlayerInput.OnFireActive += FireWeapon;
        //SpawnManager.OnWaveStarted += SetFireBonus;
    }

    private void OnDisable()
    {
        Powerup.OnWeaponPowerupCollected -= PowerupUpdate;
        Powerup.OnLaserMultiSeekPowerupCollected -= PowerupUpdate;

        PlayerInput.OnFireActive -= FireWeapon;
        //SpawnManager.OnWaveStarted -= SetFireBonus;
        StopAllCoroutines();
    }

    private void Start()
    {
        SetWeaponsValues();
    }

    private void Update()
    {
        time_Time = Time.time;
    }

    private void SetWeaponsValues()
    {
        laserIndex = 0;
        tripleLaserIndex = 1;
        multishotIndex = 2;
        laserMultiSeekIndex = 3;

        laserFireRate = weapons[laserIndex].gameObject.GetComponent<LaserPlayer>().weaponData.fireRate;
        tripleLaserFireRate = laserFireRate;
        multiShotFireRate = 5;
        laserMultiSeekFireRate = 5;

        SetActiveWeapon(laserIndex);
        ammoMax = 15;
        ammoRefill = 15;

        UpdateAmmo(15);
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
                break;
            case MultipleShot:
                SetActiveWeapon(multishotIndex);
                break;
            case LaserMultiSeekPickup:
                SetActiveWeapon(laserMultiSeekIndex);
                break;
        }
    }

    private void SetActiveWeapon(int weaponIndex)
    {
        switch (weaponIndex)
        {
            case 0:
                laserIsActive = true;
                tripleLaserIsActive = false;
                multipleShotIsActive = false;
                laserMultiSeekIsActive = false;
                break;
            case 1:
                laserIsActive = false;
                tripleLaserIsActive = true;
                multipleShotIsActive = false;
                laserMultiSeekIsActive = false;
                break;
            case 2:
                laserIsActive = false;
                tripleLaserIsActive = false;
                multipleShotIsActive = true;
                laserMultiSeekIsActive = false;
                break;
            case 3:
                laserIsActive = false;
                tripleLaserIsActive = false;
                multipleShotIsActive = false;
                laserMultiSeekIsActive = true;
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
        else if (laserMultiSeekIsActive) FireLaserMultiSeek(laserMultiSeekFireRate);
        else Debug.Log("configure weapon to be fired");
    }

    private void FireLaser(float fireRateArg)
    {
        fireRate = fireRateArg - fireBonus;
        Instantiate(weapons[laserIndex], firePoints[0].position, Quaternion.identity);
        UpdateAmmo(-1);
    }

    private void FireTripleLaser(float fireRateArg)
    {
        StartCoroutine(TripleLaserRoutine());

        fireRate = fireRateArg - fireBonus;
        var laser = weapons[tripleLaserIndex];
        var lasers = new GameObject[3];

        for (int i = 0; i < lasers.Length; i++) { Instantiate(laser, firePoints[i].position, Quaternion.identity); }

        UpdateAmmo(-1);
    }

    private IEnumerator TripleLaserRoutine()
    {
        SetActiveWeapon(tripleLaserIndex);
        yield return new WaitForSeconds(5);
        SetActiveWeapon(laserIndex);
    }

    private void FireMultiShot(float fireRateArg)
    {
        fireRate = fireRateArg;
        Instantiate(weapons[multishotIndex], firePoints[0].transform.position, Quaternion.identity);
        SetActiveWeapon(laserIndex);
    }

    private void FireLaserMultiSeek(float fireRateArg)
    {
        fireRate = fireRateArg;
        Instantiate(weapons[laserMultiSeekIndex], firePoints[0].transform.position, Quaternion.identity);
        SetActiveWeapon(laserIndex);
    }
}
}
