using System;
using UnityEngine;
using Weapon;

namespace PlayerNS
{
public class Player : MonoBehaviour
{
    public PlayerData playerData;

    public static event Action OnPlayerActive;
    public static event Action<Transform> OnSendPlayerPosition;
    public static event Action<Collider2D> OnPlayerEnterTrigger;

    private void OnEnable()
    {
        RocketEnemy.OnLookingForPlayer += SendPlayerPosition;
    }

    private void OnDisable()
    {
        RocketEnemy.OnLookingForPlayer -= SendPlayerPosition;
    }

    private void SendPlayerPosition()
    {
        OnSendPlayerPosition?.Invoke(transform);
    }

    private void Start()
    {
        OnPlayerActive?.Invoke();
        transform.position = new Vector3(0, -3);  // Set default position
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        OnPlayerEnterTrigger?.Invoke(other);
    }
}


}
