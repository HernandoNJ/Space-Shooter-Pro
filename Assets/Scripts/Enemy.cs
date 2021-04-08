﻿using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private Player player;
    [SerializeField] private Animator anim;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private GameObject doubleLaserPrefab;

    [SerializeField] private bool isEnemyAlive;
    [SerializeField] private float enemySpeed = 2f;

    private float fireRate = 3f;
    private float canFire = -1;

    private void Start()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
        if (player == null) Debug.LogError("There is not Player script in player");

        anim = GetComponent<Animator>();
        if (anim == null) Debug.LogError("anim is null in Enemy script");

        audioSource = GetComponent<AudioSource>();
        if (audioSource == null) Debug.LogError("audioSource is null in Enemy script");

        isEnemyAlive = true;
    }

    private void Update()
    {
        MoveEnemy();
        ShotDoubleLaser();
    }
    
    private void MoveEnemy()
    {
        transform.Translate(Vector2.down * enemySpeed * Time.deltaTime);

        if (transform.position.y <= -6.0f)
        {
            // place enemy at top (reuse the enemy) in random pos.x
            float randomXPos = Random.Range(-8.0f, 8.0f);
            transform.position = new Vector2(randomXPos, 5.0f);
        }
    }

    public void ShotDoubleLaser()
    {
        if (Time.time > canFire && isEnemyAlive)
        {
            fireRate = Random.Range(3f, 7f);
            canFire = Time.time + fireRate;

            Instantiate(doubleLaserPrefab, transform.position, Quaternion.identity);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Ignore collision if other is an Enemy
        if (other.transform.parent != null && other.transform.parent.CompareTag("EnemyLaser")) 
            return;

        if (other.CompareTag("Player"))
        {
            player.Damage(1);
            enemySpeed = 0f;
            anim.SetTrigger("OnEnemyDestroyed");
            audioSource.Play();
            Destroy(GetComponent<Collider2D>());
            gameObject.SetActive(false); // added because enemy was hitting the player twice
            Destroy(gameObject, 2.0f);
        }
    }

    // Called by a laser when hit
    public void OnEnemyDestroyed()
    {
        isEnemyAlive = false;
        anim.SetTrigger("OnEnemyDestroyed");
        enemySpeed = 0f;
        audioSource.Play();
        Destroy(GetComponent<Collider2D>());
        Destroy(gameObject, 2.0f);
    }
}

/* FIXED: Enemies are destroying themselves with double laser - changing tag not working
 * FIXED: Enemy shoting after destroyed
 */

