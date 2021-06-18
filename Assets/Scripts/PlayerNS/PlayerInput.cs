using System;
using System.Collections;
using Powerups;
using UnityEngine;

namespace PlayerNS{
public class PlayerInput : MonoBehaviour{

	[SerializeField] private float speed;
	[SerializeField] private float speedMultiplier;
	
	public static event Action OnFireActive;

	private void OnEnable()
	{
		Powerup.OnMovementPowerupCollected += UpdateSpeed;
		speed = GetComponent<Player>().playerData.speed;
	}

	private void Update()
	{
		MovePlayer();
		Fire();
	}

	private void MovePlayer()
	{
		var moveH = Input.GetAxis("Horizontal");
		var moveV = Input.GetAxis("Vertical");
		transform.Translate(new Vector3(moveH,moveV) * (speed * Time.deltaTime));
		//transform.position += new Vector3(moveH,moveV,0) * (speed * Time.deltaTime);
	}

	private void Fire()
	{
		if (Input.GetKeyDown(KeyCode.Space))
			OnFireActive?.Invoke();
	}

	private void UpdateSpeed(float speedMultiplierArg)
	{
		speedMultiplier = speedMultiplierArg;
		speed *= speedMultiplier;
		StartCoroutine(SpeedMultiplierRoutine());
	}

	private IEnumerator SpeedMultiplierRoutine()
	{
		yield return new WaitForSeconds(3);
		speed /= speedMultiplier;
	}

}
}
