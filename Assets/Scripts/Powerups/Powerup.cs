using System;
using UnityEngine;
using static PowerupType;

public enum PowerupType { TripleLaser, SpeedBoost, Shield, RefillAmmo, RecoverHealth, MultipleShot, DamagePickup }

namespace Powerups
{
public class Powerup : MonoBehaviour
{
    [SerializeField] private AudioClip powerupSound;
    [SerializeField] private PowerupType powerupType;
    [SerializeField] private float speed = 2.5f;

    public static event Action<PowerupType> OnWeaponPowerupCollected;
    public static event Action<int> OnHealthPowerupCollected;
    public static event Action<int> OnMovementPowerupCollected;

    private void Update()
    {
        Move();

        if (transform.position.y < -6f)
            Destroy(gameObject);
    }

    private void Move()
    {
        transform.Translate(speed * Time.deltaTime * Vector2.down);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;
        AudioSource.PlayClipAtPoint(powerupSound, transform.position, 0.3f);
        SetPowerupInfo(powerupType);
        Destroy(gameObject);
    }
    
    private void SetPowerupInfo(PowerupType powerupInfo)
    {
        switch (powerupInfo)
        {
            case TripleLaser: case MultipleShot: case RefillAmmo:
                OnWeaponPowerupCollected?.Invoke(powerupInfo); break;
            case Shield:
                OnHealthPowerupCollected?.Invoke(3); break; 
            case RecoverHealth:
                OnHealthPowerupCollected?.Invoke(1); break; 
            case DamagePickup:
                OnHealthPowerupCollected?.Invoke(-2); break;
            case SpeedBoost: 
                OnMovementPowerupCollected?.Invoke(2); break;
            default:
                throw new ArgumentOutOfRangeException(nameof(powerupInfo), powerupInfo, null);
        }
    }
}
}
