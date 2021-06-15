using System;
using System.Collections.Generic;
using Powerups;
using UnityEngine;
using static PowerupType;

namespace Weapon
{
	public class WeaponsManager: MonoBehaviour
	{
		[SerializeField] private int weaponIndex;
		[SerializeField] private float fireRateValue;
		[SerializeField] private GameObject[] firePoints;
		[SerializeField] private List<GameObject> weapons;

		private void OnEnable() => Powerup.OnWeaponPowerupCollected += SetFireWeapon;
		private void OnDisable() => Powerup.OnWeaponPowerupCollected -= SetFireWeapon;
		 // **** todo 1
		// **** todo define fire methods implementation
		// **** todo define how to fire each weapon (if?switch?enum?)
		// **** todo increase fireRate with wave value
		

		private void SetFireWeapon(PowerupType powerupType)
		{
			switch (powerupType)
			{
				case TripleLaser: FireTripleLaser(); break;
				case MultipleShot: FireMultipleShot(); break;
			}
		}

		private float SetFireRate(int indexArg)
		{
			weaponIndex = indexArg; 
			fireRateValue = weapons[indexArg].gameObject.GetComponent<WeaponData>().fireRate;
			return fireRateValue;
		}

		private void FireLaser()
		{
		   SetFireRate(0);
		   // instantiate laser
			
		}
		
		private void FireTripleLaser()
		{
			SetFireRate(0);
			// instantiate triple laser
			
			
		}
		
		private void FireMultipleShot()
		{
		
		}

		private void FireWeapon()
		{
				
		}
		




		//
		//	//[SerializeField] private List<WeaponsListData> weaponsListData = new List<WeaponsListData>();
		// [SerializeField] private bool isTripleLaserActive;
		// [SerializeField] private WeaponsListData weaponList;
		// [SerializeField] private int laserIndex;
		// [SerializeField] private int rocketIndex;
		//SerializeField] private float canShoot = 0;
		//[SerializeField] private bool hasAmmo;
		// [SerializeField] private float fireRate;
		
				
		// Fire weapon procedure
		// Check input
		// if input is spacekey, shoot laser(weaponType)
		//	weapontype = laser
		//		set firepoint, instantiate laser
		//	weapontype = tripleLaser
		//		set firepoints, instantiate lasers
		//	else shootLaser
		
		
		
		
		
		// private void OnEnable()
		// {
		// 	//Player.PlayerInput.OnFireActive += FireWeapons;
		// 	PlayerNS.Player.OnTripleLaserActivated += ShootTripleLaser;
		// }

		// private void Start()
		// {
		// 	laserIndex = 0;
		// 	rocketIndex = 1;
		// 	var laserweaponList = weaponsListData.
		// 	var x = weaponList.weapons[0].gameObject.GetComponent<WeaponData>().fireRate;
		// 	//for (int weao = 0; weao < UPPER; weao++)
		// 	{
		// 		//weaponData1[weaponIndex].GetFirePoint(firePoints[weaponIndex]);
		// 	}
		//
		// }
		//
		// private void ShootTripleLaser(WeaponType weaponType)
		// {
		// 	//isTripleLaserActive = tripleLaserEnabled;
		// 	StartCoroutine(TripleLaserRoutine());
		// }

		


