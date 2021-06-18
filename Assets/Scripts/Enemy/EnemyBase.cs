using System;
using System.Collections.Generic;
using Interfaces;
using Starting;
using UnityEngine;
using Weapon;

namespace Enemy
{
public abstract class EnemyBase : MonoBehaviour, IDamageable, IMove, IWeaponShooter, ICollisionable
{
    public EnemyData enemyData;
    private Animator animator;
    private AudioSource audioSource;
    private EnemyType enemyType;
    private GameObject explosionPrefab;
    private GameObject weapon;
    private Transform firePoint;
    private bool isAlive;
    private float speed;
    private float fireRate;
    private float timeToFire;
    private int collisionDamage;
    private float currentHealth;
    private int scorePoints;
    protected List<WeaponData> weapons = new List<WeaponData>();

    public int ColDamage{ get; set; }

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
        other.GetComponent<IDamageable>().TakeDamage(collisionDamage);
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
        firePoint = gameObject.transform.Find("FirePoint").transform;
        fireRate = _data.fireRate;
        isAlive = true;
        speed = _data.speed;
        weapon = _data.weapon;

        ColDamage = 1;
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

    public virtual void FireWeapon(float fireRateArg)
    {
        if (!((Time.time > timeToFire) && isAlive)) return;

        Instantiate(weapon, firePoint.transform.position, Quaternion.identity);
        fireRateArg = UnityEngine.Random.Range(2f, 5f);
        timeToFire = Time.time + fireRateArg;
    }

    public virtual void TakeDamage(int damage)
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
        gameObject.SetActive(false); // TODO fix: added because enemy was hitting the player twice
        Destroy(gameObject, 2.0f);
    }

    private void OnCollisionEnter(Collision other)
    {
        var iCollision = other.gameObject.GetComponent<ICollisionable>();
        iCollision?.CollisionDamage(ColDamage);
    }

    public void CollisionDamage(int colDamage)
    {
        currentHealth -= colDamage;
    }
}
}
