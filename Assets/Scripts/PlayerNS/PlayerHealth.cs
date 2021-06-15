using System;
using Interfaces;
using Powerups;
using UnityEngine;

namespace PlayerNS
{
public class PlayerHealth : MonoBehaviour, IShootable
{
    public PlayerData playerData;
    [SerializeField] private int collisionDamage;
    [SerializeField] private int maxHealth = 10;
    [SerializeField] private float health;

    public event Action OnPlayerDie;

    private void OnEnable()
    {
        Player.OnCollisionPlayer += PlayerCollision;
        Powerup.OnHealthPowerupCollected += UpdateHealthInfo;

        playerData = GetComponent<Player>().playerData;
        collisionDamage = playerData.collisionDamage;
        maxHealth = playerData.maxHealth;
        health = maxHealth;
    }

    private void OnDisable() => Player.OnCollisionPlayer -= PlayerCollision;

    private void UpdateHealthInfo(int healthModifier) => health += healthModifier;

    private void PlayerCollision(Collision2D other)
    {
        var iDamage = other.collider.GetComponent<IShootable>();
        iDamage?.TakeDamage(collisionDamage);
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        if (health <= 0) Die();
    }

    private void Die()
    {
        OnPlayerDie?.Invoke();
        Destroy(gameObject);
    }
}
}
