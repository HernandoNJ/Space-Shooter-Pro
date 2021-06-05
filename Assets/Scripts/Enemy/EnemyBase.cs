using System;
using System.Collections.Generic;
using Interfaces;
using ScriptableObjects.Inventory.Weapon;
using SO;
using Starting;
using UnityEngine;

namespace Enemy
{
public abstract class EnemyBase : MonoBehaviour, ITakeDamage, IMove, IShoot
{
    public EnemyData enemyData;
    private Animator animator;
    private AudioSource audioSource;
    private EnemyType enemyType;
    private GameObject explosionPrefab;
    private GameObject weapon;
    private GameObject firePoint;
    private bool isAlive;
    private float speed;
    private float fireRate;
    private float timeToFire;
    private int collisionDamage;
    private float currentHealth;
    private int scorePoints;
    protected List<WeaponData> weapons = new List<WeaponData>();

    public static Action onEnemyDestroyed;

    private void Start()
    {
        ConfigureEnemy(enemyData);
    }

    private void Update()
    {
        MoveEnemy(speed);
        FireWeapon(fireRate);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;
        other.GetComponent<ITakeDamage>().TakeDamage(collisionDamage);
        TakeDamage(collisionDamage);
    }

    private void ConfigureEnemy(EnemyData _data)
    {
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        collisionDamage = _data.collisionDamage;
        currentHealth = _data.maxHealth;
        scorePoints = _data.scorePoints;
        enemyType = _data.enemyType;
        explosionPrefab = _data.explosionPrefab;
        firePoint = gameObject.transform.Find("FirePoint").gameObject;
        fireRate = _data.fireRate;
        isAlive = true;
        speed = _data.speed;
        weapon = _data.weapon;
    }

    public virtual void MoveEnemy(float speed)
    {
        transform.Translate(Vector2.down * (speed * Time.deltaTime));
        CheckBottomPosition();
    }

    private void CheckBottomPosition()
    {
        if (transform.position.y <= -6.0f)
            // Reuse enemy in random pos.x
            transform.position = new Vector2(UnityEngine.Random.Range(-8.0f, 8.0f), 5.0f);
    }

    public virtual void FireWeapon(float fireRate)
    {
        if (Time.time > timeToFire && isAlive)
        {
            Instantiate(weapon, firePoint.transform.position, Quaternion.identity);
            fireRate = UnityEngine.Random.Range(2f, 5f);
            timeToFire = Time.time + fireRate;
        }
    }

    public virtual void TakeDamage(float damage)
    {
        currentHealth -= damage;
        if (currentHealth > 0) return;
        isAlive = false;
        EnemyDestroyed();
    }

    private void EnemyDestroyed()
    {
        PlayerStart.onAddScore?.Invoke(scorePoints);
        onEnemyDestroyed?.Invoke();

        Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        speed = 0f;
        animator.SetTrigger("OnEnemyDestroyed");
        audioSource.Play();
        Destroy(GetComponent<Collider2D>());
        gameObject.SetActive(false); // added because enemy was hitting the player twice
        Destroy(gameObject, 2.0f);
    }

}
}

