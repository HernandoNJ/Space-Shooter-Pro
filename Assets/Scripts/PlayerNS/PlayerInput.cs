using System;
using System.Collections;
using System.Collections.Generic;
using EnemyNS;
using Managers;
using PowerupNS;
using UnityEngine;

namespace PlayerNS{
public class PlayerInput : MonoBehaviour{

	[SerializeField] private float playerSpeed;
	[SerializeField] private float speedMultiplier;
	[SerializeField] private bool bossActive;

	public static event Action OnFireActive;

	private void OnEnable()
	{
		SpawnManager.OnWaveStarted += IncreasePlayerSpeed;
		Powerup.OnMovementPowerupCollected += UpdateSpeed;
		playerSpeed = GetComponent<Player>().playerData.speed;
		EnemyBoss.OnBossStarted += SetBossActive;
		EnemyBoss.OnBossDestroyed += SetBossInactive;
	}

	private void OnDisable()
	{
		SpawnManager.OnWaveStarted -= IncreasePlayerSpeed;
		Powerup.OnMovementPowerupCollected -= UpdateSpeed;
		EnemyBoss.OnBossStarted -= SetBossActive;
		EnemyBoss.OnBossDestroyed -= SetBossInactive;

	}

	private void SetBossActive(int intArg)
	{
		bossActive = true;
		StartCoroutine(SpeedForBossWaveRoutine());
	}

	private void SetBossInactive()
	{
		bossActive = false;
		Debug.Log("Stopping SpeedForBossWaveRoutine");
		StopCoroutine(SpeedForBossWaveRoutine());
	}


	private void Update()
	{
		MovePlayer();
		Fire();
	}

	private void MovePlayer()
	{
		var xPos = transform.position.x + Input.GetAxis("Horizontal") * playerSpeed * Time.deltaTime;
		var yPos = transform.position.y +Input.GetAxis("Vertical") * playerSpeed * Time.deltaTime;

		xPos = xPos > 9.5f ? -9.5f : xPos;
		xPos = xPos < -9.5f ? 9.5f : xPos;

		var yMove = Mathf.Clamp(yPos, -4,4);

		transform.position = new Vector3(xPos, yMove, 0);
	}

	private void Fire()
	{
		if (Input.GetKeyDown(KeyCode.Space))
			OnFireActive?.Invoke();
	}

	private void IncreasePlayerSpeed(int speedBooster)
	{
		playerSpeed += speedBooster * 1.2f;
	}

	private void UpdateSpeed(float speedMultiplierArg)
	{
		speedMultiplier = speedMultiplierArg;
		playerSpeed *= speedMultiplier;
		StartCoroutine(SpeedMultiplierRoutine());
	}

	private IEnumerator SpeedMultiplierRoutine()
	{
		yield return new WaitForSeconds(3);
		playerSpeed /= speedMultiplier;
		speedMultiplier = 0;
	}

	private IEnumerator SpeedForBossWaveRoutine()
	{
		while (bossActive)
		{
			playerSpeed *= 3;
			yield return new WaitForSeconds(3);
			playerSpeed /= 3;
			yield return new WaitForSeconds(2);
		}
	}
}
}
