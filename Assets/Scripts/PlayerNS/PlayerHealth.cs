using System;
using Interfaces;
using Powerups;
using static PowerupType;
using UnityEngine;

namespace PlayerNS
{
public class PlayerHealth : MonoBehaviour, IDamageable
{
    public PlayerData playerData;
    [SerializeField] private int maxHealth = 3;
    [SerializeField] private int currentHealth;
    [SerializeField] private PlayerShield playerShield;

    public static event Action OnPlayerDie; // todo send to UIManager and game manager for game over
    public static event Action<int> OnHealthChanged;

    private void OnEnable()
    {
        Powerup.OnHealthPowerupCollected += PowerupHealth;

        playerShield = GetComponent<PlayerShield>();
        playerData = GetComponent<Player>().playerData;
        maxHealth = playerData.maxHealth;
        currentHealth = maxHealth;
    }

    private void OnDisable()
    {
        Powerup.OnHealthPowerupCollected -= PowerupHealth;
    }

    private void PowerupHealth(PowerupType powerUpArg)
    {
        switch (powerUpArg)
        {
            case RecoverHealth:
                currentHealth += 1;
                HealthChanged();
                break;
            case DamagePickup:
                currentHealth -= 2;
                HealthChanged();
                break;
        }
    }

    public void TakeDamage(int damage)
    {
        if (playerShield.IsShieldActive())
        {
            playerShield.DamageShield(1);
            return;
        }

        currentHealth -= damage;
        if(currentHealth >0) HealthChanged();
        else Die();
    }

    private void Die()
    {
        OnPlayerDie?.Invoke();
        Destroy(gameObject);
    }

    private void HealthChanged()
    {
        if (currentHealth > maxHealth) currentHealth = maxHealth;
        OnHealthChanged?.Invoke(currentHealth);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        var iDamage = other.GetComponent<IDamageable>();
        if (iDamage == null || !other.CompareTag("Enemy")) return;
        iDamage.TakeDamage(1);
        TakeDamage(1);
    }
}
}
