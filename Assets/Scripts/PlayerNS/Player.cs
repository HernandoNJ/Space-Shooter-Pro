using System;
using UnityEngine;

namespace PlayerNS{
public class Player : MonoBehaviour{

	public PlayerData playerData;

	private void Start()
	{
		// Set default position
		transform.position = new Vector3(0, -3);
	}

}
}
