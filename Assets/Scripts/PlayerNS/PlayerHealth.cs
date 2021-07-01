using System;
using EnemyNS;
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
    [SerializeField] private bool bossActive;
    [SerializeField] private int maxHealth = 3;
    [SerializeField] private int currentHealth;

    public static event Action<int> OnHealthChanged;
    public static event Action OnPlayerDestroyed;

    private void OnEnable()
    {
        Player.OnPlayerEnterTrigger += PlayerEnterTrigger;
        Powerup.OnHealthPowerupCollected += PowerupHealth;
        PlayerShield.OnShieldActive += SetShieldStatus;
        EnemyBoss.OnBossStarted += SetHealthForBossWave;

        SetPlayerHealthValues();
    }

    private void OnDisable()
    {
        Player.OnPlayerEnterTrigger -= PlayerEnterTrigger;
        Powerup.OnHealthPowerupCollected -= PowerupHealth;
        PlayerShield.OnShieldActive -= SetShieldStatus;
        EnemyBoss.OnBossStarted -= SetHealthForBossWave;
    }

    private void SetPlayerHealthValues()
    {
        playerShield = GetComponent<PlayerShield>();
        playerData = GetComponent<Player>().playerData;
        maxHealth = playerData.maxHealth;
        currentHealth = maxHealth;
    }

    private void SetHealthForBossWave(int obj)
    {
        bossActive = true;
        maxHealth = 100;
        currentHealth = maxHealth;
        HealthChanged();
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

    private void PlayerEnterTrigger(Collider2D other)
    {
        var iDamage = other.gameObject.GetComponent<IDamageable>();
        if (iDamage == null) return;
        iDamage.TakeDamage(1);
        TakeDamage(1);
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

    private void HealthChanged()
    {
        if (currentHealth > maxHealth) currentHealth = maxHealth;
        else if (currentHealth < 0) currentHealth = 0;
        else if (currentHealth == 0) PlayerDestroyed();
        OnHealthChanged?.Invoke(currentHealth);
    }

    private void SetShieldStatus(bool isShieldEnabled)
    {
        shieldActive = isShieldEnabled;
    }

    private void PlayerDestroyed()
    {
        StopAllCoroutines();
        Destroy(gameObject);
        OnPlayerDestroyed?.Invoke();
    }
}
}
