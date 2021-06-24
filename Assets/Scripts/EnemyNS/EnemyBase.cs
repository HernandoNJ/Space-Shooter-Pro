using System;
using System.Collections.Generic;
using Interfaces;
using Starting;
using UnityEngine;

namespace EnemyNS
{
public abstract class EnemyBase : MonoBehaviour, IDamageable, IMove
{
    public EnemyData enemyData;
    [SerializeField] private Animator animator;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private EnemyType enemyType;
    [SerializeField] private GameObject explosionPrefab;
    [SerializeField] private Transform firePoint;
    [SerializeField] protected List<GameObject> weapons;
    [SerializeField] protected GameObject weapon;
    [SerializeField] protected int weaponIndex;
    [SerializeField] private bool isAlive;
    [SerializeField] private float speed;
    [SerializeField] private float fireRate;
    [SerializeField] private float timeToFire;
    [SerializeField] private float currentHealth;
    [SerializeField] private int scorePoints;

    public static event Action OnEnemyDestroyed;

    private void Start()
    {
        ConfigureEnemy(enemyData);
    }

    private void Update()
    {
        MoveEnemy(speed);
        FireWeapon();
    }

    private void ConfigureEnemy(EnemyData _data)
    {
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        currentHealth = _data.maxHealth;
        scorePoints = _data.scorePoints;
        enemyType = _data.enemyType;
        explosionPrefab = _data.explosionPrefab;
        firePoint = gameObject.transform.Find("FirePoint").transform;
        fireRate = _data.fireRate;
        isAlive = true;
        speed = _data.speed;
        //weapon = weapons[weaponIndex];
        //weapon = _data.weapons[0]; // todo set weapon in children
    }

    public virtual void MoveEnemy(float speedArg)
    {
        transform.Translate(Vector2.down * (speedArg * Time.deltaTime));
        CheckBottomPosition();
    }

    private void CheckBottomPosition()
    {
        if (transform.position.y <= -6.0f)
            // Reuse enemy in random pos.x
            transform.position = new Vector2(UnityEngine.Random.Range(-8.0f, 8.0f), 5.0f);
    }

    private void FireWeapon()
    {
        if (!((Time.time > timeToFire) && isAlive)) return;

        Instantiate(weapon, firePoint.transform.position, Quaternion.identity);
        fireRate = UnityEngine.Random.Range(2f, 5f);
        timeToFire = Time.time + fireRate;
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
        PlayerStart.onAddScore?.Invoke(scorePoints); // todo: remove later
        OnEnemyDestroyed?.Invoke();

        Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        speed = 0f;
        animator.SetTrigger("OnEnemyDestroyed");
        audioSource.Play();
        Destroy(GetComponent<Collider2D>());
        gameObject.SetActive(false); // TODO: fix added because enemy was hitting the player twice
        Destroy(gameObject, 2.0f);
    }

    // private void OnCollisionEnter(Collision other)
    // {
    //     var iCollision = other.gameObject.GetComponent<ICollisionable>();
    //     iCollision?.CollisionDamage(ColDamage);
    // }

    // public void CollisionDamage(int colDamage)
    // {
    //     currentHealth -= colDamage;
    // }

    // private void OnTriggerEnter2D(Collider2D other)
    // {
    //     if (!other.CompareTag("Player")) return;
    //     other.GetComponent<IDamageable>().TakeDamage(collisionDamage);
    //     TakeDamage(collisionDamage);
    // }

}
}
