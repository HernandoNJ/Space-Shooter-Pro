using System;
using Powerups;
using UnityEngine;

namespace PlayerNS
{
public class Player : MonoBehaviour
{
    public PlayerData playerData;
    [SerializeField] private PlayerInput playerInput;

    public float Speed{ get; private set; }

    public static event Action<Collision2D> OnCollisionPlayer;
    // todo fix public static event Action<Collider2D> OnTriggerPlayer;
    // todo fix public static event Action<WeaponType> OnTripleLaserActivated;
    // todo check if collision with enemy works ok

    private void OnEnable()
    {
        playerInput = GetComponent<PlayerInput>();
        Powerup.OnMovementPowerupCollected += UpdateMoveSpeed;
    }

    private void UpdateMoveSpeed(int speedMultiplier)
    {
        // todo coroutine to apply speedMult for some time
        Speed *= speedMultiplier;
    }

    private void Start()
    {
        // Set default position
        transform.position = Vector3.down * 3;
        Speed = playerData.speed * Time.deltaTime;
    }

    private void Update()
    {
        MovePlayer(playerInput.Horizontal, playerInput.Vertical, 9, -3, 2, Speed);
    }

    private void MovePlayer(float xInput, float yInput, float xPosValue, float yPosMin, float yPosMax, float speed)
    {
        var xPos = transform.position.x;
        if (xPos > xPosValue) xPos = -xPosValue;
        if (xPos < -xPosValue) xPos = xPosValue;

        var yPos = Mathf.Clamp(transform.position.y, -yPosMin, yPosMax);

        transform.position += new Vector3(xInput * xPos * speed, yInput * yPos * speed, 0);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        OnCollisionPlayer?.Invoke(other);
    }




    // #region additional code
    // private bool FiringActive()
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
    //
    // #endregion

}
}
