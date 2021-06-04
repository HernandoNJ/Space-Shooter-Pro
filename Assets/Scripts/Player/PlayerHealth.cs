using System;
using ScriptableObjects.Player;
using UnityEngine;

namespace Player
{
public class PlayerHealth : MonoBehaviour, ITakeDamage
{
    public PlayerData playerData;
    [SerializeField] private int collisionDamage;
    [SerializeField] private int maxHealth = 10;
    [SerializeField] private int health;

    public event Action OnDie;

    private void OnEnable()
    {
        playerData = GetComponent<Player>().playerData;
        collisionDamage = playerData.collisionDamage;
        maxHealth = playerData.maxHealth;
        health = maxHealth;
    }

    private void OnCollisionEnter(Collision other)
    {
        var iDamage = other.collider.GetComponent<ITakeDamage>();
        iDamage?.TakeDamage(collisionDamage);
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0) Die();
    }

    private void Die()
    {
        OnDie?.Invoke();
        Destroy(gameObject);
    }
}
}
