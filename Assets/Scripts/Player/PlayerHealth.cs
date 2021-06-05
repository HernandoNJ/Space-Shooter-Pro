using System;
using Interfaces;
using ScriptableObjects.Player;
using UnityEngine;

namespace Player
{
public class PlayerHealth : MonoBehaviour, ITakeDamage
{
    public PlayerData playerData;
    [SerializeField] private int collisionDamage;
    [SerializeField] private int maxHealth = 10;
    [SerializeField] private float health;

    public event Action OnPlayerDie;

    private void OnEnable()
    {
        GetComponent<Player>().onCollisionPlayer += PlayerCollision;
    
        playerData = GetComponent<Player>().playerData;
        collisionDamage = playerData.collisionDamage;
        maxHealth = playerData.maxHealth;
        health = maxHealth;
    }

    private void OnDisable()
    {
        GetComponent<Player>().onCollisionPlayer -= PlayerCollision;
    }

    private void PlayerCollision(Collision2D other)
    {
        var iDamage = other.collider.GetComponent<ITakeDamage>();
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
