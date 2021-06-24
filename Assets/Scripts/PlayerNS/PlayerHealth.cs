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
    [SerializeField] private PlayerShield playerShield;
    [SerializeField] private bool shieldActive;
    [SerializeField] private int maxHealth = 3;
    [SerializeField] private int currentHealth;

    public static event Action<int> OnHealthChanged;
    public static event Action OnPlayerDestroyed;

    private void OnEnable()
    {
        Powerup.OnHealthPowerupCollected += PowerupHealth;
        PlayerShield.OnShieldActiveChanged += CheckShieldActive;

        playerShield = GetComponent<PlayerShield>();
        playerData = GetComponent<Player>().playerData;
        maxHealth = playerData.maxHealth;
        currentHealth = maxHealth;
    }

    private void OnDisable()
    {
        Powerup.OnHealthPowerupCollected -= PowerupHealth;
        PlayerShield.OnShieldActiveChanged -= CheckShieldActive;
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
                TakeDamage(2); // damage = 2
                HealthChanged();
                break;
        }
    }

    public void TakeDamage(int damage)
    {
        if (shieldActive)
        {
            if (playerShield.CheckShieldStrength() >= damage)
                playerShield.DamageShield(damage);
            else
            {
                playerShield.DamageShield(1);
                currentHealth -= damage - 1;
            }
        }
        else
        {
            currentHealth -= damage;
            HealthChanged();
        }
    }

    private void PlayerDestroyed()
    {
        OnPlayerDestroyed?.Invoke();
        Destroy(gameObject);
    }

    private void HealthChanged()
    {
        if (currentHealth > maxHealth) currentHealth = maxHealth;
        if (currentHealth < 0) currentHealth = 0;
        OnHealthChanged?.Invoke(currentHealth);
        if (currentHealth == 0) PlayerDestroyed();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        var iDamage = other.gameObject.GetComponent<IDamageable>();
        if (iDamage == null) return; // || !other.gameObject.CompareTag("Enemy")
        iDamage.TakeDamage(1);
        TakeDamage(1);
    }

    private void CheckShieldActive(bool checkShield)
    {
        shieldActive = checkShield;
        Debug.Log($"{shieldActive}... shield ");
    }
}
}