		//
		// private IEnumerator TripleLaserRoutine()
		// {
		// 	yield return new WaitForSeconds(5f);
		// 	isTripleLaserActive = false;
		// }
		//


//private bool FiringActive()
		// {
		// 	return Input.GetKeyDown(KeyCode.Space) && Time.time > canShoot && hasAmmo;
		// }
		//
		// private bool IncreaseSpeed()
		// {
		// 	return Input.GetKey(KeyCode.LeftShift);
		// }
		//
		// private void FireLaser()
		// {
		// 	// Fire MultipleShot
		// 	if(isMultipleShotActive)
		// 	{
		// 		canShoot = Time.time + MultipleShotFireRate;
		// 		Instantiate(multipleShotPrefab, firepoint.transform.position, Quaternion.identity);
		// 		isMultipleShotActive = false;
		// 	}
		// 	// Shot triple laser
		// 	else if(isTripleLaserActive)
		// 	{
		// 		canShoot = Time.time + fireRate;
		// 		Instantiate(tripleLaserPrefab, transform.position, Quaternion.identity);
		// 	}
		// 	// Shot one laser
		// 	else
		// 	{
		// 		canShoot = Time.time + fireRate;
		// 		Vector2 laserPos = transform.position + new Vector3(0, 1.0f);
		// 		Instantiate(laserPrefab, laserPos, Quaternion.identity);
		// 	}
		//
		// 	ammoAvailable--;
		// 	audioSource.Play();
		//
		// 	UpdateAmmo();
		// }
		//
		// public void Damage(int damage)
		// {
		// 	lives -= damage;
		// 	UpdateLives();
		// 	UpdateDamage();
		//
		// 	if(lives == 0)
		// 	{ Destroy(gameObject); }
		// }
		//
		// public void UpdateDamage()
		// {
		// 	if(isShieldActive)
		// 	{ ChangeShieldColor(); }
		// 	else
		// 	{
		// 		if(lives == 3)
		// 		{
		// 			leftEngine.SetActive(false);
		// 			rightEngine.SetActive(false);
		// 		}
		// 		if(lives == 2)
		// 		{
		// 			leftEngine.SetActive(true);
		// 			rightEngine.SetActive(false);
		// 		}
		// 		else if(lives == 1)
		// 		{
		// 			leftEngine.SetActive(true);
		// 			rightEngine.SetActive(true);
		// 		}
		// 	}
		// }
		//
		// public void UpdateLives()
		// {
		// 	uiManager.UpdateLives(lives);
		// }
		//
		// public void AddScore(int points)
		// {
		// 	score += points;
		// 	uiManager.UpdateScore(score);
		// }
		//
		// #endregion
		//
		// #region Activate powerups mechanics
		// public void ActivateTripleLaser()
		// {
		// 	isTripleLaserActive = true;
		// 	StartCoroutine(TripleLaserRoutine());
		// }
		//
		// IEnumerator TripleLaserRoutine()
		// {
		// 	yield return new WaitForSeconds(5f);
		// 	isTripleLaserActive = false;
		// }
		//
		// public void ActivateMultipleShot()
		// {
		// 	isMultipleShotActive = true;
		// 	ammoAvailable++;
		// 	UpdateAmmo();
		// }
		//
		// public void ActivateSpeedBoost()
		// {
		// 	isSpeedBoostActive = true;
		// 	StartCoroutine(SpeedBoostRoutine());
		// }
		//
		// IEnumerator SpeedBoostRoutine()
		// {
		// 	yield return new WaitForSeconds(5f);
		// 	isSpeedBoostActive = false;
		// }
		//
		// public void ActivateShield()
		// {
		// 	isShieldActive = true;
		// 	shield.SetActive(true);
		// 	lives = 3;
		// 	ChangeShieldColor();
		// 	UpdateLives();
		// }
		//
		// public void ChangeShieldColor()
		// {
		// 	switch(lives)
		// 	{
		// 		case 3:
		// 			shield.GetComponent<SpriteRenderer>().color = new Color(0f, 1f, 1f, 0.7f);
		// 			break;
		// 		case 2:
		// 			shield.GetComponent<SpriteRenderer>().color = new Color(1f, 0.5f, 0f, 0.7f);
		// 			leftEngine.SetActive(true);
		// 			break;
		// 		case 1:
		// 			shield.GetComponent<SpriteRenderer>().color = new Color(1f, 0.2f, 0f, 0.6f);
		// 			rightEngine.SetActive(true);
		// 			break;
		// 		case 0:
		// 			spawnManager.OnPlayerDestroyed();
		// 			Destroy(gameObject);
		// 			break;
		// 	}
		// }
		//
		// public void UpdateAmmo()
		// {
		// 	if(ammoAvailable > ammoMax)
		// 	{ ammoAvailable = ammoMax; }
		//
		// 	uiManager.UpdateAmmo(ammoAvailable, ammoMax);
		//
		// 	hasAmmo = (ammoAvailable >= 1) ? true : false;
		//
		// 	if(!hasAmmo)
		// 	{
		// 		uiManager.UpdateAmmo(ammoAvailable, ammoMax);
		// 		uiManager.OnEmptyAmmo();
		// 	}
		// }
		//
		// public void RefillAmmo()
		// {
		// 	ammoAvailable = 15;
		// 	UpdateAmmo();
		//
		// 	uiManager.OnFullAmmo();
		// }
		//
		// public void RecoverHealth()
		// {
		// 	if(lives >= 3) return;
		// 	lives++;
		// 	UpdateLives();
		// 	UpdateDamage();
		// }


		/*
		 * Define in player one shot, triple shot and rocket
		 * raise event
		 * listen to event here
		 *
		 * SPAWN MANAGER
		 * raise event for wave number
		 * listen to event here
		 * create a variable weaponsSpeedMultiplier
		 * set its value to waveNumber
		 */

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
